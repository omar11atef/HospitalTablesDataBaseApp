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
    public  class AppointmentConfigration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id).HasName("Appointment_PrimaryKey");
            builder.Property(x => x.AppointmentDate).IsRequired();
            builder.Property(x => x.Notes).HasDefaultValue("Not comment");
            
        }
    }
}
