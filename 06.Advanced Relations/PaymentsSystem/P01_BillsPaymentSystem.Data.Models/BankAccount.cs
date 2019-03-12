namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    [Table("BankAccounts")]
    public class BankAccount
    {
        public BankAccount(decimal balance, string bankName, string swiftCode)
        {
            this.Balance = balance;
            this.BankName = bankName;
            this.SwiftCode = swiftCode; 
        }

        [Key]
        public int BankAccountId { get; private set; }

        public decimal Balance { get; private set; }

        [Required]
        [MaxLength(50)]
        public string BankName { get; private set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string SwiftCode { get; private set; }

        public PaymentMethod PaymentMethod { get; private set; }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (this.Balance < amount)
            {
                throw new ArgumentException("Not enough money in your account!");
            }

            this.Balance -= amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("--ID: " + this.BankAccountId);
            sb.AppendLine("---Balance: " + this.Balance);
            sb.AppendLine("---Bank: " + this.BankName);
            sb.AppendLine("---SWIFT: " + this.SwiftCode);

            return sb.ToString().Trim(); 
        }
    }
}