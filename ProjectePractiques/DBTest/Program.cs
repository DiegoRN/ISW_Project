﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GestDep.Entities;
using GestDep.Persistence;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                EntityFrameworkDAL dal = new EntityFrameworkDAL(new GestDepDbContext());

                new Program(dal);


            }
            catch (Exception e)
            {
                printError(e);
                Console.WriteLine("Press any key.");
                Console.ReadLine();
            }

        }

        public Program(IDAL dal)
        {
            createSampleDB(dal);
            Console.WriteLine("\n\n\n");
            displayData(dal);
        }

        static void printError(Exception e)
        {
            while (e != null)
            {
                if (e is DbEntityValidationException)
                {
                    DbEntityValidationException dbe = (DbEntityValidationException)e;

                    foreach (var eve in dbe.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
                e = e.InnerException;
            }
        }

        private void createSampleDB(IDAL dal)
        {
            // Remove all data from DB
            dal.RemoveAllData();

            CityHall cityHall = new CityHall("Valencia");
            dal.Insert<CityHall>(cityHall);
            dal.Commit();


            Gym gym1 = new Gym(Convert.ToDateTime("14:00:00"), 5, 5, 2.00, "Gym1", Convert.ToDateTime("08:00:00"), 46022);
            dal.Insert<Gym>(gym1);
            cityHall.Gyms.Add(gym1);
            dal.Commit();

            // Populate here the rest of the database with data

        }

        private void displayData(IDAL dal)
        {

            Console.WriteLine("===================================");
            Console.WriteLine("          CityHall details         ");
            Console.WriteLine("===================================");

            CityHall cityhall;
            cityhall = dal.GetAll<CityHall>().First();
            Console.WriteLine("Name: " + cityhall.Name + ", id = " + cityhall.Id);

            Console.WriteLine("Pres Key to exit...");
            Console.ReadKey();

            Console.WriteLine("===================================");
            Console.WriteLine("            Gym details            ");
            Console.WriteLine("===================================");

            foreach (Gym gym in cityhall.Gyms)
            {
                Console.WriteLine("Name: " + gym.Name + ", Opening Hour: " + gym.OpeningHour + ", Closing Hour: " + gym.ClosingHour);
            }

            Console.WriteLine("Pres Key to exit...");
            Console.ReadKey();

        }

        // Display here the information stored in the rest of the database tables

    }
}
