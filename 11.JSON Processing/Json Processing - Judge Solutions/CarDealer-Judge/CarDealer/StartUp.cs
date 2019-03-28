using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        private const string path = @"C:\Users\Petya\Desktop\SoftUniPavel\CarDealer-Judge\CarDealer\Datasets";

        public static void Main()
        {
            using (var context = new CarDealerContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                //Seed(context);

                var result = GetTotalSalesByCustomer(context);
                Console.WriteLine(result);
            }
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
               .Select(x => new
               {
                   car = x.Car,
                   customerName = x.Customer.Name,
                   x.Discount,
                   price = x.Car.PartCars.Select(pc => pc.Part.Price).Sum()
               })
               .Take(10)
               .ToArray()
               .Select(x => new
               {
                   car = new
                   {
                       x.car.Make,
                       x.car.Model,
                       x.car.TravelledDistance
                   },
                   x.customerName,
                   Discount = x.Discount.ToString("F2"),
                   price = x.price.ToString("F2"),
                   priceWithDiscount = (x.price - ((x.Discount / 100.0m) * x.price)).ToString("F2")
               })
               .ToArray();

            return JsonConvert.SerializeObject(sales, Formatting.Indented);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
              .Where(x => x.Sales.Any())
              .Select(x => new
              {
                  fullName = x.Name,
                  boughtCars = x.Sales.Count,
                  sales = x.Sales.Select(s => new
                  {
                      Price = s.Car.PartCars.Select(pc => pc.Part.Price).Sum(),
                      s.Discount
                  })
                  .ToList()
              })
              .ToList()
              .Select(x => new
              {
                  x.fullName,
                  x.boughtCars,
                  spentMoney = x.sales.Sum(s => s.Price)
              })
              .OrderByDescending(x => x.spentMoney)
              .ThenByDescending(x => x.boughtCars)
              .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new
                {
                    car = new
                    {
                        x.Make,
                        x.Model,
                        x.TravelledDistance
                    },
                    parts = x.PartCars.Select(p => new
                    {
                        p.Part.Name,
                        Price = p.Part.Price.ToString("F2")
                    })
                    .ToList()
                })
                .ToList();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var values = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            return JsonConvert.SerializeObject(values, Formatting.Indented);
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
               .Where(x => x.Make == "Toyota")
               .Select(x => new
               {
                   x.Id,
                   x.Make,
                   x.Model,
                   x.TravelledDistance
               })
               .OrderBy(x => x.Model)
               .ThenByDescending(x => x.TravelledDistance)
               .ToList();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                   .OrderBy(x => x.BirthDate)
                   .ThenBy(x => x.IsYoungDriver)
                   .Select(x => new
                   {
                       x.Name,
                       BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
                       x.IsYoungDriver
                   })
                   .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        #region Seeding

        public static void Seed(CarDealerContext context)
        {
            string json;
            string result;

            json = File.ReadAllText(path + @"\suppliers.json");
            result = ImportSuppliers(context, json);

            json = File.ReadAllText(path + @"\parts.json");
            result = ImportParts(context, json);

            json = File.ReadAllText(path + @"\cars.json");
            result = ImportCars(context, json);

            json = File.ReadAllText(path + @"\customers.json");
            result = ImportCustomers(context, json);

            json = File.ReadAllText(path + @"\sales.json");
            result = ImportSales(context, json);
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            var youngDrivers = context.Customers
                .Where(x => x.IsYoungDriver)
                .Select(x => x.Id)
                .ToArray();

            foreach (var sale in sales)
            {
                if (youngDrivers.Contains(sale.CustomerId))
                {
                    sale.Discount += 5;
                }
            }

            context.Sales.AddRange(sales);

            var seededLength = context.SaveChanges();

            return $"Successfully imported {seededLength}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);

            var seededLength = context.SaveChanges();

            return $"Successfully imported {seededLength}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var values = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);

            var cars = new List<Car>();
            var partsCar = new List<PartCar>();

            var partsCount = context.Parts.Count();

            foreach (var value in values)
            {
                var car = new Car()
                {
                    Make = value.Make,
                    Model = value.Model,
                    TravelledDistance = value.TravelledDistance
                };

                foreach (var part in value.PartsId.Distinct())
                {
                    if (part <= partsCount)
                    {
                        partsCar.Add(new PartCar()
                        {
                            Car = car,
                            PartId = part
                        });
                    }
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);

            var seededCarsLength = context.SaveChanges();

            context.PartCars.AddRange(partsCar);

            context.SaveChanges();

            return $"Successfully imported {seededCarsLength}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var suppliersCount = context.Suppliers.Count();

            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(x => x.SupplierId < suppliersCount)
                .ToArray();

            context.Parts.AddRange(parts);

            var seededLength = context.SaveChanges();

            return $"Successfully imported {seededLength}.";
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);

            var seededLength = context.SaveChanges();

            return $"Successfully imported {seededLength}.";
        }

        #endregion
    }
}