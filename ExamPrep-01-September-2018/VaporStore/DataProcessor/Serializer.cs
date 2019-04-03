namespace VaporStore.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Json = Newtonsoft.Json;

    using Data;
    using Data.Enums;
    using Dtos.ExportDtos;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = context.Genres
                .Where(x => genreNames.Contains(x.Name))
                .Select(x => new
                {
                    x.Id,
                    Genre = x.Name,
                    Games = x.Games
                    .Where(g => g.Purchases.Any())
                    .Select(g => new
                    {
                        g.Id,
                        Title = g.Name,
                        Developer = g.Developer.Name,
                        Tags = string.Join(", ", g.GameTags.Select(gt => gt.Tag.Name).ToArray()),
                        Players = g.Purchases.Count
                    })
                    .OrderByDescending(g => g.Players)
                    .ThenBy(g => g.Id)
                    .ToArray(),
                    TotalPlayers = x.Games.Select(g => g.Purchases.Count).Sum()
                })
                .OrderByDescending(x => x.TotalPlayers)
                .ThenBy(x => x.Id)
                .ToArray();

            var json = Json.JsonConvert.SerializeObject(genres, Json.Formatting.Indented);

            return json;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var type = Enum.Parse<PurchaseType>(storeType);

            var users = context.Purchases
                .Where(x => x.Type == type && x.Card.Purchases.Any())
                .GroupBy(x => x.Card.User.Username)
                .Select(x => new UserDto()
                {
                    Username = x.Key,
                    Purchases = x.Select(p => new PurchaseDto()
                    {
                        Card = p.Card.Number,
                        Cvc = p.Card.Cvc,
                        Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        Game = new GameDto()
                        {
                            Title = p.Game.Name,
                            Genre = p.Game.Genre.Name,
                            Price = p.Game.Price
                        }
                    })
                    .OrderBy(p => p.Date)
                    .ToArray(),
                    TotalSpent = x.Select(p => p.Game.Price).Sum()
                })
                .OrderByDescending(x => x.TotalSpent)
                .ThenBy(x => x.Username)
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("Users"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString();
        }
    }
}