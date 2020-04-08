
using RoyalTravel.Data.Models;
using System;
using System.Collections.Generic;

namespace RoyalTravel.ViewModels.Booking
{
    public class MakeBookingViewModel
    {
        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public string CheckIn { get; set; }

        public string CheckOut { get; set; }

        public int Adults { get; set; }

        public int Children { get; set; }

        public int NumberOfNights { get; set; }

        public IEnumerable<Room> Rooms { get; set; }



    }
}
