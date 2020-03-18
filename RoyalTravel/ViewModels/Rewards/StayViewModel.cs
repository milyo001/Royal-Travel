using RoyalTravel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTravel.ViewModels
{
    public class StayViewModel
    {
        public string RoomType { get; set; }

        public decimal Price { get; set; }

        public string ArrivalDate { get; set; }

        public string DepartureDate { get; set; }
        public double Rate { get; set; }

        public int PointsEarned { get => (int)this.Price * ConstData.PointsMultiplier; }
        //For each currency(1 BGN, 1EUR, etc) the user will get 10 points(by default) 

        public int PointsSpend { get; set; }

        public string Hotel { get; set; }

        public string Room { get; set; }
    }
}
