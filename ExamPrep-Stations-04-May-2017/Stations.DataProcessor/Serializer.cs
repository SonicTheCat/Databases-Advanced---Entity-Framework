namespace Stations.DataProcessor
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Xml;
    using System.IO;

    using Json = Newtonsoft.Json;

    using Stations.Data;
    using Stations.Models.Enums;
    using Stations.DataProcessor.Dto.ExportDtos;

    public class Serializer
    {
        public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
        {
            var date = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", null);

            var values = context.Trips
                .Where(x => x.Status == TripStatus.Delayed && x.DepartureTime <= date)
                .GroupBy(x => x.Train.TrainNumber)
                .Select(x => new
                {
                    TrainNumber = x.Key,
                    DelayedTimes = x
                            .Where(t => t.Status == TripStatus.Delayed && t.DepartureTime <= date)
                            .Count(),
                    MaxDelayedTime = x.
                            Where(t => t.Status == TripStatus.Delayed && t.DepartureTime <= date)
                            .Select(t => t.TimeDifference)
                            .OrderByDescending(t => t)
                            .Select(t => t.ToString())
                            .ToArray()[0]
                })
                .OrderByDescending(x => x.DelayedTimes)
                .ThenByDescending(x => x.MaxDelayedTime)
                .ThenBy(x => x.TrainNumber)
                .ToArray();

            var json = Json.JsonConvert.SerializeObject(values, Json.Formatting.Indented);

            return json;
        }

        public static string ExportCardsTicket(StationsDbContext context, string cardType)
        {
            var typeToEnum = Enum.Parse<CardType>(cardType);

            var values = context.Tickets
                .Where(x => x.CustomerCard.Type == typeToEnum)
                .GroupBy(x => x.CustomerCard.Name)
                .Select(x => new CardDto()
                {
                    Name = x.Key,
                    Type = typeToEnum,
                    Tickets = x.Select(gr => new TicketDto()
                    {
                        OriginStation = gr.Trip.OriginStation.Name,
                        DestinationStation = gr.Trip.DestinationStation.Name,
                        DepartureTime = gr.Trip.DepartureTime.ToString("dd/MM/yyyy HH:mm")
                    })
                    .ToArray()
                })
                .OrderBy(x => x.Name)
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), values, namespaces);

            return sb.ToString();
        }
    }
}