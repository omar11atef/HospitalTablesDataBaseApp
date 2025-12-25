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
            new Configration.DoctorConfigrated().Configure(modelBuilder.Entity<Doctor>());
            new Configration.PatientsConfigration().Configure(modelBuilder.Entity<Patients>());
            new Configration.DepartmentsConfigration().Configure(modelBuilder.Entity<Departments>());
            new Configration.AppointmentConfigration().Configure(modelBuilder.Entity<Appointment>());
            new Configration.MedicalRecordsConfigration().Configure(modelBuilder.Entity<MedicalRecords>());
            new Configration.RoomConfigration().Configure(modelBuilder.Entity<Room>());
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Room> Rooms { get; set; }

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
