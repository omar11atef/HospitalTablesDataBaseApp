using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public enum Gender
    {
        female=0,
        male=1,
    }
    public class Patients : BaseEntity
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public DateTime Visit { get; set; } = DateTime.Now;
        public decimal Max_Medical_Expenses { get; set; }   
        public string? NameofDisease { get; set; }

        [Required(ErrorMessage = "pleas enter your NationalId")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "that Must 14 number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "only Numbers")]
        public string NationalId { get; set; } = null!;

        public ICollection<Appointment>? Appointments { get; set; }

        //relastionship with Room :
        public ICollection<PatientRoom>? PatientRooms { get; set; }
        //relastionship with Docoter :
        public ICollection<Doctor>? Doctors { get; set; }
        //relastionship with MedicalRecords :
        public ICollection<MedicalRecords>? MedicalRecords { get; set; }
    }
}
