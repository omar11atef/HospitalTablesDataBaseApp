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
    public class PatientRoomConfiguration : IEntityTypeConfiguration<PatientRoom>
    {
        public void Configure(EntityTypeBuilder<PatientRoom> builder)
        {
            // Primary Key
            builder.HasKey(pr => pr.Id);
        
            //(Patient -> PatientRooms)
            builder.HasOne(pr => pr.Patient)
                  .WithMany(p => p.PatientRooms)
                  .HasForeignKey(pr => pr.PatientId)
                  .OnDelete(DeleteBehavior.Restrict);

            // (Room -> PatientRooms)
            builder.HasOne(pr => pr.Room)
                  .WithMany(r => r.PatientRooms)
                  .HasForeignKey(pr => pr.RoomId)
                  .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
