namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var moviesDto = JsonConvert.DeserializeObject<MovieDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var movies = new List<Movie>();

            var existingTitles = context.Movies.Select(x => x.Title).ToList();

            foreach (var dto in moviesDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (existingTitles.Contains(dto.Title))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                existingTitles.Add(dto.Title);

                var genreType = Enum.TryParse<Genre>(dto.Genre, out Genre genre);

                if (!genreType)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var duration = TimeSpan.Parse(dto.Duration);

                var movie = new Movie()
                {
                    Title = dto.Title,
                    Genre = genre,
                    Duration = duration,
                    Rating = dto.Rating,
                    Director = dto.Director
                };

                movies.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("F2")));
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallsDto = JsonConvert.DeserializeObject<HallDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var halls = new List<Hall>();

            foreach (var dto in hallsDto)
            {
                if (!IsValid(dto) || dto.Seats <= 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = new Hall()
                {
                    Name = dto.Name,
                    Is4Dx = dto.Is4Dx,
                    Is3D = dto.Is3D
                };

                for (int i = 0; i < dto.Seats; i++)
                {
                    hall.Seats.Add(new Seat());
                }

                var type = "Normal";

                if (hall.Is4Dx)
                {
                    type = "4Dx";
                }

                if (hall.Is3D)
                {
                    type = "3D";
                }

                if (hall.Is4Dx && hall.Is3D)
                {
                    type = "4Dx" + "/3D";
                }

                halls.Add(hall);
                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, type, hall.Seats.Count));
            }

            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProjectionDto[]), new XmlRootAttribute("Projections"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var projectionsDto = (ProjectionDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var projections = new List<Projection>();

            var existingMovies = context.Movies.Select(x => x.Id).ToArray();
            var existingHalls = context.Halls.Select(x => x.Id).ToArray();

            foreach (var dto in projectionsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var date = DateTime.Parse(dto.DateTime);

                if (!existingHalls.Contains(dto.HallId) || !existingMovies.Contains(dto.MovieId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection()
                {
                    MovieId = dto.MovieId,
                    HallId = dto.HallId,
                    DateTime = date
                };

                var movie = context.Movies.Find(projection.MovieId);

                projections.Add(projection);
                sb.AppendLine(string.Format(SuccessfulImportProjection, movie.Title, projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var customersDto = (CustomerDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var customers = new List<Customer>();

            foreach (var dto in customersDto)
            {
                if (!IsValid(dto) || !dto.Tickets.All(x => IsValid(x)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer()
                {
                    FirstName = dto.FirstName, 
                    LastName = dto.LastName, 
                    Age= dto.Age, 
                    Balance = dto.Balance
                };

                foreach (var dtoTicket in dto.Tickets)
                {
                    customer.Tickets.Add(new Ticket()
                    {
                        Price = dtoTicket.Price,
                        ProjectionId = dtoTicket.ProjectionId
                    }); 
                }

                customers.Add(customer);

                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count)); 
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString(); 
        }

        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, context, results, true);

            return isValid;
        }
    }
}