

using System;

namespace RoyalTravel.ViewModels
{
    public class StayViewModel
    {
        public string ConfirmationNumber { get; set; }
        public int StayId { get; set; }

        public string RoomType { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public int? PointsEarned { get; set; }

        public int? PointsSpend { get; set; }

        public string Hotel { get; set; }

        public string Room { get; set; }

        public DateTime BookedOn { get; set; }

        public bool IsCanceled { get; set; }
    }
}
