namespace NonStopMarket.App.Core.Dtos.EmployeeDtos
{
    using System;
    using System.Text;

    public class EmployeePersonalInfoDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.EmployeeId} - {this.FirstName} {this.LastName} -  ${this.Salary:f2}");
            sb.AppendLine($"Birthday: {this.Birthday}"); 
            sb.AppendLine($"Address: {this.Address}");

            return sb.ToString().Trim(); 
        }
    }
}