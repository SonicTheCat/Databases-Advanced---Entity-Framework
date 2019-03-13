namespace P01_BillsPaymentSystem.Data.Repositories.Contracts
{
    using P01_BillsPaymentSystem.Data.Models;

    public interface IUserRepository : IRepository<User>
    {
        string PayBills(int id, decimal amount);

        string GetUserAndAllPayments(int id); 
    }
}