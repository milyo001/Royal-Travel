
using RoyalTravel.Data.Models;

namespace RoyalTravel.Services.Stays
{
    public interface IStaysService
    {
        void CancelReservation(int stayId);

        public Stay FindStayById(int stayId);
    }
}
