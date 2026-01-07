using HospitalTablesDataBaseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HospitalTablesDataBaseApp.Querys
{
    public static class RoomQuery
    {
        public static void GetAllRooms(this DbSet<Room> rooms)
        {
            foreach(var room in rooms)
            {
                Console.WriteLine($"Room Id : {room.Id} " +
                    $"CreateAt:{room.CreatedAt} " +
                    $"Room Number :{room.RoomNumber} " +
                    $"IsOccupied{room.IsOccupied}");
            }
        }

        public static void GetRoomsOrdering(this DbSet<Room> rooms)
        {
            var roomOrding = rooms.OrderBy(e => e.RoomNumber);
            foreach (var room in roomOrding)
            {
                Console.WriteLine($"Room Number:{room.RoomNumber} , Room Type: {room.Type}");
            }
        }

        public static void GetRoomActivation(this DbSet<Room> rooms)
        {
            var AvctivationRoom = rooms.Where(x => !x.IsOccupied)
                .GroupBy(x => x.Type)
                .Select(x => new
                {
                    RoomType = x.Key,
                    AvailableCount = x.Count(),
                    Rooms = x.Select(r => r.RoomNumber).ToList(),
                    Price = x.FirstOrDefault() != null ? x.FirstOrDefault()!.PricePerDay : 0m
                }).ToList();

            foreach (var room in AvctivationRoom)
            {
                Console.WriteLine($"Type :{room.RoomType} | " +
                    $"Count: {room.AvailableCount} | " +
                    $"Room :{room.Rooms}| " +
                    $"Price :{room.Price}");
            }
        }
        public static void GetPriceRoomActivationPreDay(this DbSet<Room> rooms)
        {
            var PriceRoomTakeingPerDay = rooms.Where(x => x.IsOccupied)
                .GroupBy(x => x.Type)
                .Select(x => new
                {
                    RoomType = x.Select(x=>x.Type),
                    RoomNumber = x.Select(x=>x.RoomNumber),
                    RoomTotalOccupied = x.Count(),
                    AverageRoomPrice = x.Average(r => r.PricePerDay)
                });
        }

        public static void GetRoomLongNotOccupiedMonth(this DbSet<Room> rooms)

        {
            var oneMonthAgo = DateTime.Now.AddDays(-30);
            var CheckRoomLongMonth = rooms
                .FirstOrDefault(x => !x.IsOccupied && x.LastOpen <= oneMonthAgo);
            Console.WriteLine(CheckRoomLongMonth);
        }

        public static void GetInfoPatientRoom(this DbSet<Room>rooms, string nationid)
        {
            var context = new ApplacationDBContext();
            var RoomPerPatient = context.PatientRooms
                .Where(x => x.Patient.NationalId == nationid)
                .Select(pr => new
                  {
                      PatientName = pr.Patient.Name,
                      RoomType = pr.Room.Type,
                      RoomPricePerDay = pr.Room.PricePerDay,
                      CheckInDate = pr.CheckInDate,
                      CheckOutDate = pr.CheckOutDate,
                      TotalCost = pr.TotalCost
                  })
        .ToList(); ;
            if (!RoomPerPatient.Any())
            {
                Console.WriteLine("No patient found with this National ID.");
                return;
            }

            foreach (var item in RoomPerPatient)
            {
                Console.WriteLine($"Patient Name  : {item.PatientName}");
                Console.WriteLine($"Room Type     : {item.RoomType}");
                Console.WriteLine($"Price / Day   : {item.RoomPricePerDay}");
                Console.WriteLine($"Check-In Date : {item.CheckInDate:yyyy-MM-dd}");
                Console.WriteLine($"Check-Out     : {(item.CheckOutDate.HasValue ? item.CheckOutDate.Value.ToString("yyyy-MM-dd") : "Still In")}");
                Console.WriteLine($"Total Cost    : {item.TotalCost}");
                Console.WriteLine("----------------------------------");
            }



        }

        public static void GetCalcOccupancyStats(this DbSet<Room> rooms)
        {
            var context = new ApplacationDBContext();
            var occupancyStats = context.Rooms
                        .GroupBy(r => r.Type)
                        .Select(g => new
                        {
                            Type = g.Key,
                            TotalRooms = g.Count(),
                            OccupiedCount = g.Count(r => r.IsOccupied),
                            FreeCount = g.Count(r => !r.IsOccupied),
                            // (Occupied / Free) * 100
                            OccupancyRate = Math.Round((double)g.Count(r => r.IsOccupied) / g.Count() * 100, 1) + " %"
                        })
                        .OrderByDescending(x => x.OccupiedCount) 
                        .ToList();
            Console.WriteLine(occupancyStats.MaxBy(c => c.OccupancyRate));

            foreach (var room in occupancyStats)
            {
                Console.WriteLine($"Typr: {room.Type}| Total Rooms: {room.TotalRooms}| Occupied Rate: {room.OccupancyRate}");
            }

        }


    }
}
