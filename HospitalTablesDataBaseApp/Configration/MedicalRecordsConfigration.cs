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

            // MedicalRecords to Appointment Relationship
            builder.HasOne(x=>x.Appointment)
                   .WithOne(x=>x.MedicalRecords!)
                   .HasForeignKey<MedicalRecords>(x=>x.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // MedicalRecords to Patient Relationship
            builder.HasOne(m => m.Patient)
                      .WithMany(p => p.MedicalRecords)
                      .HasForeignKey(m => m.PatientId)
                      .OnDelete(DeleteBehavior.Restrict); //Keep records even if patient is deleted
            // relationship to Doctor
            builder.HasOne(m => m.Doctor)
                      .WithMany(d => d.MedicalRecords)
                      .HasForeignKey(m => m.DoctorId)
                      .OnDelete(DeleteBehavior.Restrict);
           
             

        }
    }
}
