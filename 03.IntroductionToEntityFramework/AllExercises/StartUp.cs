namespace SoftUni
{
    using Microsoft.EntityFrameworkCore;
    using SoftUni.Data;
    using SoftUni.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new SoftUniContext())
            {
                //var employees = RemoveTown(context);
                //Console.WriteLine(employees);
            }
        }

        /* Exercise 15 */
        public static string RemoveTown(SoftUniContext context)
        {
            var town = context.Towns
                .Include(x => x.Addresses)
                .FirstOrDefault(x => x.Name == "Seattle");

            var townId = town.TownId;
            var addressesCount = town.Addresses.Count;

            context.Employees
                .Where(e => e.Address.TownId == townId)
                .ToList()
                .ForEach(e => e.AddressId = null);

            context.Addresses.RemoveRange(town.Addresses);
            context.Towns.Remove(town);
            context.SaveChanges();

            return $"{addressesCount} addresses in {town.Name} were deleted";
        }

        /* Exercise 14 */
        public static string DeleteProjectById(SoftUniContext context)
        {
            var empProjects = context.EmployeesProjects.Where(p => p.ProjectId == 2).ToList();
            var project = context.Projects.SingleOrDefault(p => p.ProjectId == 2);

            context.EmployeesProjects.RemoveRange(empProjects);
            context.Projects.Remove(project);

            context.SaveChanges();

            var sb = new StringBuilder();
            context.Projects
                .Take(10)
                .Select(p => p.Name)
                .ToList()
                .ForEach(p => sb.AppendLine(p));

            return sb.ToString().TrimEnd();
        }

        /* Exercise 13 */
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var sb = new StringBuilder();

            context.Employees
                .Where(e => EF.Functions.Like(e.FirstName, "Sa%"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList()
                .ForEach(e => sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})"));

            return sb.ToString().TrimEnd();
        }

        /* Exercise 12 */
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departments = new string[]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var employees = context.Employees
                .Where(e => departments.Contains(e.Department.Name))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            var sb = new StringBuilder();
            foreach (var e in employees)
            {
                e.Salary *= 1.12m;
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
            }

            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        /* Exercise 11 */
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate.Ticks)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .Take(10)
                .OrderBy(p => p.Name)
                .ToList();

            var sb = new StringBuilder();
            foreach (var p in projects)
            {
                sb.AppendLine(p.Name);
                sb.AppendLine(p.Description);
                sb.AppendLine(p.StartDate.ToString("M/d/yyyy h:mm:ss tt"));
            }

            return sb.ToString().TrimEnd();
        }

        /* Exercise 10 - Second Solution */
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var departments = context.Departments
                .Include(d => d.Employees)
                .Include(d => d.Manager)
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .ToList();

            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.Manager}");
                foreach (var e in d.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        /* Exercise 10 */
        //public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        //{
        //    var sb = new StringBuilder();

        //    context.Departments
        //          .Where(d => d.Employees.Count > 5)
        //          .Select(d => new
        //          {
        //              d.Name,
        //              Manager = d.Manager.FirstName + " " + d.Manager.LastName,
        //              Employees = d.Employees
        //              .OrderBy(e => e.FirstName)
        //              .ThenBy(e => e.LastName)
        //              .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle}")
        //              .ToList()
        //          })
        //          .OrderBy(d => d.Employees.Count)
        //          .ThenBy(d => d.Name)
        //          .ToList()
        //          .ForEach(d => sb.AppendLine($"{d.Name} - {d.Manager}\n{string.Join("\n", d.Employees)}"));

        //    return sb.ToString().TrimEnd();
        //}

        /* Exercise 9 */
        public static string GetEmployee147(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var employee = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    ProjectNames = e.EmployeesProjects.Select(p => p.Project.Name).ToList()
                })
                .SingleOrDefault(e => e.EmployeeId == 147);

            sb.AppendLine(employee.FirstName + " " + employee.LastName + " - " + employee.JobTitle);
            foreach (var project in employee.ProjectNames.OrderBy(p => p))
            {
                sb.AppendLine(project);
            }

            return sb.ToString().TrimEnd();
        }

        /* Exercise 8 - Second Solution */
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var sb = new StringBuilder();

            context.Addresses
                  .Select(a => new
                  {
                      a.AddressText,
                      a.Town.Name,
                      EmployeesCount = a.Employees.Count
                  })
                  .Take(10)
                  .OrderByDescending(a => a.EmployeesCount)
                  .ThenBy(a => a.Name)
                  .ThenBy(a => a.AddressText)
                  .ToList()
                  .ForEach(a => sb.AppendLine($"{a.AddressText}, {a.Name} - {a.EmployeesCount} employees"));

            return sb.ToString().TrimEnd();
        }

        /* Exercise 8 */
        //public static string GetAddressesByTown(SoftUniContext context)
        //{
        //    var sb = new StringBuilder();

        //    context.Addresses
        //    .GroupBy(a => new
        //    {
        //        a.AddressId,
        //        a.AddressText,
        //        a.Town.Name
        //    },
        //    (key, group) => new
        //    {
        //        Address = key.AddressText,
        //        Town = key.Name,
        //        Count = group.Sum(a => a.Employees.Count)
        //    })
        //    .OrderByDescending(a => a.Count)
        //    .ThenBy(a => a.Town)
        //    .ThenBy(a => a.Address)
        //    .Take(10)
        //    .ToList()
        //    .ForEach(a => sb.AppendLine($"{a.Address}, {a.Town} - {a.Count} employees"));

        //    return sb.ToString().TrimEnd();
        //}

        /* Exercise 7 */
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var emoloyees = context.Employees
             .Where(e => e.EmployeesProjects
                           .Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
             .Select(e => new
             {
                 e.EmployeeId,
                 EmployeeFullName = e.FirstName + " " + e.LastName,
                 ManagerFullName = e.Manager.FirstName + " " + e.Manager.LastName,
                 ProjectsInfo = e.EmployeesProjects
                 .Select(p => new
                 {
                     p.Project.Name,
                     p.Project.StartDate,
                     p.Project.EndDate
                 }).ToList()
             })
             .Take(10)
             .ToList();

            var sb = new StringBuilder();
            foreach (var emp in emoloyees)
            {
                sb.AppendLine($"{emp.EmployeeFullName} - Manager: {emp.ManagerFullName}");

                foreach (var info in emp.ProjectsInfo)
                {
                    var startDate = info.StartDate.ToString("M/d/yyyy h:mm:ss tt");
                    var endDate = !info.EndDate.HasValue ? "not finished" : info.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt");
                    sb.AppendLine($"--{info.Name} - {startDate} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        /* Exercise 6 */
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            employee.Address = new Address("Vitoshka 15", 4);

            context.SaveChanges();

            context.Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10)
                .ToList()
                .ForEach(address => sb.AppendLine(address));

            return sb.ToString().TrimEnd();
        }

        /* Exercise 5 */
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();

            context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList()
                .ForEach(e => sb.AppendLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:F2}"));

            return sb.ToString().TrimEnd();
        }

        /* Exercise 4 */
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var sb = new StringBuilder();

            context.Employees
                .Where(e => e.Salary > 50000)
               .Select(e => new
               {
                   e.FirstName,
                   e.Salary
               })
               .OrderBy(e => e.FirstName)
               .ToList()
               .ForEach(e => sb.AppendLine($"{e.FirstName} - {e.Salary:F2}"));

            return sb.ToString().Trim();
        }

        /* Exercise 3 */
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

            context.Employees
                 .Select(e => new
                 {
                     e.EmployeeId,
                     e.FirstName,
                     e.LastName,
                     e.MiddleName,
                     e.JobTitle,
                     e.Salary
                 })
             .OrderBy(e => e.EmployeeId)
            .ToList()
            .ForEach(emp => sb.AppendLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary.ToString("F2")}"));

            return sb.ToString().TrimEnd();
        }
    }
}