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
    public  class PatientsConfigration : IEntityTypeConfiguration<Patients>
    {
        public void Configure(EntityTypeBuilder<Patients> builder)
        {
            builder.HasKey(x => x.Id).HasName("Patients_PrimaryKey");
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Age).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValueSql("1");
            builder.Property(x=>x.Visit).HasDefaultValueSql("GETDATE()");
        }
    }
}
