
using RoyalTravel.Data.Models;
using System.Collections.Generic;

namespace RoyalTravel.Services.Stays
{
    public interface IStaysService
    {
        void CancelReservation(int stayId);

        public Stay FindStayById(int stayId);

        void UsePoints(int stayId);

        List<Stay> GetAllStaysForUser(string userId);

        int GetTotalPoints(string userId);

        int GetRefundedPoints(string userId);

        public List<Stay> GetEarning(string userId);

        public List<Stay> AllReservations();

    }
}
