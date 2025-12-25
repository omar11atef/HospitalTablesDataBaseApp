using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public class MedicalRecords
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string? Diagnosis { get; set; }
        public DateTime VisitDate { get; set; }
        [MaxLength(100)]
        public string? Notes { get; set; }
        // relationship to Appointment
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        // relationship to Doctor
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}
