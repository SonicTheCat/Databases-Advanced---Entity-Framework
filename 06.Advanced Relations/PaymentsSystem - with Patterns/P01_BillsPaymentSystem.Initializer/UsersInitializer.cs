namespace P01_BillsPaymentSystem.Initializer
{
    using P01_BillsPaymentSystem.Data.Models;

    public class UsersInitializer
    {
        public static User[] GetUsers()
        {
            return new User[]
            {
                new User() {FirstName = "Pencho", LastName = "Stoilkov", Email = "Pen4o@abv.bg", Password = "123" },
                new User() {FirstName = "Viktor", LastName = "Paskalev", Email = "vic@abv.bg", Password = "12345" },
                new User() {FirstName = "Minka", LastName = "Svirkata", Email = "Minka_TheWistle@hotmail.bg", Password = "12341432432" },
                new User() {FirstName = "Penka", LastName = "Penkova", Email = "Penka@gmail.com", Password = "123" },
                new User() {FirstName = "Ginka", LastName = "Stoilkova", Email = "Pen4o&Ginka@abv.bg", Password = "12121xaxa23" },
                new User() {FirstName = "Pesho", LastName = "Petrov", Email = "bmw_maniak@abv.bg", Password = "212131" },
                new User() {FirstName = "Ivan", LastName = "Ivanov", Email = "vanio_95@abv.bg", Password = "vanio123" },
                new User() {FirstName = "Vili", LastName = "Vucova", Email = "Sladuranata_70@abv.bg", Password = "321" },
            }; 
        }
    }
}