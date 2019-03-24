namespace ProductsShop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using ProductsShop.Data;
    using ProductsShop.Models;

    public class Startup
    {
        public static void Main()
        {
            using (var context = new ProductsShopContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.Migrate();
                //Seed(context);

                UsersAndProducts(context);
            }
        }

        #region Query and Export Data

        #region Users and Products
        public static void UsersAndProducts(ProductsShopContext context)
        {
            var values = context.Users
                 .Where(u => u.SellingProducts.Any())
                 .Select(x => new
                 {
                     x.FirstName,
                     x.LastName,
                     x.Age,
                     SoldProducts = new
                     {
                         x.SellingProducts.Count,
                         Products = x.SellingProducts
                         .Select(sp => new
                         {
                             sp.Name,
                             sp.Price
                         })
                         .ToList()
                     }
                 })
                 .OrderByDescending(o => o.SoldProducts.Count)
                 .ThenBy(o => o.LastName)
                 .ToList();

            var obj = new
            {
                UsersCount = values.Count,
                Users = values
            };

            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);

            File.WriteAllText(Paths.result + "/usersAndProducts.json", json);
        }
        #endregion

        #region Categories By Products Count
        public static void CategoriesByProducts(ProductsShopContext context)
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
                     x.Name,
                     x.Prices.Count,
                     Average = Math.Round(x.Prices.Sum() / x.Prices.Count, 2),
                     TotalRevenue = Math.Round(x.Prices.Sum(), 2)
                 })
                 .OrderByDescending(x => x.Count)
                 .ToList();

            var json = JsonConvert.SerializeObject(values, Formatting.Indented);

            File.WriteAllText(Paths.result + "/categoriesByProducts.json", json);
        }
        #endregion

        #region Successfully Sold Products
        public static void SoldProducts(ProductsShopContext context)
        {
            var values = context.Users
                .Where(x => x.SellingProducts.Any(sp => sp.Buyer != null))
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    SoldProducts = x.SellingProducts
                         .Where(p => p.Buyer != null)
                         .Select(p => new
                         {
                             p.Name,
                             p.Price,
                             p.Buyer.FirstName,
                             p.Buyer.LastName,
                             p.BuyerId
                         })
                         .ToList()
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToList();

            var json = JsonConvert.SerializeObject(values, Formatting.Indented);

            File.WriteAllText(Paths.result + "/soldProducts.json", json);
        }
        #endregion

        #region Products In Range
        public static void ProductsInRange()
        {
            var startRange = 500;
            var endRange = 1000;

            using (var context = new ProductsShopContext())
            {
                var values = context.Products
                     .Where(x => x.Price >= startRange && x.Price <= endRange)
                     .OrderBy(x => x.Price)
                     .Select(x => new
                     {
                         x.Name,
                         x.Price,
                         FullNameSeller = x.Seller.FirstName + " " + x.Seller.LastName
                     })
                     .ToArray();

                var json = JsonConvert.SerializeObject(values, Formatting.Indented);

                File.WriteAllText(Paths.result + "/productsInrange.json", json);
            }
        }
        #endregion

        #endregion

        #region SeedAllData

        public static void Seed(ProductsShopContext context)
        {
            SeedUsers(context);
            SeedProducts(context);
            SeedCategories(context);
            SeedCategoryProducts(context);
        }

        #region SeedUsers
        public static void SeedUsers(ProductsShopContext context)
        {
            var json = File.ReadAllText(Paths.path + Paths.users);
            var users = JsonConvert.DeserializeObject<User[]>(json);

            context.Users.AddRange(users);

            context.SaveChanges();
        }
        #endregion

        #region SeedProducts
        public static void SeedProducts(ProductsShopContext context)
        {
            var json = File.ReadAllText(Paths.path + Paths.products);
            var products = JsonConvert.DeserializeObject<Product[]>(json);

            var random = new Random();
            var usersCount = context.Users.Count();

            for (int i = 0; i < products.Length; i++)
            {
                int sellerId;
                int? buyerId;

                do
                {
                    sellerId = random.Next(1, usersCount);
                    buyerId = random.Next(1, usersCount);

                } while (sellerId == buyerId);

                if (i % 5 == 0)
                {
                    buyerId = null;
                }

                products[i].BuyerId = buyerId;
                products[i].SellerId = sellerId;
            }

            context.Products.AddRange(products);

            context.SaveChanges();
        }
        #endregion

        #region SeedCategories
        public static void SeedCategories(ProductsShopContext context)
        {
            var json = File.ReadAllText(Paths.path + Paths.categories);
            var categories = JsonConvert.DeserializeObject<Category[]>(json);

            context.Categories.AddRange(categories);

            context.SaveChanges();
        }
        #endregion

        #region SeedCategoryProdicts
        public static void SeedCategoryProducts(ProductsShopContext context)
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
        #endregion

        #endregion
    }
}