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
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasOne(p => p.MedicalRecord)
                   .WithMany(m => m.Prescriptions) 
                   .HasForeignKey(p => p.MedicalRecordId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
