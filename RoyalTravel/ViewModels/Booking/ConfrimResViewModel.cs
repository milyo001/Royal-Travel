

using System;

namespace RoyalTravel.ViewModels.Booking
{
    public class ConfrimResViewModel
    {
        public int StayId { get; set; }

        public string HotelName { get; set; }

        public string ConfirmationNumber { get; set; }

        public string CheckIn { get; set; }

        public string CheckOut { get; set; }

        public string CheckInTime { get; set; }

        public string CheckOutTime { get; set; }

        public string Adults { get; set; }

        public string Children { get; set; }

        public string Nights { get; set; }

        public DateTime BookedOn { get; set; }

        public bool IsCanceled { get; set; }

    }
}
