using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false; // Soft Delete
    }
    public interface IDoctorPrinter
    {
        void DisplayDoctorInfo(Doctor doctor);
    }

    public class Doctor : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        [Required]
        public string? AcademicDegree { get; set; }
        public decimal TotalHoursWorked { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "pleas enter your NationalId")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "that Must 14 number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "only Numbers")]
        public string NationalId { get; set; } = null!;


        //relationships
        // One Doctor belongs to one Department
        public int DepartmentId { get; set; }
        public Departments? Department { get; set; }
        // One Doctor has many Appointments
        public ICollection<Appointment>? Appointments { get; set; }
        // One Docotr has many MedicalRecords 
        public ICollection<MedicalRecords> MedicalRecords { get; set; }
        = new HashSet<MedicalRecords>();
        // Many Doctors have many Patients
        public ICollection<Patients>? Patients { get; set; }


    }

    public class PrintInfoDoctor: IDoctorPrinter
    {
        public void DisplayDoctorInfo(Doctor doctor)
        {
            
        }
    }
    /* Used in Main :
    Doctor doctor = new Doctor
    {
        Id = 1,
        Name = "Dr. Ahmed",
        Age = 40,
        Specialization = "Cardiology"
    };
    IDoctorPrinter printer = new PrintInfoDoctor();
    printer.DisplayDoctorInfo(doctor);
     */
}

