using HospitalTablesDataBaseApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Querys
{
    public static class AppointmentQuery
    {
        public static void GetAllAppointments(this DbSet<Appointment> Appointment)
        {
            foreach (var appointment in Appointment.Include(a => a.Doctor).Include(a => a.Patient))
            {
                Console.WriteLine($"Appointment ID: {appointment.Id}, Date: {appointment.AppointmentDate}, Doctor: {appointment.Doctor?.Name}, Patient: {appointment.Patient?.Name}");
            }
        }

        public static void GetAppointmentsDay(this DbSet<Appointment> Appointment)
        {
            var context = new ApplacationDBContext();
            var today = DateTime.Today;
            // By Include :
            var dailyAgenda = context.Appointments
                .Where(a => a.AppointmentDate.Date == today)
                .Where(a => a.Status != AppointmentStatus.Canceled)
                .Include(a => a.Doctor)  
                .Include(a => a.Patient)
                .OrderBy(a => a.AppointmentDate) 
                .Select(a => new
                {
                    Time = a.AppointmentDate.ToString("hh:mm tt"), 
                    PatientName = a.Patient != null ? a.Patient.Name ?? "Unknown" : "Unknown",
                    DoctorName = a.Doctor != null ? a.Doctor.Name ?? "Unknown" : "Unknown",
                    Reason = a.ReasonForVisit ?? "N/A",
                    Status = a.Status
                })
                .ToList();

            /* ByJoin :    
             * var dailyAgenda =
                           (from a in context.Appointments
                            join d in context.Doctors on a.DoctorId equals d.Id
                            join p in context.Patients on a.PatientId equals p.Id
                            where a.AppointmentDate.Date == today
                                  && a.Status != AppointmentStatus.Canceled
                            orderby a.AppointmentDate
                            select new
                            {
                                Time = a.AppointmentDate.ToString("hh:mm tt"),
                                PatientName = p.Name,
                                DoctorName = d.Name,
                                Reason = a.ReasonForVisit ?? "N/A",
                                Status = a.Status
                            }).ToList();*/


            foreach (var appointment in dailyAgenda)
            {
                Console.WriteLine($"Time: {appointment.Time}, " +
                    $"Patient: {appointment.PatientName}, " +
                    $"Doctor: {appointment.DoctorName}, " +
                    $"Reason: {appointment.Reason}, " +
                    $"Status: {appointment.Status}");
            }


        }

        public static void IfManyAppointmentONSameTime(this DbSet<Appointment> Appointment, ApplacationDBContext context)
        {
            //var context = new ApplacationDBContext();
            var ConflictingAppointments = context.Appointments
                .GroupBy(a => new { a.DoctorId, a.AppointmentDate })
                .Where(g => g.Count() > 1)
                .Select(g => new
                {
                    DoctorId = g.Key.DoctorId,
                    AppointmentDate = g.Key.AppointmentDate,
                    Count = g.Count(),
                    Appointments = g.ToList()
                });
            foreach (var conflict in ConflictingAppointments)
                {
                var doctor = context.Doctors.Find(conflict.DoctorId);
                Console.WriteLine($"Doctor: {doctor?.Name}, Appointment DateTime: {conflict.AppointmentDate}, Conflicting Appointments Count: {conflict.Count}");
                foreach (var appointment in conflict.Appointments)
                {
                    var patient = context.Patients.Find(appointment.PatientId);
                    Console.WriteLine($"\tAppointment ID: {appointment.Id}, Patient: {patient?.Name}, Status: {appointment.Status}");
                }
            }
        }

        public static void GetHistoryPatientAppointments(this DbSet<Appointment> Appointment, int patientId)
        {
            var context = new ApplacationDBContext();
            var pastAppointments =
                     from a in context.Appointments
                     join d in context.Doctors on a.DoctorId equals d.Id
                     where a.PatientId == patientId
                           && a.AppointmentDate < DateTime.Now
                     orderby a.AppointmentDate descending
                     select new
                     {
                         AppointmentId = a.Id,
                         AppointmentDate = a.AppointmentDate,
                         Doctor = d.Name,
                         Status = a.Status
                     };

            var list = pastAppointments.ToList();
            var TotlalHistory = list.Count();
            foreach (var appointment in pastAppointments)
            {
                Console.WriteLine($"Total Visiting History: {TotlalHistory}" +
                    $"Appointment ID: {appointment.AppointmentId}, " +
                    $"Date: {appointment.AppointmentDate}, " +
                    $"Doctor: {appointment.Doctor}," +
                    $"Status: {appointment.Status}");
            }
        }

        public static void AppointmentsByNationalID (this DbSet<Appointment> Appointment,string NationalId)
        {
            var context = new ApplacationDBContext();
            var PatientNationalId = context.Patients.Select(p => p.NationalId == NationalId).Distinct().FirstOrDefault();
            if (PatientNationalId)
            {
                var appointments = context.Appointments
                    .OrderBy(a => a.AppointmentDate)
                    .ToList();
                foreach (var appointment in appointments)
                {
                    Console.WriteLine($"Appointment ID: {appointment.Id}, " +
                        $"Date: {appointment.AppointmentDate}," +
                        $" Doctor: {appointment.Doctor?.Name}, " +
                        $"Patient: {appointment.Patient?.Name}, " +
                        $"Status: {appointment.Status}");
                }
            }
            else
            {
                Console.WriteLine($"No patient found with National ID: {NationalId}");
            }
        }

        public static void TotalAppointmentsPerDoctor(this DbSet<Appointment> Appointment)
        {
            var context = new ApplacationDBContext();
            var appointmentCounts = context.Appointments
                .GroupBy(a => a.DoctorId)
                .Select(g => new
                {
                    DoctorId = g.Key,
                    AppointmentCount = g.Count()
                })
                .ToList();
            // In addition , Doctor Performance This Month :
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfNextMonth = startOfMonth.AddMonths(1);

            var doctorPerformance =
                from a in context.Appointments
                join d in context.Doctors on a.DoctorId equals d.Id
                where a.AppointmentDate >= startOfMonth
                      && a.AppointmentDate < startOfNextMonth
                      && a.Status == AppointmentStatus.Completed
                group a by d.Name into g
                orderby g.Count() descending
                select new
                {
                    DoctorName = g.Key,
                    CompletedVisits = g.Count(),
                    LastVisitDate = g.Max(a => a.AppointmentDate)
                };

            foreach (var item in appointmentCounts)
            {
                var doctor = context.Doctors.Find(item.DoctorId);
                Console.WriteLine($"Doctor: {doctor?.Name}, Total Appointments: {item.AppointmentCount}");
            }

            foreach (var doc in doctorPerformance)
            {
                Console.WriteLine($"Doctor: {doc.DoctorName}, " +
                    $"Completed Visits This Month: {doc.CompletedVisits}, " +
                    $"Last Visit Date: {doc.LastVisitDate}");
            }
        }   


    }
}
