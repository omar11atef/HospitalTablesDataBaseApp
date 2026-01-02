using HospitalTablesDataBaseApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Querys
{
    public static class PatientsQuery
    {
        public static void InsertNewPatient(this DbSet<Patients> patients, Patients newPatient)
        {
            
            using (var context = new ApplacationDBContext())
            {
                try
                {
                    if (newPatient == null || string.IsNullOrEmpty(newPatient.NationalId))
                    {
                        Console.WriteLine("Error: Invalid patient data. National ID is required.");
                        return;
                    }
                    bool isExists = context.Patients.Any(p => p.NationalId == newPatient.NationalId);
                    if (isExists)
                    {
                        Console.WriteLine($"Warning: A patient with National ID '{newPatient.NationalId}' already exists.");
                        return;
                    }
                    context.Patients.Add(newPatient);
                    context.SaveChanges();
                    Console.WriteLine($"Success: Patient '{newPatient.Name}' added successfully");
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
                {
                    Console.WriteLine($"Database Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"General Error: {ex.Message}");
                }
            }
        }
        public static async Task<bool> UpdatePatientByNationalIdAsync(
            this ApplacationDBContext context,
            string nationalId,
            string? newName = null,
            string? newAddress = null,
            string? newPhoneNumber = null,
            int? newAge = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nationalId))
                    throw new ArgumentNullException(nameof(nationalId));

                // 1. call the patient by NationalId
                var patient = await context.Patients
                    .FirstOrDefaultAsync(p => p.NationalId == nationalId);

                if (patient == null) return false; // Not Found

                bool isUpdated = false;

                // For Update Fields
                if (!string.IsNullOrWhiteSpace(newName) && patient.Name != newName)
                {
                    patient.Name = newName;
                    isUpdated = true;
                }

                if (!string.IsNullOrWhiteSpace(newAddress) && patient.Address != newAddress)
                {
                    patient.Address = newAddress;
                    isUpdated = true;
                }

                if (!string.IsNullOrWhiteSpace(newPhoneNumber))
                {
                    if (!Regex.IsMatch(newPhoneNumber, @"^01[0-2,5]{1}[0-9]{8}$"))
                        throw new ArgumentException("Invalid phone number format.");

                    if (patient.PhoneNumber != newPhoneNumber)
                    {
                        patient.PhoneNumber = newPhoneNumber;
                        isUpdated = true;
                    }
                }

                if (newAge.HasValue && newAge > 0 && newAge < 120 && patient.Age != newAge.Value)
                {
                    patient.Age = newAge.Value;
                    isUpdated = true;
                }

                //save changes if any update occurred
                if (isUpdated)
                {
                    await context.SaveChangesAsync();
                    return true;
                }

                return false; // Not Updated
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating patient: {ex.Message}");
                throw;
            }
        }

        public static async Task<bool> DeletePatientByNationalIdAsync(
             this ApplacationDBContext context,
             string nationalId)
        {
            try
            {
                // (ExecuteDeleteAsync)
                var rowsAffected = await context.Patients
                    .Where(p => p.NationalId == nationalId)
                    .ExecuteDeleteAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting patient: {ex.Message}");
                throw;
            }
        }

        public static void GetAllPatients(this DbSet<Patients> patients)
        {
            using (var context = new ApplacationDBContext())
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}");
                }
            }
        }

        public static void GetPatientWithListDoctors (this DbSet<Patients> patients)
        {
            var context = new ApplacationDBContext();
            var patientsWithDoctors = context.Patients.GroupBy(p => new
            {
                PatientName = p.Name,
                PatientId = p.Id,
                DoctorName = (p.Doctors ?? Enumerable.Empty<Doctor>()).Select(d => d.Name),
                SpatientName = (p.Doctors ?? Enumerable.Empty<Doctor>()).Select(d => d.Specialization)
            });
            foreach (var patient in patientsWithDoctors)
            {
                Console.WriteLine($"Patient ID: {patient.Key.PatientId}, Name: {patient.Key.PatientName}");
                var doctorNames = patient.Key.DoctorName.ToList();
                var specializations = patient.Key.SpatientName.ToList();
                for (int i = 0; i < doctorNames.Count; i++)
                {
                    Console.WriteLine($"  - Doctor: {doctorNames[i]}, Specialization: {specializations[i]}");
                }
            }


        }

        public static void GetDoctorsTreatedPatient (this DbSet<Patients> patients)
        {
            var context = new ApplacationDBContext();
            var DoctorsTreatedPatient = context.Patients
                .SelectMany(p => p.Doctors!, (p, d) => new
                {
                    PatientName = p.Name,
                    DoctorName = d.Name,
                    Specialization = d.Specialization
                });
            foreach (var entry in DoctorsTreatedPatient)
                {
                Console.WriteLine($"Patient: {entry.PatientName}, Doctor: {entry.DoctorName}, Specialization: {entry.Specialization}");
            }
        }

        public static void GetCountLast10Visiting (this DbSet<Patients> patients)
        {
            var context = new ApplacationDBContext();
            var patientVisitCounts = context.Patients
                .OrderBy(p => p.Visit)
                .TakeLast(10);

           foreach (var patient in patientVisitCounts)
            {
                Console.WriteLine($"Name: {patient.Name}, Last 10 Visit: {patient.Visit}");
            }
        }

        public static void GetPatientsByAgeRange(this DbSet<Patients> patients, int minAge, int maxAge)
        {
            using (var context = new ApplacationDBContext())
            {
                var patientsInRange = patients
                    .Where(p => p.Age >= minAge && p.Age <= maxAge)
                    .ToList();
                Console.WriteLine($"Patients aged between {minAge} and {maxAge}:");
                foreach (var patient in patientsInRange)
                {
                    Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}");
                }
            }
        }

        public static void GetPatientsTimeDay(this DbSet<Patients> patients)
        {
            var context = new ApplacationDBContext();
            var cunrrentvisitDay = context.Appointments
                .Where(a => a.AppointmentDate.Date == DateTime.Now.Date && a.Patient != null)
                .OrderBy(a => a.AppointmentDate)
                .Select(a => new
                {
                    PatientName = a.Patient != null ? a.Patient.Name : "Unknown",
                    AppointmentTime = a.AppointmentDate
                })
                .ToList();
            foreach (var appointment in cunrrentvisitDay)
            {
                Console.WriteLine($"Patient: {appointment.PatientName}, Appointment Time: {appointment.AppointmentTime}");
            }
        }

        public static void GetCheckForRegiserNationalID(this DbSet<Patients> patients, string nationalID)
        {
            using (var context = new ApplacationDBContext())
            {
                var patientExists = patients
                    .Any(p => p.NationalId == nationalID);
                if (patientExists)
                {
                    Console.WriteLine($"Patient with National ID '{nationalID}' is already registered.");
                }
                else
                {
                    Console.WriteLine($"No patient found with National ID '{nationalID}'.");
                }
            }
        }

        public static void GetHistoryInformationPatient(this DbSet<Patients> patients, string nationalID)
        {
            var context = new ApplacationDBContext();
            var patientHistory = context.Patients
                .Where(p => p.NationalId == nationalID)
                .Select(p => new
                {
                    PatientName = p.Name,
                    Age = p.Age,
                    Doctors = p.Doctors!.Select(d => new
                    {
                        DoctorName = d.Name,
                        Specialization = d.Specialization
                    }),
                    Appointments = p.Appointments!.Select(a => new
                    {
                        AppointmentDate = a.AppointmentDate,
                        Notes = a.Notes
                    })
                })
                .FirstOrDefault();
            if (patientHistory != null)
            {
                Console.WriteLine($"Patient Name: {patientHistory.PatientName}, Age: {patientHistory.Age}");
                Console.WriteLine("Doctors:");
                foreach (var doctor in patientHistory.Doctors)
                {
                    Console.WriteLine($"  - Name: {doctor.DoctorName}, Specialization: {doctor.Specialization}");
                }
                Console.WriteLine("Appointments:");
                foreach (var appointment in patientHistory.Appointments)
                {
                    Console.WriteLine($"  - Date: {appointment.AppointmentDate}, Notes: {appointment.Notes}");
                }
            }
            else
            {
                Console.WriteLine($"No patient found with National_ID '{nationalID}'.");
            }
        }
    }
}
