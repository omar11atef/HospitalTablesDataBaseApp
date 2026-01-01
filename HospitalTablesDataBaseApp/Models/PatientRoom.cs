using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public class PatientRoom
    {
        // New Class: PatientRoom.cs
            public int Id { get; set; }
            // Realationships with Patient and Room
            public int PatientId { get; set; }
            public Patients Patient { get; set; } = null!;
            public int RoomId { get; set; }
            public Room Room { get; set; } = null!;
            public DateTime CheckInDate { get; set; }
            public DateTime? CheckOutDate { get; set; } // Nullable because patient might still be in
            public decimal TotalCost { get; set; } // Can be calculated later
    }
}
