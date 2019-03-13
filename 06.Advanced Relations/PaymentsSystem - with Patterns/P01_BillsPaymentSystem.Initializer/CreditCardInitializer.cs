namespace P01_BillsPaymentSystem.Initializer
{
    using P01_BillsPaymentSystem.Data.Models;
    using System;

    public class CreditCardInitializer
    {
        public static CreditCard[] GetCreditCards()
        {
            return new CreditCard[]
            {
                new CreditCard(10_000, 200, DateTime.Parse("10-10-2020")),
                new CreditCard(1000, 300, DateTime.Parse("09-09-2021")),
                new CreditCard(2_00_000, 1_00_000, DateTime.Parse("05-05-2030")),
                new CreditCard(10_000, 7653, DateTime.Parse("03-06-2020"))
            };
        }
    }
}