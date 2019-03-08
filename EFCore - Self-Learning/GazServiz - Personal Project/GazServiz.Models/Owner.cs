namespace GazServiz.Models
{
    using System.Collections.Generic;

    public class Owner
    {
        private int ownerId; 

        public Owner(int ownerId, string firstName, string lastName, string mobile)
        {
            this.ownerId = ownerId; 
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Mobile = mobile;
            this.Cars = new HashSet<Car>();
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string MiddleName { get; private set; }

        public string Mobile { get; private set; }

        public ICollection<Car> Cars { get; }
    }
}