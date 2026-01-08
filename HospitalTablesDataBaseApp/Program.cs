using HospitalTablesDataBaseApp.Models;
using HospitalTablesDataBaseApp.Querys;
using Microsoft.EntityFrameworkCore; // Add this using directive
using Microsoft.EntityFrameworkCore.Infrastructure; // Add this using directive
using System;
using System.Data.Common; // Add this using directive
namespace HospitalTablesDataBaseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Check Connection :
            using (var context = new ApplacationDBContext())
            {
                // Print the connection string
               /* Console.WriteLine("Connection String:");
<<<<<<< HEAD
                var connectionString = context.Database.GetDbConnection().ConnectionString
                Console.WriteLine(connectionString);*/
               
=======
                var connectionString = context.Database.GetDbConnection().ConnectionString;
                Console.WriteLine(connectionString);*/
               

                Console.WriteLine(connectionString);
>>>>>>> 2def763b892178e34518f459fce6f8fe48cc8700
                Console.WriteLine(connectionString);
                //context.Doctors.GetAllDoctors;
                
                // Optional: Test if connection works
                bool isConnected = context.TestConnection();
                Console.WriteLine(isConnected
                    ? "Database connection is successful!"
                    : "Failed to connect to the database.");
<<<<<<< HEAD
=======

                // Seed Test Data :
>>>>>>> 2def763b892178e34518f459fce6f8fe48cc8700
                /*try
                {
                    Console.WriteLine("Seeding Data...");
                    DataSeederTestQuery.Seed(context);

                    Console.WriteLine("✅ Database seeded successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error seeding data: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"🔍 Inner Exception: {ex.InnerException.Message}");
                    }
                }*/

                context.Appointments.IfManyAppointmentONSameTime(context);
<<<<<<< HEAD
                context.Rooms.GetAllRooms();   
=======
                context.Rooms.GetAllRooms();
>>>>>>> 2def763b892178e34518f459fce6f8fe48cc8700
            }


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
