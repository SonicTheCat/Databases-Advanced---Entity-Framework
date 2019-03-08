namespace GazServiz.Models
{
    using System.Collections.Generic;

    public class Car
    {
        private int id;

        public Car(int id, string model, string licensePlate, double millage, int ownerId)
        {
            this.id = id; 
            this.Model = model;
            this.LicensePlate = licensePlate;
            this.Millage = millage;
            this.OwnerId = ownerId;
            this.Repairs = new HashSet<Repair>();
        }

        public string Model { get; }

        public string LicensePlate { get; }

        public double Millage { get; }

        public int OwnerId { get; }
        public Owner Owner { get; set; }

        public ICollection<Repair> Repairs { get; }
    }
}