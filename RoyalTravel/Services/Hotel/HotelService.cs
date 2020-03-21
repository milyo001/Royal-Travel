
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
               .ToList();
            return result;
        }

        public async Task<List<Data.Models.Hotel>> SearchWithHotelNameAsync(string hotelName)
        {
            var result = this.db.Hotels
               .Where(h => h.Name.Contains(hotelName) || hotelName == null)
               .Include(h => h.Address)
               .Include(h => h.Amenity)
               .Include(h => h.Info)
               .ToList();
            return result;
        }

    }
}
