namespace FastFood.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Json = Newtonsoft.Json;

    using Data;
    using Dto.Export;
    using Models.Enums;

    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var type = Enum.Parse<OrderType>(orderType);

            var employee = context.Orders
                .Where(x => x.Employee.Name == employeeName && x.Type == type)
                .Select(x => new EmployeeDto()
                {
                    Name = x.Employee.Name,
                    Orders = x.Employee.Orders
                    .Select(o => new OrderDto()
                    {
                        Customer = o.Customer,
                        Items = o.OrderItems
                        .Select(oi => new ItemDto()
                        {
                            Name = oi.Item.Name,
                            Price = oi.Item.Price,
                            Quantity = oi.Quantity
                        })
                        .ToArray(),
                    })
                    .ToArray(),
                })
                .ToArray()[0];


            foreach (var order in employee.Orders)
            {
                foreach (var item in order.Items)
                {
                    order.TotalPrice += item.Price * item.Quantity;
                }
                employee.TotalMade += order.TotalPrice;
            }

            employee.Orders = employee.Orders
                .OrderByDescending(x => x.TotalPrice)
                .ThenByDescending(x => x.Items.Length)
                .ToArray();

            var employeesTostring = Json.JsonConvert.SerializeObject(employee, Json.Formatting.Indented);

            return employeesTostring;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var wantedCategories = categoriesString.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var categories = context.Items
                .Where(x => wantedCategories.Contains(x.Category.Name))
                .GroupBy(x => x.Category.Name)
                .Select(x => new CategoryDto
                {
                    Name = x.Key,
                    MostPopularItem = x
                    .Select(i => new MostPopularItemDto
                    {
                        Name = i.Name,
                        TotalMade = i.OrderItems.Sum(oi => oi.Quantity * oi.Item.Price),
                        TimesSold = i.OrderItems.Sum(oi => oi.Quantity),
                    })
                    .OrderByDescending(i => i.TotalMade)
                    .ToArray()[0]
                })
                .OrderByDescending(x => x.MostPopularItem.TotalMade)
                .ThenByDescending(x => x.MostPopularItem.TimesSold)
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), categories, namespaces);

            return sb.ToString();
        }
    }
}