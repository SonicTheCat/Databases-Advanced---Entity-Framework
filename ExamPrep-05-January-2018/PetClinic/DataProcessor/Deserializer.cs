namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using Annotations = System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.Models;
    using PetClinic.Dto.ImportDtos;
    using System.Xml.Serialization;
    using System.Xml;
    using System.IO;
    using System.Linq;

    public class Deserializer
    {
        private const string errorMessage = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var aidDtos = JsonConvert.DeserializeObject<AnimalAidsDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            var animalAids = new HashSet<AnimalAid>();

            foreach (var dto in aidDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                var animalAid = new AnimalAid()
                {
                    Name = dto.Name,
                    Price = dto.Price
                }; 

                if (animalAids.Contains(animalAid))
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                animalAids.Add(animalAid);
                sb.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var animalDtos = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            var animals = new HashSet<Animal>();

            foreach (var dto in animalDtos)
            {
                if (!IsValid(dto) || !IsValid(dto.Passport))
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                var animal = new Animal()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Type = dto.Type,
                    Passport = new Passport()
                    {
                        SerialNumber = dto.Passport.SerialNumber,
                        OwnerName = dto.Passport.OwnerName,
                        OwnerPhoneNumber = dto.Passport.OwnerPhoneNumber,
                        RegistrationDate = DateTime.ParseExact(dto.Passport.RegistrationDate, "dd-MM-yyyy", null)
                    }
                }; 

                if (animals.Contains(animal))
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                animals.Add(animal);
                sb.AppendLine($"Record {animal.Name} Passport №: {animal.Passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animals);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var vetsDto = (VetDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            var vets = new HashSet<Vet>();

            foreach (var dto in vetsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                var vet = new Vet()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Profession = dto.Profession,
                    PhoneNumber = dto.PhoneNumber
                }; 

                if (vets.Contains(vet))
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                vets.Add(vet);
                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var proceduresDtos = (ProcedureDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            var procedures = new HashSet<Procedure>();

            foreach (var dto in proceduresDtos)
            {
                var animal = context.Animals.FirstOrDefault(x => x.PassportSerialNumber == dto.Animal);
                var vet = context.Vets.FirstOrDefault(x => x.Name == dto.Vet);

                if (animal == null || vet == null)
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                var aids = new List<string>();
                var existingAids = context.AnimalAids.Select(x => x.Name).ToList();
                var flag = false;

                foreach (var aid in dto.ProcedureAnimalAids)
                {
                    if (!existingAids.Contains(aid.Name) || aids.Contains(aid.Name))
                    {
                        flag = true;
                        break;
                    }

                    aids.Add(aid.Name);
                }

                if (flag)
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                var procedure = new Procedure()
                {
                    AnimalId = animal.Id,
                    VetId = vet.Id,
                    DateTime = DateTime.ParseExact(dto.DateTime, "dd-MM-yyyy", null)
                };

                var existingAidIds = context.AnimalAids.Where(x => aids.Contains(x.Name)).ToList();

                foreach (var item in existingAidIds)
                {
                    procedure.ProcedureAnimalAids.Add(new ProcedureAnimalAid()
                    {
                        Procedure = procedure, 
                        AnimalAid = item
                    });
                }

                procedures.Add(procedure);
                sb.AppendLine("Record successfully imported."); 
            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges(); 

            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var context = new Annotations.ValidationContext(obj);
            var results = new List<Annotations.ValidationResult>();

            var isValid = Annotations.Validator.TryValidateObject(obj, context, results, true);

            return isValid;
        }
    }
}