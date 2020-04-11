
using RoyalTravel.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoyalTravel.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace RoyalTravel.Services.Hotel
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext db;

        public HotelService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Data.Models.Hotel>> SearchWithLocationAsync(string searchLocation)
        {
            var result = this.db.Hotels
              .Where(h => h.Address.City.Contains(searchLocation) || searchLocation == null)
              .Include(h => h.Address)
              .Include(h => h.Amenity)
              .Include(h => h.Info)
              .Take(20)
              .ToList();
            return await Task.FromResult(result);
        }

        public async Task<List<Data.Models.Hotel>> SearchWithHotelNameAsync(string hotelName)
        {
            var result = this.db.Hotels
               .Where(h => h.Name.Contains(hotelName) || hotelName == null)
               .Include(h => h.Address)
               .Include(h => h.Amenity)
               .Include(h => h.Info)
               .Include(h=> h.Rooms)
               .Take(20)
               .ToList();
            return await Task.FromResult(result);
        }
        

        public async Task<List<Data.Models.Hotel>> FindHotelById(int? id)
        {
            var hotel = db.Hotels
                .Where(h => h.Id == id)
                .Include(h=> h.Address)
                .Include(h => h.Amenity)
                .Include(h => h.Info)
                .Include(h => h.Rooms)
                .Include(h => h.Reviews)
                .Include(h => h.Stays)
                .ToList();

            return await Task.FromResult(hotel);
        }

        public async Task<Data.Models.Hotel> FindSingleHotelById(int? id)
        {
            var hotel = db.Hotels
                .Include(h => h.Stays)
                .Include(h => h.Address)
                .Include(h => h.Rooms)
                .Include(h => h.Info)
                .FirstOrDefault(h => h.Id == id);
                
            return await Task.FromResult(hotel);
        }

    }
}
