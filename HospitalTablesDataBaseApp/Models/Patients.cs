using System;
using System.Collections.Generic;
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
    public class Patients
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime Visit { get; set; }
        public bool IsActive { get; set; }
        public decimal Max_Medical_Expenses { get; set; }   
        public string? NameofDisease { get; set; }

    }
}
