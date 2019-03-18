namespace NonStopMarket.App.Core
{
    using System.Text;

    public static class Messagess
    {
        public const string Loading = "Loading...";
        public const string InitializingDatabase = "Initializing Database...";
        public const string InvalidCommand = "Invalid Command! Please, Take Your Time And Try Again... :) ";

        public static string ShowCommands()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Type a command from the list below!\n"); 
            sb.AppendLine("Emoloyee's Commands ");
            sb.AppendLine("1. AddEmployee {FirstName} {LastName} {Salary}");
            sb.AppendLine("2. EmployeeInfo {EmployeeId}");
            sb.AppendLine("3. EmployeePersonalInfo {EmployeeId}");
            sb.AppendLine("4. SetAddress {EmployeeId} {Address}");
            sb.AppendLine("5. SetBirthday {EmployeeId} {Birthday}");

            sb.AppendLine("Manager's Commands"); 
            sb.AppendLine("1. ManagerInfo {ManagerId}");
            sb.AppendLine("2. SetManager {EmployeeId} {ManagerId}");

            sb.AppendLine("Product's Commands");
            sb.AppendLine("1. AddProduct {Name} {TotalQuantity} {QuantityInstock} {price}");
            sb.AppendLine("2. FindBestseller");
            sb.AppendLine("3. GetAllWithExpiredDate");
            sb.AppendLine("4. GetMostExpensiveProducts {productsCount}");
            sb.AppendLine("5. GetCheapestProducts {productsCount}");
            sb.AppendLine("6. FindTopSellingProducts {productsCount}");
            sb.AppendLine("7. SetExpiryDate {ProductId} {Date}");

            sb.AppendLine("Order's Commands");
            sb.AppendLine("1. AddOrder {EmployeeId} {PaymentMethod}");
            sb.AppendLine("2. GetOrdersByIncome");

            sb.AppendLine("\n! Tip: All Commands are case insensitive");

            return sb.ToString().Trim(); 
        }
    }
}