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
    //public class RoomConfigration : IEntityTypeConfiguration<Room>
    public class RoomConfigration : BaseEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x=>x.Id).HasName("Room_PrimaryKey");
            //builder.Property(x=>x.CreatedAt).HasDefaultValueSql("GETDATE()");
            //builder.Property(x => x.isAvailable)
            //        .HasDefaultValue(true); //
            builder.Property(r => r.PricePerDay)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.RoomNumber)
                 .IsRequired()
                 .HasMaxLength(50);

        }
    }
}
