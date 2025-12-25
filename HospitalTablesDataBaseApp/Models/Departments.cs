using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public class Departments
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(100)]
        public string? Location { get; set; }
        [MaxLength(100)]
        public string? PhoneNumber { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
