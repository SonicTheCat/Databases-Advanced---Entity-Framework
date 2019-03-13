namespace P01_BillsPaymentSystem.Data
{
    using Repositories.Contracts;

    using System;

    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete(); 
    }
}