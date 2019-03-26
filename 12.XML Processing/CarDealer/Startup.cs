namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using AutoMapper;

    using Models;
    using Data;
    using Dtos;
    using Dtos.ExerciseFiveDto;
    using Dtos.ExerciseFourDto;
    using Dtos.ExerciseSixDto;
    using Dtos.ExerciseThreeDto;
    using Dtos.ExerciseTwoDto;

    public class Startup
    {
        public static void Main()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            using (var context = new CarDealerContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //Seed();

                SalesWithAppliedDiscount(context);
            }
        }

        #region Query and Export Data

        public static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(x => new
                {
                    x.Customer.Name,
                    x.Car,
                    x.Discount,
                    Price = x.Car.PartCars.Select(y => y.Part.Price).Sum()
                })
                .ToArray()
                .Select(x => new SaleDto()
                {
                    Carr = new SaleCarDto()
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    FullName = x.Name,
                    Discount = x.Discount,
                    Price = x.Price,
                    PriceWothDiscount = x.Price - (x.Price * (decimal)(x.Discount / 100.0))
                })
                .ToArray();

            var root = new XmlRootAttribute("sales");
            var serializer = new XmlSerializer(typeof(SaleDto[]), root);
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), sales, namespaces);

            File.WriteAllText("../../../Export/sales-discounts.xml", sb.ToString());
        }

        public static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(x => x.Sales.Any())
                .Select(x => new 
                {
                    x.Name,
                    x.Sales.Count,
                    CarsPrices = x.Sales
                        .Select(s => s.Car.PartCars.Select(pc => pc.Part.Price).Sum())
                        .ToList()
                })
                .ToArray()
                .Select(x => new SalesByCustomerDto()
                   {
                       FullName = x.Name,
                       BoughtCarsCount = x.Count,
                       SpentMoney = x.CarsPrices.Sum()
                   })
                .ToArray();

            var root = new XmlRootAttribute("customers");
            var serializer = new XmlSerializer(typeof(SalesByCustomerDto[]), root);
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), customers, namespaces);

            File.WriteAllText("../../../Export/customers-total-sales.xml", sb.ToString());
        }

        public static void CarsWithParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new CarDto()
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance,
                    Parts = x.PartCars.Select(pc => new PartDto()
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price
                    })
                   .ToArray()
                })
                .ToArray();

            var root = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(CarDto[]), root);
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), cars, namespaces);

            File.WriteAllText("../../../Export/cars-and-parts.xml", sb.ToString());
        }

        public static void LocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new LocalSupplierDto()
                {
                    Id = x.SupplierId,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToArray();

            var root = new XmlRootAttribute("suppliers");
            var serializer = new XmlSerializer(typeof(LocalSupplierDto[]), root);
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), suppliers, namespaces);

            File.WriteAllText("../../../Export/local-suppliers.xml", sb.ToString());
        }

        public static void CarsFromFerrari(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.Make == "Ferrari")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(x => new FerrariCarDto()
                {
                    Id = x.CarId,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .ToArray();

            var root = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(FerrariCarDto[]), root);
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), cars, namespaces);

            File.WriteAllText("../../../Export/ferrari-cars.xml", sb.ToString());
        }

        public static void CarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.TravelledDistance > 2_000_000)
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Select(x => new ImportCarDto()
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .ToArray();

            var root = new XmlRootAttribute("cars");
            var serializer = new XmlSerializer(typeof(ImportCarDto[]), root);
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            var sb = new StringBuilder();

            serializer.Serialize(new StringWriter(sb), cars, namespaces);

            File.WriteAllText("../../../Export/cars.xml", sb.ToString());
        }

        #endregion

        #region Seeding

        private static void Seed()
        {
            SeedSuppliers();
            SeedParts();
            SeedCars();
            SeedPartCars();
            SeedCustomers();
            SeedSales();
        }

        private static void SeedCustomers()
        {
            using (var context = new CarDealerContext())
            {
                var root = new XmlRootAttribute("customers");
                var serializer = new XmlSerializer(typeof(ImportCustomersDto[]), root);
                var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

                var stringCustomers = File.ReadAllText("Import/customers.xml");
                var customersDto = (ImportCustomersDto[])serializer.Deserialize(new StringReader(stringCustomers));

                var customers = new List<Customer>();
                foreach (var dto in customersDto)
                {
                    var customer = Mapper.Map<Customer>(dto);
                    customers.Add(customer);
                }

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        private static void SeedCars()
        {
            using (var context = new CarDealerContext())
            {
                var root = new XmlRootAttribute("cars");
                var serializer = new XmlSerializer(typeof(ImportCarDto[]), root);
                var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

                var stringCars = File.ReadAllText("Import/cars.xml");
                var partsDto = (ImportCarDto[])serializer.Deserialize(new StringReader(stringCars));

                var cars = new List<Car>();
                foreach (var dto in partsDto)
                {
                    var car = Mapper.Map<Car>(dto);
                    cars.Add(car);
                }

                context.Cars.AddRange(cars);
                context.SaveChanges();
            }
        }

        private static void SeedParts()
        {
            using (var context = new CarDealerContext())
            {
                var root = new XmlRootAttribute("parts");
                var serializer = new XmlSerializer(typeof(ImportPartDto[]), root);
                var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

                var stringParts = File.ReadAllText("Import/parts.xml");

                var partsDto = (ImportPartDto[])serializer.Deserialize(new StringReader(stringParts));

                var random = new Random();
                var suppliersCount = context.Suppliers.Count();

                var parts = new List<Part>();
                foreach (var dto in partsDto)
                {
                    var part = Mapper.Map<Part>(dto);

                    var supplierId = random.Next(1, suppliersCount + 1);
                    part.SupplierId = supplierId;

                    parts.Add(part);
                }

                context.AddRange(parts);
                context.SaveChanges();
            }
        }

        private static void SeedSuppliers()
        {
            using (var context = new CarDealerContext())
            {
                var root = new XmlRootAttribute("suppliers");
                var serializer = new XmlSerializer(typeof(ImportSupplierDto[]), root);
                var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

                var stringSuppliers = File.ReadAllText("Import/suppliers.xml");

                var suppliersDto = (ImportSupplierDto[])serializer.Deserialize(new StringReader(stringSuppliers));

                var suppliers = new List<Supplier>();
                foreach (var dto in suppliersDto)
                {
                    var supplier = Mapper.Map<Supplier>(dto);
                    suppliers.Add(supplier);
                }

                context.Suppliers.AddRange(suppliers);
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

        #endregion
    }
}