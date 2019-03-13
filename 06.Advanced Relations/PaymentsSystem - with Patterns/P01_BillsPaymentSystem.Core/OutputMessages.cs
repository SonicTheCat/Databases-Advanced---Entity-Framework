namespace P01_BillsPaymentSystem.Core
{
    using System.Text;

    public static class OutputMessages
    {
        public static string EnterId => "Enter userId: ";

        public static string Loading => "Loading...";

        public static string InvalidCommand=> "Invalid Command! Try again";

        public static string EnterIdAndAmount => "Enter userId and bills amount separated by white space: ";

        public static string WelcomeString() => "Welcome to our BillPaymentSystem. Please choose one of the following options from the menu!";

        public static string MenuOptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(); 
            sb.AppendLine("Type 'UserInfo {id}' to see specific information about a user");
            sb.AppendLine("Type 'PayBills {id} {billsAmount}' to try pay bills for a user");

            return sb.ToString(); 
        } 
    }
}