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

        public int PointsEarned { get => (int)this.Price * 10; }
        //For all dollar the user will get 10 points

        public int PointsSpend { get; set; }

        public string Hotel { get; set; }

        public string Room { get; set; }
    }
}
