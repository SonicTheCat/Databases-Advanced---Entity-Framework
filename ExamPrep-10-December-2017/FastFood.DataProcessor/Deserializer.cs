namespace FastFood.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using Annotations = System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json;
    using AutoMapper;

    using FastFood.Data;
    using Dto.Import;
    using FastFood.Models;
    using System.Xml.Serialization;
    using System.Xml;
    using System.IO;
    using FastFood.Models.Enums;
    using System.Globalization;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";
        private const string DateFormat = "dd/MM/yyyy HH:mm"; 

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var employeesDto = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            var sb = new StringBuilder();

            var employeesToAdd = new List<Employee>();
            var existingPositions = context.Positions.ToHashSet();

            foreach (var dto in employeesDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Position position = existingPositions.FirstOrDefault(x => x.Name == dto.Position);
                if (position == null)
                {
                    position = new Position() { Name = dto.Position };
                }

                var employee = new Employee()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Position = position
                };

                existingPositions.Add(employee.Position);
                employeesToAdd.Add(employee);

                sb.AppendLine(string.Format(SuccessMessage, employee.Name));
            }

            context.Employees.AddRange(employeesToAdd);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var itemsDto = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            var sb = new StringBuilder();

            var itemsToAdd = new List<Item>();
            var existingItems = context.Items.ToHashSet();
            var existingCategories = context.Categories.ToList();

            foreach (var dto in itemsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var category = existingCategories.FirstOrDefault(x => x.Name == dto.Category);
                if (category == null)
                {
                    category = new Category() { Name = dto.Category };
                }

                var item = new Item()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Category = category
                };

                if (existingItems.Contains(item))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                existingCategories.Add(category); 
                existingItems.Add(item);
                itemsToAdd.Add(item);

                sb.AppendLine(string.Format(SuccessMessage, item.Name));
            }

            context.Items.AddRange(itemsToAdd);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            var ordersDto = (OrderDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var existingEmployees = context.Employees.Select(x => x.Name).ToHashSet();
            var existingItems = context.Items.Select(x => x.Name).ToHashSet();

            var orders = new List<Order>(); 

            foreach (var dto in ordersDto)
            {
                if (!IsValid(dto) || !existingEmployees.Contains(dto.Employee))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var flag = false; 
                foreach (var itemToInsert in dto.Items)
                {
                    if (!existingItems.Contains(itemToInsert.Name) || !IsValid(itemToInsert))
                    {
                        flag = true;
                        break; 
                    }
                }

                if (flag)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var employee = context.Employees.FirstOrDefault(x => x.Name == dto.Employee);
              
                var order = new Order()
                {
                    Customer = dto.Customer,
                    Employee = employee,
                    Type = dto.Type,
                    DateTime = DateTime.ParseExact(dto.DateTime, DateFormat, CultureInfo.InvariantCulture)
                };

                foreach (var itemToInsert in dto.Items)
                {
                    var item = context.Items.First(x => x.Name == itemToInsert.Name);

                    order.OrderItems.Add(new OrderItem()
                    {
                        Item = item,
                        Quantity = itemToInsert.Quantity
                    }); 
                }

                orders.Add(order);
                sb.AppendLine($"Order for {order.Customer} on {order.DateTime.ToString(DateFormat, CultureInfo.InvariantCulture)} added"); 
            }

            context.Orders.AddRange(orders);
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