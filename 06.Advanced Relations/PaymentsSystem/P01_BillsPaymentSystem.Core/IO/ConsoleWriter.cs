namespace P01_BillsPaymentSystem.Core.IO
{
    using System;

    using Contracts;
    
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}