namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Data;
    using Models;
    
    public class Startup
    {
        public static void Main()
        {
            using (var context = new CarDealerContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //Seed();

                SalesDiscounts(context);
            }
        }

        #region Query and Export Data

        private static void SalesDiscounts(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(x => new
                {
                    car = x.Car,
                    customerName = x.Customer.Name,
                    x.Discount,
                    price = x.Car.PartCars.Select(pc => pc.Part.Price).Sum()
                })
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
                    x.Discount,
                    x.price,
                    priceWithDiscount = x.price - ((decimal)(x.Discount / 100.0) * x.price)
                })
                .ToArray(); 

            var json = JsonConvert.SerializeObject(sales, Formatting.Indented);

            File.WriteAllText(Paths.result + "/sales-discounts.json", json); 
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
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
                    spentMoney = x.sales
                        .Sum(s => s.Price - ((decimal)(s.Discount / 100.0) * s.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList(); 

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText(Paths.result + "/customers-total-sales.json", json); 
        }

        private static void CarsWithParts(CarDealerContext context)
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
                        p.Part.Price
                    })
                    .ToList()
                })
                .ToList();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText(Paths.result + "/cars-and-parts.json", json);
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            var values = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.SupplierId,
                    x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            var json = JsonConvert.SerializeObject(values, Formatting.Indented);

            File.WriteAllText(Paths.result + "/local-suppliers.json", json);
        }

        private static void CarsFromToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.Make == "Toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText(Paths.result + "/toyota-cars.json", json);
        }

        private static void OrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                    .OrderBy(x => x.BirthDate)
                    .ThenBy(x => x.IsYoungDriver)
                    .ToList();

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText(Paths.result + "/ordered-customers.json", json);
        }

        #endregion

        #region Seed

        public static void Seed()
        {
            SeedSuppliers();
            SeedParts();
            SeedCars();
            SeedPartCars();
            SeedCustomers();
            SeedSales();
        }

        private static void SeedSales()
        {
            var discounts = new int[] { 0, 5, 10, 15, 20, 30, 40, 50 };

            using (var context = new CarDealerContext())
            {
                var carsCount = context.Cars.Count();
                var customersCount = context.Customers.Count();

                var random = new Random();

                var sales = new List<Sale>();

                for (int i = 0; i < carsCount / 2; i++)
                {
                    var customerId = random.Next(1, customersCount);
                    var carId = random.Next(1, carsCount);
                    double discount = discounts[random.Next(0, discounts.Length)];

                    if (context.Customers.Find(customerId).IsYoungDriver)
                    {
                        discount *= 1.05;
                    }

                    sales.Add(new Sale()
                    {
                        CustomerId = customerId,
                        CarId = carId,
                        Discount = discount
                    });
                }

                context.Sales.AddRange(sales);

                context.SaveChanges();
            }
        }

        private static void SeedCustomers()
        {
            using (var context = new CarDealerContext())
            {
                var json = File.ReadAllText(Paths.path + Paths.customers);
                var customers = JsonConvert.DeserializeObject<Customer[]>(json);

                context.Customers.AddRange(customers);

                context.SaveChanges();
            }
        }

        private static void SeedPartCars()
        {
            using (var context = new CarDealerContext())
            {
                var partsCount = context.Parts.Count();
                var cars = context.Cars.ToList();

                var random = new Random();

                var partCars = new List<PartCars>();

                for (int carId = 1; carId <= cars.Count; carId++)
                {
                    var partIds = new int[random.Next(10, 20)];
                    for (int j = 0; j < partIds.Length; j++)
                    {
                        int partId;

                        do
                        {
                            partId = random.Next(1, partsCount + 1);

                        } while (partIds.Contains(partId));

                        partIds[j] = partId;
                    }

                    foreach (var partId in partIds)
                    {
                        partCars.Add(new PartCars()
                        {
                            CarId = carId,
                            PartId = partId
                        });
                    }
                }

                context.PartCars.AddRange(partCars);

                context.SaveChanges();
            }
        }

        private static void SeedCars()
        {
            using (var context = new CarDealerContext())
            {
                var cars = GetValues<Car>(Paths.cars);

                context.Cars.AddRange(cars);

                context.SaveChanges();
            }
        }

        private static void SeedParts()
        {
            using (var context = new CarDealerContext())
            {
                var parts = GetValues<Part>(Paths.parts);

                var random = new Random();

                var suppliersCount = context.Suppliers.Count();
                foreach (var part in parts)
                {
                    var supplierId = random.Next(1, suppliersCount);
                    part.SupplierId = supplierId;
                }

                context.Parts.AddRange(parts);

                context.SaveChanges();
            }
        }

        private static void SeedSuppliers()
        {
            using (var context = new CarDealerContext())
            {
                var suppliers = GetValues<Supplier>(Paths.suppliers);

                context.Suppliers.AddRange(suppliers);

                context.SaveChanges();
            }
        }

        private static T[] GetValues<T>(string path)
        {
            var json = File.ReadAllText(Paths.path + path);

            return JsonConvert.DeserializeObject<T[]>(json);
        }

        #endregion
    }
}