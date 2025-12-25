using HospitalTablesDataBaseApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Configration
{
    public class MedicalRecordsConfigration : IEntityTypeConfiguration<MedicalRecords>
    {
        public void Configure(EntityTypeBuilder<MedicalRecords> builder)
        {
            builder.HasKey(x => x.Id).HasName("MedicalRecords_PrimaryKey");
            builder.Property(x=>x.VisitDate).HasDefaultValueSql("GETDATE()");
            builder.HasOne(x=>x.Appointment)
                   .WithOne(x=>x.MedicalRecords!)
                   .HasForeignKey<MedicalRecords>(x=>x.AppointmentId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
