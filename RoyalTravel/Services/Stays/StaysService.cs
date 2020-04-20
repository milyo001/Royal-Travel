
using Microsoft.AspNetCore.Identity;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTravel.Services.Stays
{
    public class StaysService : IStaysService
    {
        private readonly ApplicationDbContext db;

        public StaysService(ApplicationDbContext db)
        {
            this.db = db;
            
        }

        public void CancelReservation(int stayId)
        {
            var stay = this.FindStayById(stayId);
            stay.IsCanceled = true;
            db.SaveChanges();
        }

        public Stay FindStayById(int stayId)
        {
            var stay = db.Stays.First(s => s.Id == stayId);
            return stay;
        }

        public void UsePoints(int stayId)
        {
            var selectedStay = FindStayById(stayId);
            selectedStay.PointsSpend += StaticData.FreeNightPoints;
            
            selectedStay.TotalPrice -= selectedStay.Price;
                //Remove one night charge from the total price
            
            //Check if the total price is greater than or equal to the price per night
            db.SaveChanges();
        }
    }
}
