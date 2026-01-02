using HospitalTablesDataBaseApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Querys
{
    public static class DoctorQuery
    {

        public static void InsertNewDoctor(this DbSet<Doctor> doctors, ApplacationDBContext context, Doctor newDoctor)
        {
            try
            {
                if (newDoctor == null)
                {
                    Console.WriteLine("Error: Doctor data cannot be null.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(newDoctor.NationalId))
                {
                    Console.WriteLine("Error: National ID is required.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(newDoctor.Name))
                {
                    Console.WriteLine("Error: Doctor Name is required.");
                    return;
                }

                // 2. Business Logic:
                bool isExists = doctors.Any(d => d.NationalId == newDoctor.NationalId);

                if (isExists)
                {
                    Console.WriteLine($"Warning: A doctor with National ID '{newDoctor.NationalId}' already exists.");
                    return;
                }

                // Integrity Check: 
                bool departmentExists = context.Departments.Any(d => d.Id == newDoctor.DepartmentId);
                if (!departmentExists)
                {
                    Console.WriteLine($"❌ Error: Department with ID '{newDoctor.DepartmentId}' does not exist.");
                    return;
                }

                // Add & Save
                doctors.Add(newDoctor);
                context.SaveChanges();

                Console.WriteLine($"Success: Doctor '{newDoctor.Name}' added successfully.");
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
        public static async Task<bool> UpdateDoctorByNationalIdAsync(
            this ApplacationDBContext context,
            string currentNationalId, 
            string? newName = null,
            string? newNationalId = null, 
            int? newDepartmentId = null)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(currentNationalId))
                    throw new ArgumentNullException(nameof(currentNationalId));

                // 1. Find the Doctor
                var doctor = await context.Doctors
                    .FirstOrDefaultAsync(d => d.NationalId == currentNationalId);

                if (doctor == null) return false; // Not Found

                bool isUpdated = false;

                // 2. Update Name
                if (!string.IsNullOrWhiteSpace(newName) && doctor.Name != newName)
                {
                    doctor.Name = newName;
                    isUpdated = true;
                }

                // 3. Update National ID (Complex Logic: Check Uniqueness)
                if (!string.IsNullOrWhiteSpace(newNationalId) && doctor.NationalId != newNationalId)
                {
                    // يجب التأكد أن الرقم الجديد غير محجوز لطبيب آخر
                    bool isTaken = await context.Doctors
                        .AnyAsync(d => d.NationalId == newNationalId && d.Id != doctor.Id);

                    if (isTaken)
                    {
                        Console.WriteLine($"Warning: National ID '{newNationalId}' is already taken.");
                        return false; // فشل التحديث بسبب التكرار
                    }

                    doctor.NationalId = newNationalId;
                    isUpdated = true;
                }

                // 4. Update Department (Complex Logic: Check Existence)
                if (newDepartmentId.HasValue && doctor.DepartmentId != newDepartmentId.Value)
                {
                    // يجب التأكد أن القسم الجديد موجود في قاعدة البيانات
                    bool deptExists = await context.Departments
                        .AnyAsync(d => d.Id == newDepartmentId.Value);

                    if (!deptExists)
                    {
                        Console.WriteLine($"Warning: Department ID '{newDepartmentId}' does not exist.");
                        return false;
                    }

                    doctor.DepartmentId = newDepartmentId.Value;
                    isUpdated = true;
                }

                // 5. Save Changes
                if (isUpdated)
                {
                    await context.SaveChangesAsync();
                    return true;
                }

                return false; // No changes detected
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating doctor: {ex.Message}");
                throw;
            }
        }

        public static async Task<bool> DeleteDoctorByNationalIdAsync(
                this ApplacationDBContext context,
                string nationalId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nationalId))
                    throw new ArgumentNullException(nameof(nationalId));
                var rowsAffected = await context.Doctors
                    .Where(d => d.NationalId == nationalId)
                    .ExecuteDeleteAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting doctor: {ex.Message}");
                throw;
            }
        }

        public static void GetAllDoctors(this DbSet<Doctor> doctors)
        {
            using (var context = new ApplacationDBContext())
            {
                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Specialty: {doctor.Specialization}");
                }
            }
        }
        public static void GetMaxHoursWorkedDoctor(this DbSet<Doctor> doctors)
        {
            using (var context = new ApplacationDBContext())
            {
                var maxHoursDoctor = doctors
                    .OrderByDescending(d => d.TotalHoursWorked)
                    .FirstOrDefault();
                if (maxHoursDoctor != null)
                {
                    Console.WriteLine($"Doctor with Max Hours Worked: ID: {maxHoursDoctor.Id}, Name: {maxHoursDoctor.Name}, Hours Worked: {maxHoursDoctor.TotalHoursWorked}");
                }
                else
                {
                    Console.WriteLine("No doctors found.");
                }
            }
        }

        public static void GetDoctorsinDepartments(this DbSet<Doctor> doctors)
        {
            using (var context = new ApplacationDBContext())
            {
                var doctorsWithDepartments = context.Doctors
                    .Where(d => d.Department != null && d.Department.Name != null)
                    .GroupBy(d => d.Department!.Name!)
                    .Select(g => new
                    {
		                DepartmentName = g.Key,
		                DoctorCount = g.Count(),
		                DoctorName = g.Select(d => d.Name)
                    })
                    .ToList();
                foreach (var dept in doctorsWithDepartments)
                {
                    Console.WriteLine($"Department Name: {dept.DepartmentName} - numbers Doctors: {dept.DoctorCount}");
                    foreach (var name in dept.DoctorName)
                    {
                        Console.WriteLine($"  - {name}");
                    }
                }
            }
        }

        public static void GetDoctorsBySpecialization(this DbSet<Doctor> doctors, string specialization)
        {
            using (var context = new ApplacationDBContext())
            {
                var specializedDoctors = doctors
                    .Where(d => d.Specialization == specialization)
                    .ToList();
                Console.WriteLine($"Doctors with Specialization '{specialization}':");
                foreach (var doctor in specializedDoctors)
                {
                    Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}");
                }
            }
        }

        public static void GetDoctorsForPatient(this DbSet<Doctor> doctors, int patientId)
        {
            using (var context = new ApplacationDBContext())
            {
                var doctorNames = context.Appointments
                    .Where(a => a.PatientId == patientId && a.Doctor != null && a.Doctor.Name != null)
                    .Select(a => a.Doctor!.Name!)
                    .Distinct()
                    .ToList();

                Console.WriteLine($"Doctors for Patient ID {patientId}:");
                foreach (var name in doctorNames)
                {
                    Console.WriteLine(name);
                }
            }
        }

        public static void OrderDoctorsByDegree(this DbSet<Doctor> doctors)
        {
            using (var context = new ApplacationDBContext())
            {
                var orderedDoctors = doctors
                    .OrderBy(d => d.AcademicDegree)
                    .ToList();
                Console.WriteLine("Doctors ordered by Academic Degree:");
                foreach (var doctor in orderedDoctors)
                {
                    Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Degree: {doctor.AcademicDegree}");
                }
            }
        }

        public static void GetTopDoctorsInDepartment(this DbSet<Doctor> doctors, int departmentId, int topN)
        {
            using (var context = new ApplacationDBContext())
            {
                var topDoctors = doctors
                    .Where(d => d.DepartmentId == departmentId)
                    .OrderByDescending(d => d.TotalHoursWorked)
                    .Take(topN)
                    .ToList();
                Console.WriteLine($"Top {topN} Doctors in Department ID {departmentId} by Hours Worked:");
                foreach (var doctor in topDoctors)
                {
                    Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Hours Worked: {doctor.TotalHoursWorked}");
                }
            }
        }

    }
}
