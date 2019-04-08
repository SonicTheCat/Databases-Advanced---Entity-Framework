namespace Cinema.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies
                 .Where(x => x.Rating >= rating && x.Projections.Any(p => p.Tickets.Count > 0))
                 .OrderByDescending(x => x.Rating)
                 .ThenByDescending(x => x.Projections.Sum(p => p.Tickets.Sum(t => t.Price)))
                 .Select(x => new
                 {
                     MovieName = x.Title,
                     Rating = x.Rating.ToString("F2"),
                     TotalIncomes = x.Projections.Sum(p => p.Tickets.Sum(t => t.Price)).ToString("F2"),
                     Customers = x.Projections
                        .SelectMany(p => p.Tickets).Select(t => new
                        {
                            t.Customer.FirstName,
                            t.Customer.LastName,
                            Balance = t.Customer.Balance.ToString("F2")
                        })
                        .OrderByDescending(c => c.Balance)
                        .ThenBy(c => c.FirstName)
                        .ThenBy(c => c.LastName)
                        .ToArray()
                 })
                 .Take(10)
                 .ToArray();

            var json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context.Customers
                .Where(x => x.Age >= age)
                .OrderByDescending(x => x.Tickets.Select(t => t.Price).Sum())
                .Select(x => new CustomerDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SpentMoney = (x.Tickets.Select(t => t.Price).Sum()).ToString("F2"),
                    SpentTime = new TimeSpan(x.Tickets.Select(t => t.Projection.Movie.Duration).Sum(span => span.Ticks)).ToString(@"hh\:mm\:ss")
                })
                .Take(10)
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), customers, namespaces);

            return sb.ToString();
        }
    }
}