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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(i => i.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            // relationship with Patient
            builder.HasOne(i => i.Patient)
                  .WithMany() 
                  .HasForeignKey(i => i.PatientId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.Property(item => item.Price)
                   .HasColumnType("decimal(18,2)");

            // العلاقة: البند يتبع فاتورة واحدة
            builder.HasOne(item => item.Invoice)
                  .WithMany(inv => inv.Items)
                  .HasForeignKey(item => item.InvoiceId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
