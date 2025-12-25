using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? RoomNumber { get; set; }
        public string? Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastOpen { get; set; }
        public bool IsOccupied { get; set; }
        public decimal PricePerDay { get; set; }  
        public bool isAvailable { get; set; }

        // Relationship With Partient
        public ICollection<Patients>? Patients { get; set; }
    }
}
