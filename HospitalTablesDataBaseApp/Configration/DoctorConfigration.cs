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
    public class DoctorConfigrated : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x=>x.Id).HasName("Doctor_PrimaryKey");
            builder.Property(x => x.PhoneNumber)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x=>x.TotalHoursWorked).HasDefaultValue(0.0);
            builder.Property(x=>x.IsAvailable).HasDefaultValue(true);
            builder.HasOne(x=>x.Department).WithMany(d=>d.Doctors).HasForeignKey(x=>x.DepartmentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x=>x.Appointments).WithOne(a=>a.Doctor).HasForeignKey(x=>x.DoctorId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x=>x.MedicalRecords).WithOne(m=>m.Doctor).HasForeignKey(x=>x.DoctorId).OnDelete(DeleteBehavior.Restrict);
           
        }

    }
}
