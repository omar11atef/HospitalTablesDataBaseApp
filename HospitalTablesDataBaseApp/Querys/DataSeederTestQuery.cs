using HospitalTablesDataBaseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Querys
{
    public static class DataSeederTestQuery
    {
        public static void Seed(ApplacationDBContext context)
        {
            context.Database.EnsureCreated();
            /*if (!context.Departments.Any())
            {
                var departments = new List<Departments>
            {
                new Departments { Name = "Cardiology", Location = "Building A, Floor 1", PhoneNumber = "01000000001" },
                new Departments { Name = "Neurology", Location = "Building A, Floor 2", PhoneNumber = "01000000002" },
                new Departments { Name = "Orthopedics", Location = "Building B, Floor 1", PhoneNumber = "01000000003" },
                new Departments { Name = "Pediatrics", Location = "Building B, Floor 2", PhoneNumber = "01000000004" },
                new Departments { Name = "Dermatology", Location = "Building C, Floor 1", PhoneNumber = "01000000005" },
                new Departments { Name = "Ophthalmology", Location = "Building C, Floor 2", PhoneNumber = "01000000006" },
                new Departments { Name = "Emergency", Location = "Ground Floor", PhoneNumber = "01000000007" },
                new Departments { Name = "Surgery", Location = "Building D, Floor 3", PhoneNumber = "01000000008" },
                new Departments { Name = "Internal Medicine", Location = "Building A, Floor 3", PhoneNumber = "01000000009" },
                new Departments { Name = "Psychiatry", Location = "Building E, Floor 1", PhoneNumber = "01000000010" }
            };
                context.Departments.AddRange(departments);
                context.SaveChanges();
                Console.WriteLine(" Departments seeded.");
            }*/

            // (Doctors)
            if (!context.Doctors.Any())
            {
                var deptIds = context.Departments.Select(d => d.Id).ToList();

                var doctors = new List<Doctor>
            {
                new Doctor { Name = "Dr. Ahmed Ali", NationalId = "28010101111111", PhoneNumber = "01011111111", Specialization = "Cardiologist", DepartmentId = deptIds[0], TotalHoursWorked = 40 , AcademicDegree="A"},
                new Doctor { Name = "Dr. Sara Mohamed", NationalId = "28510101111112", PhoneNumber = "01011111112", Specialization = "Neurologist", DepartmentId = deptIds[1], TotalHoursWorked = 35 ,AcademicDegree="B" },
                new Doctor { Name = "Dr. Khaled Omar", NationalId = "29010101111113", PhoneNumber = "01011111113", Specialization = "Orthopedic Surgeon", DepartmentId = deptIds[2], TotalHoursWorked = 50,AcademicDegree="-B" },
                new Doctor { Name = "Dr. Mona Youssef", NationalId = "29210101111114", PhoneNumber = "01011111114", Specialization = "Pediatrician", DepartmentId = deptIds[3], TotalHoursWorked = 30,AcademicDegree="-B" },
                new Doctor { Name = "Dr. Omar Hassan", NationalId = "28810101111115", PhoneNumber = "01011111115", Specialization = "Dermatologist", DepartmentId = deptIds[4], TotalHoursWorked = 25 , AcademicDegree = "+A"},
                new Doctor { Name = "Dr. Huda Nabil", NationalId = "28910101111116", PhoneNumber = "01011111116", Specialization = "Ophthalmologist", DepartmentId = deptIds[5], TotalHoursWorked = 20 , AcademicDegree = "+B"},
                new Doctor { Name = "Dr. Tarek Salah", NationalId = "28110101111117", PhoneNumber = "01011111117", Specialization = "ER Doctor", DepartmentId = deptIds[6], TotalHoursWorked = 60 , AcademicDegree = "+C"},
                new Doctor { Name = "Dr. Rania Mahmoud", NationalId = "29510101111118", PhoneNumber = "01011111118", Specialization = "General Surgeon", DepartmentId = deptIds[7], TotalHoursWorked = 45 , AcademicDegree = "-A"},
                new Doctor { Name = "Dr. Yasser Fathy", NationalId = "28310101111119", PhoneNumber = "01011111119", Specialization = "Internist", DepartmentId = deptIds[8], TotalHoursWorked = 38 ,AcademicDegree="-B"},
                new Doctor { Name = "Dr. Layla Samir", NationalId = "29110101111120", PhoneNumber = "01011111120", Specialization = "Psychiatrist", DepartmentId = deptIds[9], TotalHoursWorked = 32 , AcademicDegree = "+B"}
            };
                context.Doctors.AddRange(doctors);
                context.SaveChanges();
                Console.WriteLine("Doctors seeded.");
            }

            //(Patients)
            if (!context.Patients.Any())
            {
                var patients = new List<Patients>
            {
                new Patients { Name = "Ali Hassan", NationalId = "30001011111111", Age = 25, PhoneNumber = "01222222221", Address = "Cairo" , Gender=Gender.male },
                new Patients { Name = "Fatma Ibrahim", NationalId = "30101011111112", Age = 30,PhoneNumber = "01222222222", Address = "Giza", Gender=Gender.female},
                new Patients { Name = "Mahmoud Saied", NationalId = "30201011111113", Age = 45, PhoneNumber = "01222222223", Address = "Alexandria",Gender=Gender.male },
                new Patients { Name = "Noha Adel", NationalId = "30301011111114", Age = 28,PhoneNumber = "01222222224", Address = "Tanta",Gender=Gender.female },
                new Patients { Name = "Kareem Ezzat", NationalId = "30401011111115", Age = 50,  PhoneNumber = "01222222225", Address = "Mansoura",Gender=Gender.male },
                new Patients { Name = "Salma Hany", NationalId = "30501011111116", Age = 22, PhoneNumber = "01222222226", Address = "Cairo" ,Gender=Gender.female},
                new Patients { Name = "Samir Mounir", NationalId = "30601011111117", Age = 35, PhoneNumber = "01222222227", Address = "Giza" , Gender = Gender.male},
                new Patients { Name = "Dina Kamel", NationalId = "30701011111118", Age = 29, PhoneNumber = "01222222228", Address = "Fayoum",Gender=Gender.female },
                new Patients { Name = "Hassan Zaki", NationalId = "30801011111119", Age = 60, PhoneNumber = "01222222229", Address = "Aswan",Gender=Gender.male },
                new Patients { Name = "Reem Ahmed", NationalId = "30901011111120", Age = 24, PhoneNumber = "01222222230", Address = "Luxor" , Gender = Gender.female}
            };
                context.Patients.AddRange(patients);
                context.SaveChanges();
                Console.WriteLine("Patients seeded.");
            }

            // (Rooms)
            if (!context.Rooms.Any())
            {
                var rooms = new List<Room>();
                for (int i = 1; i <= 10; i++)
                {
                    rooms.Add(new Room
                    {
                        RoomNumber = $"R-10{i}",
                        Type = i % 2 == 0 ? "Single" : "Double",
                        PricePerDay = i % 2 == 0 ? 500 : 300,
                        IsOccupied = false
                    });
                }
                context.Rooms.AddRange(rooms);
                context.SaveChanges();
                Console.WriteLine("Rooms seeded.");
            }

            // (Appointments) 
            if (!context.Appointments.Any())
            {
                var doctors = context.Doctors.ToList();
                var patients = context.Patients.ToList();

                if (doctors.Any() && patients.Any())
                {
                    var appointments = new List<Appointment>();
                    for (int i = 0; i < 10; i++)
                    {
                        appointments.Add(new Appointment
                        {
                            AppointmentDate = DateTime.Now.AddDays(i + 1),
                            Status = AppointmentStatus.Pending,
                            DoctorId = doctors[i % doctors.Count].Id,   
                            PatientId = patients[i % patients.Count].Id 
                        });
                    }
                    context.Appointments.AddRange(appointments);
                    context.SaveChanges();
                    Console.WriteLine("Appointments seeded.");
                }
            }

            // (MedicalRecords)
            if (!context.MedicalRecords.Any())
            {
                var appointments = context.Appointments.ToList();

                if (appointments.Any())
                {
                    var records = new List<MedicalRecords>();
                    foreach (var app in appointments)
                    {
                        records.Add(new MedicalRecords
                        {
                            Diagnosis = "Common Cold / Flu Symptoms",
                            VisitDate = app.AppointmentDate,
                            Notes = "Patient advised to rest and drink fluids.",
                            AppointmentId = app.Id,
                            DoctorId = app.DoctorId,  
                            PatientId = app.PatientId  
                        });
                    }
                    context.MedicalRecords.AddRange(records);
                    context.SaveChanges();
                    Console.WriteLine("MedicalRecords seeded.");
                }
            }

            // (Prescriptions)
            if (!context.Prescriptions.Any())
            {
                var records = context.MedicalRecords.ToList();

                if (records.Any())
                {
                    var prescriptions = new List<Prescription>();

                    foreach (var rec in records)
                    {
                        prescriptions.Add(new Prescription
                        {
                            MedicationName = "Panadol Extra",
                            Dosage = "500mg",
                            Duration = "3 times daily for 5 days",
                            MedicalRecordId = rec.Id
                        });

                        if (rec.Id % 2 == 0)
                        {
                            prescriptions.Add(new Prescription
                            {
                                MedicationName = "Vitamin C",
                                Dosage = "1000mg",
                                Duration = "Once daily",
                                MedicalRecordId = rec.Id
                            });
                        }
                    }
                    context.Prescriptions.AddRange(prescriptions);
                    context.SaveChanges();
                    Console.WriteLine("Prescriptions seeded.");
                }
            }

            // (PatientRooms)
            if (!context.PatientRooms.Any())
            {
                var patients = context.Patients.Take(5).ToList();
                var rooms = context.Rooms.Where(r => !r.IsOccupied).Take(5).ToList(); // Empty rooms

                if (patients.Count > 0 && rooms.Count > 0)
                {
                    var patientRooms = new List<PatientRoom>();

                    for (int i = 0; i < Math.Min(patients.Count, rooms.Count); i++)
                    {
                        var checkIn = DateTime.Now.AddDays(-5);
                        var checkOut = DateTime.Now.AddDays(-1); 
                        var days = (checkOut - checkIn).Days;

                        patientRooms.Add(new PatientRoom
                        {
                            PatientId = patients[i].Id,
                            RoomId = rooms[i].Id,
                            CheckInDate = checkIn,
                            CheckOutDate = checkOut,
                            TotalCost = days * rooms[i].PricePerDay //clac total cost
                        });
                    }
                    context.PatientRooms.AddRange(patientRooms);
                    context.SaveChanges();
                    Console.WriteLine("PatientRooms seeded.");
                }
            }

            // (Invoices & InvoiceItems)
            if (!context.Invoices.Any())
            {
                var patients = context.Patients.ToList();

                if (patients.Any())
                {
                    var invoices = new List<Invoice>();

                    foreach (var patient in patients)
                    {
                        // create invoice
                        var inv = new Invoice
                        {
                            DateIssued = DateTime.Now,
                            IsPaid = false,
                            PatientId = patient.Id,
                            TotalAmount = 0 
                        };

                        //(Items)
                        var items = new List<InvoiceItem>
                    {
                        new InvoiceItem { ServiceName = "Doctor Consultation", Price = 200, Invoice = inv },
                        new InvoiceItem { ServiceName = "Blood Test", Price = 150, Invoice = inv }
                    };

                        // total amount
                        inv.TotalAmount = items.Sum(i => i.Price);
                        inv.Items = items; 
                        invoices.Add(inv);
                    }

                    context.Invoices.AddRange(invoices);
                    context.SaveChanges();
                    Console.WriteLine("Invoices & Items seeded.");
                }
            }
        }
    }
}
