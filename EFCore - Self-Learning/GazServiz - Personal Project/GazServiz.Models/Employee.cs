namespace GazServiz.Models
{
    using GazServiz.Models.Enums;
    using System.Collections.Generic;

    public class Employee
    {
        private int id; 

        public Employee(int id, string name, Speciality speciality)
        {
            this.id = id; 
            this.Name = name; 
            this.Speciality = speciality;
            this.Repairs = new HashSet<Repair>(); 
        }

        public string Name { get; }

        public Speciality Speciality { get; }

        public ICollection<Repair> Repairs { get; }
    }
}