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
    public class RoomConfigration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x=>x.Id).HasName("Room_PrimaryKey");
            builder.Property(x=>x.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.isAvailable)
                    .HasDefaultValue(true); //

            builder.Property(x => x.PricePerDay)
                    .HasComputedColumnSql(
           "DATEDIFF(DAY, [CreatedAt], [LastOpen]) * 0.5");

        }
    }
}
