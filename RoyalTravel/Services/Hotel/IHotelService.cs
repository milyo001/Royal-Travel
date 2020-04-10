
using RoyalTravel.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoyalTravel.Services.Hotel
{
    public interface IHotelService
    {
        Task<List<Data.Models.Hotel>> SearchWithLocationAsync(string searchLocation);
        Task<List<Data.Models.Hotel>> SearchWithHotelNameAsync(string hotelName);
        Task<List<Data.Models.Hotel>> FindHotelById(int? id);
        Task<Data.Models.Hotel> FindSingleHotelById(int? id);

    }
}
