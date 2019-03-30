namespace PetClinic.DataProcessor
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Json = Newtonsoft.Json;

    using PetClinic.Data;
    using PetClinic.Dto.ExportDtos;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var sb = new StringBuilder();

            var animals = context.Animals
                  .Where(x => x.Passport.OwnerPhoneNumber == phoneNumber)
                  .Select(x => new AnimalByOwnerDto()
                  {
                      OwnerName = x.Passport.OwnerName,
                      AnimalName = x.Name,
                      Age = x.Age,
                      SerialNumber = x.PassportSerialNumber,
                      RegisteredOn = x.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)

                  })
                  .OrderBy(x => x.Age)
                  .ThenBy(x => x.SerialNumber)
                  .ToList();

            var animalsToString = Json.JsonConvert.SerializeObject(animals, Json.Formatting.Indented);

            return animalsToString;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedures = context.Procedures
                .OrderBy(x => x.DateTime)
                .ThenBy(x => x.Animal.PassportSerialNumber)
                .Select(x => new ExportProceduresDto()
                {
                    Passport = x.Animal.PassportSerialNumber,
                    OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                    DateTime = x.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = x.ProcedureAnimalAids
                    .Select(pa => new ExportAidsDto
                    {
                        Name = pa.AnimalAid.Name,
                        Price = pa.AnimalAid.Price
                    })
                    .ToArray(),
                    TotalPrice = x.ProcedureAnimalAids.Select(pa => pa.AnimalAid.Price).Sum()
                })
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ExportProceduresDto[]), new XmlRootAttribute("Procedures"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), procedures, namespaces);

            return sb.ToString(); 
        }
    }
}