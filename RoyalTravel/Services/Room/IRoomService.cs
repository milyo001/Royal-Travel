
using RoyalTravel.Data;
using RoyalTravel.Data.Models;

namespace RoyalTravel.Services.Room
{
    public interface IRoomService
    {
        void AddReservation(Stay stay);

        public string GenerateConfirmationNumber(string hotelName);

    }
}
