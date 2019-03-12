namespace P01_BillsPaymentSystem.Initializer
{
    using P01_BillsPaymentSystem.Data.Models;

    public class BankAccountInitializer
    {
        public static BankAccount[] GetBankAccounts()
        {
            return new BankAccount[]
            {
                new BankAccount(2000.23m, "Fibank", "BG101ABF1092"),
                new BankAccount(1, "CCB", "GB10201772"),
                new BankAccount(1_000_000, "EurpeanBank", "EU191919"),
                new BankAccount(65201.10m, "SwissBank", "SW10101010SW"),
                new BankAccount(10, "BNB", "BG10x24343a"),
                new BankAccount(555_000_000, "Fibank", "XAXA104030"),
            };
        }
    }
}