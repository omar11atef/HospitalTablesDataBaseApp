using HospitalTablesDataBaseApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Configration
{
    //public class DoctorConfigrated : IEntityTypeConfiguration<Doctor>
    public class DoctorConfiguration : BaseEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x=>x.Id).HasName("Doctor_PrimaryKey");
            builder.Property(x => x.PhoneNumber)
                   .HasMaxLength(20)
                   .IsRequired();
            builder.Property(d => d.NationalId)
                   .IsRequired()
                   .IsFixedLength()      
                   .HasMaxLength(14) 
                   .IsUnicode(false);
            builder
                .Property(d => d.TotalHoursWorked)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.0m); 
            // search by NationalId , should be unique
            builder.HasIndex(d => d.NationalId)
                   .IsUnique();


            builder.Property(x=>x.TotalHoursWorked).HasDefaultValue(0.0);
            //builder.Property(x=>x.IsAvailable).HasDefaultValue(true);
            // relationships with Department :
            builder.HasOne(x=>x.Department).WithMany(d=>d.Doctors).HasForeignKey(x=>x.DepartmentId).OnDelete(DeleteBehavior.Cascade);
            // realationships with Appointments :
            builder.HasMany(x=>x.Appointments).WithOne(a=>a.Doctor).HasForeignKey(x=>x.DoctorId).OnDelete(DeleteBehavior.Restrict);
            // realationships with MedicalRecords :
            //builder.HasMany(x=>x.MedicalRecords).WithOne(m=>m.Doctor).HasForeignKey(x=>x.DoctorId).OnDelete(DeleteBehavior.Restrict);

        }

    }
}
