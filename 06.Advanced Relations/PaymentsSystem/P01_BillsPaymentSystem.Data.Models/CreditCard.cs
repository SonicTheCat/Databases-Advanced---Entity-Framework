namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    [Table("CreditCards")]
    public class CreditCard
    {
        public CreditCard(decimal limit, decimal moneyOwned, DateTime expirationDate)
        {
            this.Limit = limit;
            this.MoneyOwned = moneyOwned;
            this.ExpirationDate = expirationDate; 
        }

        [Key]
        public int CreditCardId { get; private set; }

        public decimal Limit { get; private set; }

        public decimal MoneyOwned { get; private set; }
        
        [NotMapped]
        public decimal LimitLeft => this.Limit - this.MoneyOwned; 

        public DateTime ExpirationDate { get; private set; }

        public PaymentMethod PaymentMethod { get; private set; }

        public void Deposit(decimal amount)
        {
            this.MoneyOwned -= amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > this.LimitLeft)
            {
                throw new ArgumentException("Not enough money in the card!"); 
            }

            this.MoneyOwned += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("--ID: " + this.CreditCardId);
            sb.AppendLine("---Limit: " + this.Limit);
            sb.AppendLine("---Money Owed: " + this.MoneyOwned);
            sb.AppendLine("---Limit Left: " + this.LimitLeft);
            sb.AppendLine("---Expiration Date: " + this.ExpirationDate);

            return sb.ToString().Trim(); 
        }
    }
}