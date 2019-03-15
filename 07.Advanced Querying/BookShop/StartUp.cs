namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                // var input = Console.ReadLine();
                var result = RemoveBooks(db);

                Console.WriteLine(result);
            }
        }

        #region Task 15 Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
               .Where(b => b.Copies < 4200)
               .ToList();

            context.RemoveRange(books);
            context.SaveChanges();

            return books.Count;
        }
        #endregion

        #region Task 14 Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList()
                .ForEach(b => b.Price += 5);

            context.SaveChanges();
        }
        #endregion

        #region Task 13 Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                   .Select(x => new
                   {
                       x.Name,
                       Books = x.CategoryBooks
                           .Select(cb => new
                           {
                               cb.Book.ReleaseDate,
                               cb.Book.Title
                           })
                           .OrderByDescending(b => b.ReleaseDate)
                           .Take(3)
                           .ToList()
                   })
                   .OrderBy(x => x.Name)
                   .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var category in categories)
            {
                sb.AppendLine("--" + category.Name);
                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().Trim();
        }
        #endregion

        #region Task 12 Profit by Category 
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Categories
                    .Select(c => new
                    {
                        c.Name,
                        Profit = c.CategoryBooks
                                  .Select(cb => cb.Book.Price * cb.Book.Copies)
                                  .Sum()
                    })
                    .OrderByDescending(x => x.Profit)
                    .ThenBy(x => x.Name)
                    .ToList()
                    .ForEach(x => sb.AppendLine(x.Name + " $" + x.Profit));

            return sb.ToString().Trim();
        }

        #endregion

        #region Task 11 Total Book Copies 
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Authors
                .Select(a => new
                {
                    a.AuthorId,
                    FullName = a.FirstName + " " + a.LastName,
                    Copies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(x => x.Copies)
                .ToList()
                .ForEach(x => sb.AppendLine(x.FullName + " - " + x.Copies));

            return sb.ToString().Trim();
        }
        #endregion

        #region Task 10 Count Books 
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books
                .Select(x => x.Title)
                .Where(x => x.Length > lengthCheck)
                .Count();
        }
        #endregion

        #region Task 9 Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                 .Select(x => new
                 {
                     x.BookId,
                     x.Title,
                     x.Author
                 })
                 .Where(x => EF.Functions.Like(x.Author.LastName, input + "%"))
                 .OrderBy(x => x.BookId)
                 .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.Author.FirstName} {book.Author.LastName})");
            }

            return sb.ToString().Trim();
        }
        #endregion

        #region Task 8 Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            return string.Join(Environment.NewLine, context.Books
                .Where(b => EF.Functions.Like(b.Title, $"%{input}%"))
                .Select(b => b.Title)
                .OrderBy(x => x));
        }
        #endregion

        #region Task 7 Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            return string.Join(Environment.NewLine, context.Authors
                         .Where(a => EF.Functions.Like(a.FirstName, "%" + input))
                         .Select(a => a.FirstName + " " + a.LastName)
                         .OrderBy(a => a)
                         .ToList());
        }
        #endregion

        #region Task 6 Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime convertTodate = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            return string.Join(Environment.NewLine,
                    context.Books
                     .Where(x => x.ReleaseDate < convertTodate)
                     .OrderByDescending(x => x.ReleaseDate)
                     .Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:F2}")
                     .ToList());
        }
        #endregion

        #region Task 5 Book Titles by Category 
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var tokens = input
                .ToLower()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            return string.Join(Environment.NewLine, context.Categories
                .Where(c => tokens.Contains(c.Name.ToLower()))
                .SelectMany(c => c.CategoryBooks
                    .Select(cb => cb.Book.Title))
                .OrderBy(b => b)
                .ToList());
        }
        #endregion

        #region Task 4 Not Released In 
        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            return string.Join(Environment.NewLine, context.Books
                   .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year != year)
                   .OrderBy(b => b.BookId)
                   .Select(b => b.Title)
                   .ToList());
        }
        #endregion

        #region Task 3 Books by Price 
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList()
                .ForEach(b => sb.AppendLine($"{b.Title} - ${b.Price:f2}"));

            return sb.ToString().TrimEnd();
        }
        #endregion

        #region Task 2 Golden Books 
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList()
                .ForEach(b => sb.AppendLine(b));

            return sb.ToString().Trim();
        }
        #endregion

        #region Task 1 Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var commandToEnum = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command, true);

            StringBuilder sb = new StringBuilder();

            context.Books
                .Where(b => b.AgeRestriction == commandToEnum)
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList()
                .ForEach(b => sb.AppendLine(b));

            return sb.ToString().Trim();
        }
        #endregion
    }
}