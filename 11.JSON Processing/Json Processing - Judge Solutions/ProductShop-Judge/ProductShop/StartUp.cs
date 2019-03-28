namespace ProductShop
{
    using System;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using ProductShop.Data;
    using ProductShop.Models;

    public class StartUp
    {
        private const string path = @"C:\Users\Petya\Desktop\SoftUniPavel\ProductShop-Judge\ProductShop\Datasets";

        public static void Main()
        {
            using (var context = new ProductShopContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                //Seed(context);

                var result = GetUsersWithProducts(context);
                System.Console.WriteLine(result);
            }
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var values = context.Users
                 .Where(u => u.ProductsSold.Any())
                 .Select(x => new
                 {
                     firstName = x.FirstName,
                     lastName = x.LastName,
                     age = x.Age,
                     soldProducts = new
                     {
                         count = x.ProductsSold.Count,
                         products = x.ProductsSold
                         .Select(sp => new
                         {
                             sp.Name,
                             sp.Price
                         })
                         .ToList()
                     }
                 })
                 .OrderByDescending(o => o.soldProducts.count)
                 .ToList();

            var obj = new
            {
                usersCount = values.Count,
                users = values
            };

            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, 
                Formatting = Formatting.Indented
            });
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var values = context.Categories
                    .Select(x => new
                    {
                        x.Name,
                        Prices = x.CategoryProducts.Select(cp => cp.Product.Price).ToList()
                    })
                    .ToList()
                    .Select(x => new
                    {
                        category = x.Name,
                        productsCount = x.Prices.Count,
                        averagePrice = (x.Prices.Sum() / x.Prices.Count).ToString("F2"),
                        totalRevenue = (x.Prices.Sum()).ToString("F2")
                    })
                    .OrderByDescending(x => x.productsCount)
                    .ToList();

            return JsonConvert.SerializeObject(values, Formatting.Indented);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var values = context.Users
                .Where(x => x.ProductsSold.Any(sp => sp.Buyer != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    soldProducts = x.ProductsSold
                         .Where(p => p.Buyer != null)
                         .Select(p => new
                         {
                             name = p.Name,
                             price = p.Price,
                             buyerFirstName = p.Buyer.FirstName,
                             buyerLastName = p.Buyer.LastName,
                         })
                         .ToList()
                })
                .OrderBy(x => x.lastName)
                .ThenBy(x => x.firstName)
                .ToList();

            return JsonConvert.SerializeObject(values, Formatting.Indented);
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var startRange = 500;
            var endRange = 1000;

            var values = context.Products
                 .Where(x => x.Price >= startRange && x.Price <= endRange)
                 .OrderBy(x => x.Price)
                 .Select(x => new
                 {
                     name = x.Name,
                     price = x.Price,
                     seller = $"{x.Seller.FirstName} {x.Seller.LastName}"
                 })
                 .ToArray();

            var json = JsonConvert.SerializeObject(values, Formatting.Indented);

            return json;
        }

        #region Seeding 

        public static void Seed(ProductShopContext context)
        {
            string json;
            string result;

            json = File.ReadAllText(path + @"\users.json");
            result = ImportUsers(context, json);

            json = File.ReadAllText(path + @"\products.json");
            result = ImportProducts(context, json);

            json = File.ReadAllText(path + @"\categories.json");
            result = ImportCategories(context, json);

            json = File.ReadAllText(path + @"\categories-products.json");
            result = ImportCategoryProducts(context, json);
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);

            var seededCount = context.SaveChanges();

            return $"Successfully imported {seededCount}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(x => x.Name != null)
                .ToArray();

            context.Categories.AddRange(categories);

            var seededCategories = context.SaveChanges();

            return $"Successfully imported {seededCategories}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);

            var seededProducts = context.SaveChanges();

            return $"Successfully imported {seededProducts}";
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);

            var seededUsers = context.SaveChanges();

            return $"Successfully imported {seededUsers}";
        }

        #endregion
    }
}