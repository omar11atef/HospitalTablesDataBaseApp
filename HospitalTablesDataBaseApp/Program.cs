using Microsoft.EntityFrameworkCore; // Add this using directive
using Microsoft.EntityFrameworkCore.Infrastructure; // Add this using directive
using System.Data.Common; // Add this using directive
using System;
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
                Console.WriteLine("Connection String:");
                var connectionString = context.Database.GetDbConnection().ConnectionString;
                Console.WriteLine(connectionString);

                // Optional: Test if connection works
                bool isConnected = context.TestConnection();
                Console.WriteLine(isConnected
                    ? "Database connection is successful!"
                    : "Failed to connect to the database.");
            }


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
