namespace GazServiz.Models
{
    using System;

    public class Repair
    {
        private int id;

        public Repair(int id, string problemDescription, int carId, int employeeId)
        {
            this.id = id; 
            this.ProblemDescription = problemDescription;
            this.DateIn = DateTime.Now;
            this.CarId = carId;
            this.EmployeeId = employeeId;
        }

        public bool IsFixed { get; private set; }

        public decimal? Bill { get; private set; }

        public string ProblemDescription { get; }

        public DateTime DateIn { get; private set; }

        public DateTime? DateOut { get; private set; }

        public int CarId { get; }
        public Car Car { get; set; }

        public int EmployeeId { get; }
        public Employee Employee { get; set; }

        public void FixCar()
        {
            //TODO Fix Car
        }
    }
}