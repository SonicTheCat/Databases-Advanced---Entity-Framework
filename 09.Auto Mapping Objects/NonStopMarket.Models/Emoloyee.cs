using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NonStopMarket.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public ICollection<Employee> ManagerEmployees { get; set; } = new List<Employee>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}