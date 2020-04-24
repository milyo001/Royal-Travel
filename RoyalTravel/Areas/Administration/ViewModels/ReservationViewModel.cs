
using System;

namespace RoyalTravel.Areas.Administration.ViewModels
{
    public class ReservationsViewModel
    {
        public string RoomType { get; set; }

        public string MoneySpend { get; set; }

        public string ArrivalDate { get; set; }

        public string DepartureDate { get; set; }

        public string PricePerNight { get; set; }

        public string TotalPrice { get; set; }

        public string PointsEarned { get; set; }

        public string HotelName { get; set; }

        public string UserName { get; set; }

        public string ConfirmationNumber { get; set; }

        public string Adults { get; set; }

        public string Children { get; set; }

        public DateTime BookedOn { get; set; }

    }
}
