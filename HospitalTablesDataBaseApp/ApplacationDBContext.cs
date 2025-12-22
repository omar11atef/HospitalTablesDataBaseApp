using HospitalTablesDataBaseApp.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp
{
    public class ApplacationDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=DESKTOP-5DDONC6\\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;Trust Server Certificate=True");
         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            

         }
        public DbSet<Doctor> Doctors { get; set; }
        public bool TestConnection()
        {
            try
            {
                return this.Database.CanConnect();
            }
            catch
            {
                return false;
            }
        }
    }
}
