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
                var connectionString = context.Database.GetDbConnection().ConnectionString;
                Console.WriteLine(connectionString);*/
               
                // Optional: Test if connection works
                bool isConnected = context.TestConnection();
                Console.WriteLine(isConnected
                    ? "Database connection is successful!"
                    : "Failed to connect to the database.");

                // Seed Test Data :
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
                
                

            }


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
