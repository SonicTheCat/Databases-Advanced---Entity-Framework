namespace P01_BillsPaymentSystem.Core
{
    using System.Linq;
    using System.Text;
    using System;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using P01_BillsPaymentSystem.Core.IO.Contracts;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly BillsPaymentSystemContext context;

        public Engine(IReader reader, IWriter writer, BillsPaymentSystemContext context)
        {
            this.reader = reader;
            this.writer = writer;
            this.context = context;
        }

        public void Run()
        {
            this.writer.WriteLine(OutputMessages.WelcomeString());

            while (true)
            {
                try
                {
                    this.writer.WriteLine(OutputMessages.MenuOptions());

                    var result = string.Empty;
                    var command = this.reader.ReadLine().Split();

                    if (command[0] == "1")
                    {
                        this.writer.WriteLine(OutputMessages.EnterId);

                        var userId = int.Parse(this.reader.ReadLine());

                        this.writer.WriteLine(OutputMessages.Loading);
                        result = this.FindUser(userId);
                    }
                    else if (command[0] == "2")
                    {
                        this.writer.WriteLine(OutputMessages.EnterIdAndAmount);

                        var tokens = this.reader.ReadLine().Split();
                        var userId = int.Parse(tokens[0]);
                        var amount = decimal.Parse(tokens[1]);

                        this.writer.WriteLine(OutputMessages.Loading);
                        result = this.PayBills(userId, amount);

                        this.context.SaveChanges();
                    }
                    else if (command[0] == "9")
                    {
                        this.writer.WriteLine("Bye bye! 4ao 4ao! Arividerchi! Адиос!");
                        Environment.Exit(0);
                    }

                    this.writer.WriteLine(result);
                }
                catch (Exception)
                {
                    this.writer.WriteLine(OutputMessages.InvalidCommand); 
                }
            }
        }

        private string FindUser(int userId)
        {
            var userInfo = context.Users
                    .Select(u => new
                    {
                        Id = u.UserId,
                        Name = u.FirstName + " " + u.LastName,
                        BankAccounts = u.PaymentMethods.Where(x => x.BankAccount != null).Select(pm => pm.BankAccount).ToList(),
                        CreditCards = u.PaymentMethods.Where(x => x.CreditCard != null).Select(pm => pm.CreditCard).ToList(),
                    })
                    .SingleOrDefault(u => u.Id == userId);


            StringBuilder sb = new StringBuilder();

            if (DoesExist(userInfo))
            {
                sb.AppendLine("User: " + userInfo.Name);

                sb.AppendLine("Bank Accounts:");
                foreach (var bankAcc in userInfo.BankAccounts)
                {
                    sb.AppendLine(bankAcc.ToString());
                }

                sb.AppendLine("Credit Cards:");
                foreach (var creditCard in userInfo.CreditCards)
                {
                    sb.AppendLine(creditCard.ToString());
                }
            }
            else
            {
                sb.AppendLine($"User with id {userId} not found!");
            }

            return sb.ToString().Trim();
        }

        private bool DoesExist(object userInfo) => userInfo != null;

        private string PayBills(int userId, decimal billsAmount)
        {
            var user = this.context.Users
                .Include(x => x.PaymentMethods)
                .ThenInclude(x => x.BankAccount)
                .Include(x => x.PaymentMethods)
                .ThenInclude(x => x.CreditCard)
                .FirstOrDefault(x => x.UserId == userId);

            decimal total = FindTotalMoney(user);

            StringBuilder sb = new StringBuilder();
            if (total < billsAmount)
            {
                sb.AppendLine("Nqma dostatachno $$$Kinti-minti!$$$ za smetkite!");
            }
            else
            {
                var accounts = user.PaymentMethods
                    .Where(x => x.BankAccount != null)
                    .Select(x => x.BankAccount)
                    .OrderBy(x => x.BankAccountId)
                    .ToList();

                foreach (var acc in accounts)
                {
                    if (acc.Balance < billsAmount)
                    {
                        billsAmount -= acc.Balance;
                        acc.Withdraw(acc.Balance);
                    }
                    else
                    {
                        acc.Withdraw(billsAmount);
                        billsAmount = 0;
                        break;
                    }
                }

                var creditCards = user.PaymentMethods
                   .Where(x => x.CreditCard != null)
                   .Select(x => x.CreditCard)
                   .OrderBy(x => x.CreditCardId)
                   .ToList();

                foreach (var card in creditCards)
                {
                    if (card.LimitLeft < billsAmount)
                    {
                        billsAmount -= card.LimitLeft;
                        card.Withdraw(card.LimitLeft);
                    }
                    else
                    {
                        card.Withdraw(billsAmount);
                        billsAmount = 0;
                        break;
                    }
                }

                sb.AppendLine("Bravo! Bravo! Bravo, Gospodine! Uspq da platish parnoto! :)");
            }

            return sb.ToString().Trim();
        }

        private static decimal FindTotalMoney(User user)
        {
            var bankAccountTotalAmount = user.PaymentMethods.Where(x => x.BankAccount != null).Sum(x => x.BankAccount.Balance);
            var creditCardTotalAmount = user.PaymentMethods.Where(x => x.CreditCard != null).Sum(x => x.CreditCard.LimitLeft);
            var total = bankAccountTotalAmount + creditCardTotalAmount;
            return total;
        }
    }
}