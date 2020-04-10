using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTravel.Services.Room
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext db;

        public RoomService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async void AddReservation(Stay stay)
        {
            db.Stays.Add(stay);
            db.SaveChanges();
        }
    }
}
