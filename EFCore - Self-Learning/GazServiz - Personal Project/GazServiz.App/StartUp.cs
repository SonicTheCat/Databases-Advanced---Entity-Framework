namespace GazServiz.App
{
    using GazServiz.Data;
    using GazServiz.Data.Paths;
    using GazServiz.Models;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new GarageDbContext())
            {
                //var cars = context.Cars.Include(c => c.Owner).ToList();

                //foreach (var car in cars)
                //{
                //    Console.WriteLine("Model: " + car.Model);
                //    Console.WriteLine($"--- Owner: {car.Owner.FirstName} {car.Owner.LastName}, LicensePlate: {car.LicensePlate}");
                //}
            } 
        }
    }
}