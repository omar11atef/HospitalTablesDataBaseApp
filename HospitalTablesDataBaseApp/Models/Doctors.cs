using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public interface IDoctorPrinter
    {
        void DisplayDoctorInfo(Doctor doctor);
    }

    public class Doctor
    {
        public int Id { get; set; }
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
        public bool IsAvailable { get; set; }

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

