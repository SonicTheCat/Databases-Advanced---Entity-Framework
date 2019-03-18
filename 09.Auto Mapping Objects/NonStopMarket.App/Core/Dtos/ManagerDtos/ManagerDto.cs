namespace NonStopMarket.App.Core.Dtos.ManagerDtos
{
    using EmployeeDtos;
    using System.Collections.Generic;
    using System.Text;

    public class ManagerDto
    {
        public string Firstname { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> ManagerEmployees { get; set; } = new List<EmployeeDto>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Firstname} {this.LastName} | Employees: {this.ManagerEmployees.Count}");

            foreach (var emp in this.ManagerEmployees)
            {
                sb.AppendLine(emp.ToString()); 
            }

            return sb.ToString().Trim(); 
        }
    }
}