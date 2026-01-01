using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalTablesDataBaseApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalTablesDataBaseApp.Configration
{
    //public  class PatientsConfigration : IEntityTypeConfiguration<Patients>
    public class PatientsConfigration : BaseEntityTypeConfiguration<Patients>
    {
        public void Configure(EntityTypeBuilder<Patients> builder)
        {
            builder.HasKey(x => x.Id).HasName("Patients_PrimaryKey");
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Age).IsRequired();
            //builder.Property(x => x.IsActive).HasDefaultValueSql("1");
            builder.Property(x=>x.Visit).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.Max_Medical_Expenses)
                .HasColumnType("decimal(18,2)");
            // search by NationalId , should be unique:
            builder.HasIndex(p => p.NationalId)
                   .IsUnique();
            // Handling NationalId properties , Stored :
            builder.Property(p => p.NationalId)
                   .IsFixedLength()
                   .HasMaxLength(14)
                   .IsUnicode(false);

            // RelationShips
            // One Patient Has Many Appointments
            builder.HasMany(x => x.Appointments)
                   .WithOne(x => x.Patient!)
                   .HasForeignKey(x => x.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
