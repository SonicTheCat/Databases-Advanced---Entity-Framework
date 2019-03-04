namespace P03_SalesDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }

        public Customer(string name, string email, string card)
            : base()
        {
            this.Name = name;
            this.Email = email;
            this.CreditCardNumber = card;
        }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}