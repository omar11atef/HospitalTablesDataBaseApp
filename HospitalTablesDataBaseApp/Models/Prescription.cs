using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public string? MedicationName { get; set; } 
        public string? Dosage { get; set; } 
        public string? Duration { get; set; } // time period for medication

        // relationship to MedicalRecords
        public int MedicalRecordId { get; set; }
        public MedicalRecords? MedicalRecord { get; set; }
    }
}
