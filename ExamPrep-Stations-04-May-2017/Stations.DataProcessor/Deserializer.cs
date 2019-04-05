namespace Stations.DataProcessor
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

    using Newtonsoft.Json;

    using Stations.Data;
    using Stations.DataProcessor.Dto.ImportDtos;
    using Stations.Models;
    using Stations.Models.Enums;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportStations(StationsDbContext context, string jsonString)
        {
            var stationsDto = JsonConvert.DeserializeObject<StationDto[]>(jsonString);

            var sb = new StringBuilder();

            var existingStations = context.Stations.Select(x => x.Name).ToList();
            var stations = new List<Station>();

            foreach (var dto in stationsDto)
            {
                if (dto.Town == null)
                {
                    dto.Town = dto.Name;
                }

                if (existingStations.Contains(dto.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                existingStations.Add(dto.Name);

                var station = new Station()
                {
                    Name = dto.Name,
                    Town = dto.Town
                };

                stations.Add(station);
                sb.AppendLine(string.Format(SuccessMessage, station.Name));
            }

            context.Stations.AddRange(stations);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportClasses(StationsDbContext context, string jsonString)
        {
            var classesDto = JsonConvert.DeserializeObject<ClassDto[]>(jsonString);

            var sb = new StringBuilder();

            var existingNames = context.SeatingClasses.Select(x => x.Name).ToList();
            var existingAbb = context.SeatingClasses.Select(x => x.Abbreviation).ToList();

            var classes = new List<SeatingClass>();

            foreach (var dto in classesDto)
            {
                if (existingNames.Contains(dto.Name) || existingAbb.Contains(dto.Abbreviation))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                existingAbb.Add(dto.Abbreviation);
                existingNames.Add(dto.Name);

                var @class = new SeatingClass()
                {
                    Name = dto.Name,
                    Abbreviation = dto.Abbreviation
                };

                classes.Add(@class);
                sb.AppendLine(string.Format(SuccessMessage, @class.Name));
            }

            context.SeatingClasses.AddRange(classes);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportTrains(StationsDbContext context, string jsonString)
        {
            var trainsDto = JsonConvert.DeserializeObject<TrainDto[]>(jsonString);

            var sb = new StringBuilder();

            var existingClassNames = context.SeatingClasses.Select(x => x.Name).ToList();
            var existingClassAbbs = context.SeatingClasses.Select(x => x.Abbreviation).ToList();
            var existingTrainNumbers = context.Trains.Select(x => x.TrainNumber).ToList();

            var trains = new List<Train>();

            foreach (var dto in trainsDto)
            {
                var flag = true;
                foreach (var seat in dto.Seats)
                {
                    if (!existingClassAbbs.Contains(seat.Abbreviation) || !existingClassNames.Contains(seat.Name))
                    {
                        flag = false;
                        break;
                    }
                }

                if (!flag)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!IsValid(dto) || !dto.Seats.All(x => IsValid(x)))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (existingTrainNumbers.Contains(dto.TrainNumber))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                existingTrainNumbers.Add(dto.TrainNumber);

                var train = new Train()
                {
                    TrainNumber = dto.TrainNumber,
                    Type = dto.Type == null ? TrainType.HighSpeed : dto.Type
                };

                foreach (var seat in dto.Seats)
                {
                    var @class = context
                        .SeatingClasses
                        .First(x => x.Name == seat.Name && x.Abbreviation == seat.Abbreviation);

                    var trainSeat = new TrainSeat()
                    {
                        SeatingClass = @class,
                        Quantity = seat.Quantity ?? 0
                    };

                    train.TrainSeats.Add(trainSeat);
                }

                trains.Add(train);
                sb.AppendLine(string.Format(SuccessMessage, train.TrainNumber));
            }

            context.Trains.AddRange(trains);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            var tripsDto = JsonConvert.DeserializeObject<TripDto[]>(jsonString);

            var sb = new StringBuilder();

            var existingTrains = context.Trains.Select(x => x.TrainNumber).ToList();
            var existingStations = context.Stations.Select(x => x.Name).ToList();

            var trips = new List<Trip>();

            foreach (var dto in tripsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!existingStations.Contains(dto.OriginStation)
                    || !existingStations.Contains(dto.DestinationStation)
                    || !existingTrains.Contains(dto.Train))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var train = context.Trains.First(x => x.TrainNumber == dto.Train);
                var originStation = context.Stations.First(x => x.Name == dto.OriginStation);
                var destinationStation = context.Stations.First(x => x.Name == dto.DestinationStation);
                var departTime = DateTime.ParseExact(dto.DepartureTime, "dd/MM/yyyy HH:mm", null);
                var arrivalTime = DateTime.ParseExact(dto.ArrivalTime, "dd/MM/yyyy HH:mm", null);
                var status = dto.Status ?? TripStatus.OnTime;
                TimeSpan? diffrence = null;

                if (dto.TimeDifference != null)
                {
                    diffrence = TimeSpan.ParseExact(dto.TimeDifference, @"hh\:mm", null);
                }


                if (departTime >= arrivalTime)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var trip = new Trip()
                {
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destinationStation,
                    DepartureTime = departTime,
                    ArrivalTime = arrivalTime,
                    TimeDifference = diffrence,
                    Status = status
                };

                trips.Add(trip);
                sb.AppendLine($"Trip from {originStation.Name} to {destinationStation.Name} imported.");
            }

            context.Trips.AddRange(trips);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var cardsDto = (CardDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            var cards = new List<CustomerCard>();

            foreach (var dto in cardsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var cardType = dto.Type ?? CardType.Normal;

                var card = new CustomerCard()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Type = cardType
                };

                cards.Add(card);
                sb.AppendLine(string.Format(SuccessMessage, card.Name));
            }

            context.Cards.AddRange(cards);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(TicketDto[]), new XmlRootAttribute("Tickets"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var ticketsDto = (TicketDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var tickets = new List<Ticket>();

            foreach (var dto in ticketsDto)
            {
                if (!IsValid(dto) || !IsValid(dto.Trip))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var date = DateTime.ParseExact(dto.Trip.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                var trip = context
                    .Trips
                    .SingleOrDefault(x => x.OriginStation.Name == dto.Trip.OriginStation && x.DestinationStation.Name == dto.Trip.DestinationStation && x.DepartureTime == date);

                if (dto.Card == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var card = context.Cards.FirstOrDefault(x => x.Name == dto.Card.Name);

                if (trip == null || card == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var abbreviation = string.Join("", dto.SeatingPlace.Take(2));
                var seatingNumber = int.Parse(string.Join("", dto.SeatingPlace.Skip(2)));

                var @class = context.SeatingClasses.FirstOrDefault(x => x.Abbreviation == abbreviation);
                var trainSeat = context.TrainSeats.FirstOrDefault(x => x.SeatingClassId == @class.Id);

                if (@class == null || trainSeat.Quantity < seatingNumber)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var ticket = new Ticket()
                {
                    SeatingPlace = dto.SeatingPlace,
                    Price = dto.Price,
                    Trip = trip,
                    CustomerCard = card
                };

                tickets.Add(ticket);
                sb.AppendLine($"Ticket from {ticket.Trip.OriginStation.Name} to {ticket.Trip.DestinationStation.Name} departing at {date.ToString("dd/MM/yyyy HH:mm")} imported.");
            }

            context.Tickets.AddRange(tickets);
            context.SaveChanges();

            return sb.ToString().TrimEnd(); 
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