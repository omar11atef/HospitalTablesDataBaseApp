using HospitalTablesDataBaseApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
            /*base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //base class properties configuration
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("CreatedAt")
                        .HasDefaultValueSql("GETDATE()");

                    // 2.default value for IsDeleted = 0
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("IsDeleted")
                        .HasDefaultValue(false);
                }
                // داخل اللوب السابق في OnModelCreating
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(e => !((BaseEntity)e).IsDeleted);
            }*/

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            new Configration.DoctorConfiguration().Configure(modelBuilder.Entity<Doctor>());
            new Configration.PatientsConfigration().Configure(modelBuilder.Entity<Patients>());
            new Configration.DepartmentsConfigration().Configure(modelBuilder.Entity<Departments>());
            new Configration.AppointmentConfigration().Configure(modelBuilder.Entity<Appointment>());
            new Configration.MedicalRecordsConfigration().Configure(modelBuilder.Entity<MedicalRecords>());
            new Configration.RoomConfigration().Configure(modelBuilder.Entity<Room>());
            new Configration.PatientRoomConfiguration().Configure(modelBuilder.Entity<PatientRoom>());
            new Configration.InvoiceConfiguration().Configure(modelBuilder.Entity<Invoice>());
            new Configration.InvoiceItemConfiguration().Configure(modelBuilder.Entity<InvoiceItem>());
            new Configration.PrescriptionConfiguration().Configure(modelBuilder.Entity<Prescription>());
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<PatientRoom> PatientRooms { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

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
