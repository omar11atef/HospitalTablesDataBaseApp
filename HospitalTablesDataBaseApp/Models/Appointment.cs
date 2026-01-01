using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public enum AppointmentStatus
    {
        Pending = 0,
        Completed = 1,
        Canceled = 2,
        NoShow = 3
    }
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        [MaxLength(100)]
        public string? ReasonForVisit { get; set; }
        [Required]
        public AppointmentStatus Status { get; set; }
        [MaxLength(100)]
        public string? Notes { get; set; }
        // relationship with Doctor
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        // relationship with Patient
        public int PatientId { get; set; }
        public Patients? Patient { get; set; }
        // relationship with MedicalRecords
        public MedicalRecords? MedicalRecords { get; set; }
        //Note : Must One Doctor Has one DateTime 

    }
}
