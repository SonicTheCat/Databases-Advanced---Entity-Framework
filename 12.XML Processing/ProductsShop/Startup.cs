namespace ProductsShop
{
    using DataAnotations = System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using System;
    using System.Linq;
    using System.Text;

    using AutoMapper;

    using Data;
    using Dtos;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            using (var context = new ProductsShopContext())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<ProductShopProfile>();
                });

                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //Seed();

                UsersAndProducts(context);
            }
        }

        #region Query and Export Data

        public static void UsersAndProducts(ProductsShopContext context)
        {
            //Всичко стана паприкаш...

            var users = new UsersDto()
            {
                Count = context.Users.Count(),
                Users = context.Users.Select(x => new UsersProductsDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age.ToString(),
                    Products = new ProductExerciseFourDto()
                    {
                        Count = x.SellingProducts.Count,
                        SoldProducts = x.SellingProducts.Select(y => new SoldProductExerciseFourDto()
                        {
                            Name = y.Name, 
                            Price = y.Price
                        })
                        .ToArray()
                    }
                })
                .ToArray()
            };

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(UsersDto), new XmlRootAttribute("users"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), users, namespaces);

            File.WriteAllText("../../../Export/users-and-products.xml", sb.ToString());
        }

        public static void CategoriesByProductsCount(ProductsShopContext context)
        {
            var categories = context.Categories
                 .Select(x => new
                 {
                     x.Name,
                     Prices = x.CategoryProducts.Select(cp => cp.Product.Price).ToList(),
                 })
                 .ToArray()
                 .Select(x => new CategoriesByCountDto()
                 {
                     Name = x.Name,
                     ProductsCount = x.Prices.Count,
                     AveragePrice = Math.Round(x.Prices.Count == 0 ? 0 : x.Prices.Sum() / x.Prices.Count, 6),
                     TotalRevenue = Math.Round(x.Prices.Sum(), 2)
                 })
                   .OrderByDescending(x => x.ProductsCount)
                 .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CategoriesByCountDto[]), new XmlRootAttribute("categories"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), categories, namespaces);

            File.WriteAllText("../../../Export/categories-by-products.xml", sb.ToString());

        }

        public static void SoldProducts(ProductsShopContext context)
        {
            var users = context.Users
                .Where(x => x.SellingProducts.Any(sp => sp.Buyer != null))
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new UserSoldProductsDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.SellingProducts.Select(sp => new SoldProductsDto()
                    {
                        Name = sp.Name,
                        Price = sp.Price
                    })
                    .ToArray()
                })
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(UserSoldProductsDto[]), new XmlRootAttribute("users"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), users, namespaces);

            File.WriteAllText("../../../Export/users-sold-products.xml", sb.ToString());
        }

        public static void ProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                .Where(x => x.Buyer != null && x.Price >= 1000 && x.Price <= 2000)
                .OrderBy(x => x.Price)
                .Select(x => new ProductInRangeDto()
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName ?? x.Buyer.LastName
                })
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ProductInRangeDto[]), new XmlRootAttribute("products"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), products, namespaces);

            File.WriteAllText("../../../Export/products-in-range.xml", sb.ToString());
        }

        #endregion

        #region Seeding

        private static void Seed()
        {
            SeedUsers();
            SeedProducts();
            SeedCategories();
            SeedCategoryProducts();
        }

        private static void SeedCategories()
        {
            using (var context = new ProductsShopContext())
            {
                var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
                

                var stringCategories = File.ReadAllText("Import/categories.xml");

                var categoriesDto = (CategoryDto[])serializer.Deserialize(new StringReader(stringCategories));

                var categories = new List<Category>();
                foreach (var dto in categoriesDto)
                {
                    if (!IsValid(dto))
                    {
                        continue;
                    }

                    var category = Mapper.Map<Category>(dto);

                    categories.Add(category);
                }

                context.AddRange(categories);
                context.SaveChanges();
            }
        }

        private static void SeedProducts()
        {
            using (var context = new ProductsShopContext())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
                var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

                var stringProducts = File.ReadAllText("Import/products.xml");

                var productsDto = (ProductDto[])serializer.Deserialize(new StringReader(stringProducts));

                var random = new Random();
                var products = new List<Product>();
                var usersCount = context.Users.Count();

                foreach (var dto in productsDto)
                {
                    if (!IsValid(dto))
                    {
                        continue;
                    }

                    var product = Mapper.Map<Product>(dto);

                    int sellerId;
                    int? buyerId;

                    do
                    {
                        sellerId = random.Next(1, usersCount + 1);
                        buyerId = random.Next(1, usersCount + 1);

                    } while (sellerId == buyerId);

                    if (buyerId % 2 == 0 && buyerId < usersCount / 2)
                    {
                        buyerId = null;
                    }

                    product.SellerId = sellerId;
                    product.BuyerId = buyerId;

                    products.Add(product);
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        private static void SeedUsers()
        {
            using (var context = new ProductsShopContext())
            {
                var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
                var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

                var stringUsers = File.ReadAllText("Import/users.xml");

                var usersDto = (UserDto[])serializer.Deserialize(new StringReader(stringUsers));

                var users = new List<User>();
                foreach (var userDto in usersDto)
                {
                    if (!IsValid(userDto))
                    {
                        continue;
                    }

                    var user = Mapper.Map<User>(userDto);

                    users.Add(user);
                }

                context.AddRange(users);
                context.SaveChanges();
            }
        }

        private static void SeedCategoryProducts()
        {
            using (var context = new ProductsShopContext())
            {
                var productsCount = context.Products.Count();
                var categoriesCount = context.Categories.Count();

                var categoryProducts = new List<CategoryProducts>();
                for (int i = 1; i <= productsCount; i++)
                {
                    categoryProducts.AddRange(CombineCategoryProducts(categoriesCount, i));
                }

                context.CategoryProducts.AddRange(categoryProducts);

                context.SaveChanges();
            }
        }

        public static List<CategoryProducts> CombineCategoryProducts(int categoriesCount, int productId)
        {
            var random = new Random();
            int categoryOneId;
            int categoryTwoId;

            do
            {
                categoryOneId = random.Next(1, categoriesCount);
                categoryTwoId = random.Next(1, categoriesCount);

            } while (categoryOneId == categoryTwoId);

            return new List<CategoryProducts>()
            {
                new CategoryProducts(){ProductId = productId, CategoryId = categoryOneId },
                new CategoryProducts(){ProductId = productId, CategoryId = categoryTwoId }
            };
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new DataAnotations.ValidationContext(obj);
            var validationsResult = new List<DataAnotations.ValidationResult>();

            var result = DataAnotations.Validator.TryValidateObject(obj, validationContext, validationsResult, true);

            return result;
        }

        #endregion 
    }
}