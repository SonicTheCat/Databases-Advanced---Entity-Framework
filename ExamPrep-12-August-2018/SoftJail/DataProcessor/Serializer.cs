namespace SoftJail.DataProcessor
{
    using System.Text;
    using System.Xml.Serialization;
    using System.Xml;
    using System.IO;
    using System;
    using System.Linq;

    using Json = Newtonsoft.Json;

    using Data;
    using ExportDto;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Where(x => ids.Contains(x.Id))
                .Select(x => new
                {
                    x.Id,
                    Name = x.FullName,
                    x.Cell.CellNumber,
                    Officers = x.PrisonerOfficers
                    .Select(po => new
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name
                    })
                    .OrderBy(o => o.OfficerName)
                    .ToArray(),
                    TotalOfficerSalary = Math.Round(x.PrisonerOfficers.Sum(po => po.Officer.Salary), 2)
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();

            var json = Json.JsonConvert.SerializeObject(prisoners, Json.Formatting.Indented);

            return json;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisoners = context.Prisoners
                 .Where(x => prisonersNames.Contains(x.FullName))
                 .Select(x => new PrisonerDto()
                 {
                     Id = x.Id,
                     Name = x.FullName,
                     IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd"),
                     Messages = x.Mails
                     .Select(m => new MessageDto()
                     {
                         Description = ReverseDescription(m.Description)
                     })
                     .ToArray()
                 })
                 .OrderBy(x => x.Name)
                 .ThenBy(x => x.Id)
                 .ToArray();
            
            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(PrisonerDto[]), new XmlRootAttribute("Prisoners"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), prisoners, namespaces);

            return sb.ToString();
        }

        private static string ReverseDescription(string value)
        {
            char[] arr = value.ToCharArray();
            Array.Reverse(arr);

            return new string(arr); 
        }
    }
}