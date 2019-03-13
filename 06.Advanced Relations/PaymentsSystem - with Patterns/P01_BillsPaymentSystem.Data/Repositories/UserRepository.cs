namespace P01_BillsPaymentSystem.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using P01_BillsPaymentSystem.Data.Models;
    using Contracts;

    using System.Linq;
    using System.Text;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BillsPaymentSystemContext context) 
            : base(context)
        {
        }

        public BillsPaymentSystemContext BillContext => this.context as BillsPaymentSystemContext; 

        public string GetUserAndAllPayments(int id)
        {
            var userInfo = this.BillContext.Users
                  .Select(u => new
                  {
                      Id = u.UserId,
                      Name = u.FirstName + " " + u.LastName,
                      BankAccounts = u.PaymentMethods.Where(x => x.BankAccount != null).Select(pm => pm.BankAccount).ToList(),
                      CreditCards = u.PaymentMethods.Where(x => x.CreditCard != null).Select(pm => pm.CreditCard).ToList(),
                  })
                  .SingleOrDefault(u => u.Id == id);

            StringBuilder sb = new StringBuilder();

            if (userInfo != null)
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
                sb.AppendLine($"User with id {id} not found!");
            }

            return sb.ToString().Trim();
        }

        public string PayBills(int id, decimal billsAmount)
        {
            var user = this.BillContext.Users
               .Include(x => x.PaymentMethods)
               .ThenInclude(x => x.BankAccount)
               .Include(x => x.PaymentMethods)
               .ThenInclude(x => x.CreditCard)
               .FirstOrDefault(x => x.UserId == id);


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

        private decimal FindTotalMoney(User user)
        {
            var bankAccountTotalAmount = user.PaymentMethods.Where(x => x.BankAccount != null).Sum(x => x.BankAccount.Balance);
            var creditCardTotalAmount = user.PaymentMethods.Where(x => x.CreditCard != null).Sum(x => x.CreditCard.LimitLeft);
            var total = bankAccountTotalAmount + creditCardTotalAmount;
            return total;
        }
    }
}