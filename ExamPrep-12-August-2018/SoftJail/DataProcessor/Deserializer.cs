namespace SoftJail.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using Newtonsoft.Json;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;

    public class Deserializer
    {
        private const string InvalidData = "Invalid Data";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentsDto = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var departments = new List<Department>();

            foreach (var dto in departmentsDto)
            {
                if (!IsValid(dto) || !dto.Cells.All(x => IsValid(x)))
                {
                    sb.AppendLine(InvalidData);
                    continue;
                }

                var cells = new List<Cell>();

                foreach (var cellDto in dto.Cells)
                {
                    var cell = new Cell()
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow
                    };

                    cells.Add(cell);
                }

                var department = new Department()
                {
                    Name = dto.Name,
                    Cells = cells
                };

                departments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {cells.Count} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonersDto = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var prisoners = new List<Prisoner>();

            foreach (var dto in prisonersDto)
            {
                if (!IsValid(dto) || !dto.Mails.All(x => IsValid(x)))
                {
                    sb.AppendLine(InvalidData);
                    continue;
                }

                var mails = new List<Mail>();

                foreach (var mailDto in dto.Mails)
                {
                    var mail = new Mail()
                    {
                        Description = mailDto.Description,
                        Sender = mailDto.Sender,
                        Address = mailDto.Address
                    };

                    mails.Add(mail);
                }

                var incarcerationDate = DateTime.ParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime? releaseDate = null;

                if (dto.ReleaseDate != null)
                {
                    releaseDate = DateTime.ParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                var cell = context.Cells.Find(dto.CellId);

                var prisoner = new Prisoner()
                {
                    FullName = dto.FullName,
                    Nickname = dto.Nickname,
                    Age = dto.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    Bail = dto.Bail,
                    Cell = cell,
                    Mails = mails
                };

                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var officersDto = (OfficerDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var officers = new List<Officer>();

            foreach (var dto in officersDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(InvalidData);
                    continue;
                }

                var department = context.Departments.Find(dto.DepartmentId);
                var isValidPosition = Enum.TryParse<Position>(dto.Position, out Position position);
                var isValidWeapon = Enum.TryParse<Weapon>(dto.Weapon, out Weapon weapon);

                if (!isValidPosition || !isValidWeapon)
                {
                    sb.AppendLine(InvalidData);
                    continue;
                }

                var officer = new Officer()
                {
                    FullName = dto.FullName,
                    Salary = dto.Salary,
                    Position = position,
                    Weapon = weapon,
                    Department = department,
                };

                foreach (var prisonerDto in dto.Prisoners)
                {
                    var prisoner = context.Prisoners.Find(prisonerDto.Id);

                    officer.OfficerPrisoners.Add(new OfficerPrisoner()
                    {
                        Prisoner = prisoner
                    });
                }

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(officers);
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