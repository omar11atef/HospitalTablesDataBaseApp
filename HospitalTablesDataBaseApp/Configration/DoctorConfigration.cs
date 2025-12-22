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
            builder.Property(x=>x.PhoneNumber).HasConversion<string>();
            builder.Property(x=>x.TotalHoursWorked).HasDefaultValueSql("0.0");
            builder.Property(x=>x.IsAvailable).HasDefaultValueSql("1");

        }

    }
}
