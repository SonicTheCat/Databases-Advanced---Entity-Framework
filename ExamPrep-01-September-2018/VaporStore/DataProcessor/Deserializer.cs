namespace VaporStore.DataProcessor
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

    using Data;
    using Data.Models;
    using Dtos.ImportDtos;

    public static class Deserializer
    {
        private const string InvalidData = "Invalid Data";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var gameDtos = JsonConvert.DeserializeObject<GameDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();

            var games = new List<Game>();

            var existingTags = context.Tags.ToList();
            var existingGenres = context.Genres.ToList();
            var existingDevs = context.Developers.ToList();

            foreach (var dto in gameDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(InvalidData);
                    continue;
                }

                var dev = GetDev(existingDevs, dto);
                var genre = GetGenre(existingGenres, dto);
                var tags = GetTags(existingTags, dto);

                var game = new Game()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    ReleaseDate = DateTime.ParseExact(dto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Developer = dev,
                    Genre = genre,
                };

                foreach (var tag in tags)
                {
                    var gameTag = new GameTag() { Tag = tag };
                    game.GameTags.Add(gameTag); 
                }

                games.Add(game);
                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {tags.Count} tags"); 
            }

            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString().TrimEnd(); 
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var usersDtos = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            var users = new List<User>(); 

            foreach (var dto in usersDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(InvalidData);
                    continue;
                }

                if (dto.Cards.Any(x => !IsValid(x)))
                {
                    sb.AppendLine(InvalidData);
                    continue;
                }

                var cards = new List<Card>();
                foreach (var cardDto in dto.Cards)
                {
                    var card = new Card()
                    {
                        Number = cardDto.Number,
                        Cvc = cardDto.Cvc,
                        Type = cardDto.Type
                    };

                    cards.Add(card); 
                }

                var user = new User()
                {
                    FullName = dto.FullName,
                    Username = dto.Username,
                    Email = dto.Email,
                    Age = dto.Age,
                    Cards = cards
                };

                users.Add(user);
                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards"); 
            }

            context.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd(); 
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var purchasesDtos = (PurchaseDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            var purchases = new List<Purchase>();

            foreach (var dto in purchasesDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(InvalidData);
                    continue;
                }

                var card = context.Cards.FirstOrDefault(x => x.Number == dto.Card);
                var game = context.Games.FirstOrDefault(x => x.Name == dto.Game);
                var date = DateTime.ParseExact(dto.Date, "dd/MM/yyyy HH:mm", null);

                var purchase = new Purchase()
                {
                    Type = dto.Type,
                    ProductKey = dto.ProductKey,
                    Card = card,
                    Game = game,
                    Date = date
                };

                purchases.Add(purchase);
                sb.AppendLine($"Imported {game.Name} for {card.User.Username}"); 
            }

            context.Purchases.AddRange(purchases);
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

        private static List<Tag> GetTags(List<Tag> existingTags, GameDto dto)
        {
            var tags = new List<Tag>();
            foreach (var tagName in dto.Tags)
            {
                var tag = existingTags.FirstOrDefault(x => x.Name == tagName);
                if (tag == null)
                {
                    tag = new Tag() { Name = tagName };
                    existingTags.Add(tag);
                }
                tags.Add(tag);
            }

            return tags;
        }

        private static Genre GetGenre(List<Genre> existingGenres, GameDto dto)
        {
            var genre = existingGenres.FirstOrDefault(x => x.Name == dto.Genre);
            if (genre == null)
            {
                genre = new Genre() { Name = dto.Genre };
                existingGenres.Add(genre);
            }

            return genre;
        }

        private static Developer GetDev(List<Developer> existingDevs, GameDto dto)
        {
            var dev = existingDevs.FirstOrDefault(x => x.Name == dto.Developer);
            if (dev == null)
            {
                dev = new Developer() { Name = dto.Developer };
                existingDevs.Add(dev);
            }

            return dev;
        }
    }
}