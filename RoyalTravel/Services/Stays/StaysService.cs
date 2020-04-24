
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using System;
using System.Collections.Generic;
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

        public List<Stay> AllReservations()
        {
            var allReservations = db.Stays
                .OrderBy(s => s.BookedOn)
                .ToList();
            return allReservations;
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

        public List<Stay> GetAllStaysForUser(string userId)
        {
            var result = db.Stays
                 .Where(s => s.ApplicationUserId == userId)
                 .Include(s => s.Hotel)
                 .OrderByDescending(s => s.BookedOn)
                 .ToList();

            return result;
        }

        public List<Stay> GetEarning(string userId)
        {
            var earningStays = db.Stays
                .Where(s => s.ApplicationUserId == userId && s.IsCanceled == false && s.DepartureDate < DateTime.Today)
                .ToList();
            return earningStays;
            //Returns only stays which are already expired
        }

        public int GetRefundedPoints(string userId)
        {
            var result = (int)db.Stays
                .Where(s => s.IsCanceled && s.PointsSpend > 0)
                .Sum(s => s.PointsSpend);
            return result;
        }

        public int GetTotalPoints(string userId)
        {
            var result = (int)db.Stays
                .Where(s => s.ApplicationUserId == userId
                && s.DepartureDate < DateTime.Today && s.IsCanceled == false)
                .Sum(s => s.TotalPrice * StaticData.PointsMultiplier);
            return result;
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
