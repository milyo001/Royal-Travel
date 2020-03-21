﻿
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.ViewModels.Booking
{
    
    public class BookingOutputViewModel
    {
        public string HotelName { get; set; }

        public string Address { get; set; }

        public string TotalRooms { get; set; }

        public string Rating { get; set; }

        public string AveragePrice { get; set; }

        public string PointsCost { get; set; }

        public string Wifi { get; set; }

        public string Pool { get; set; }

        public string PetFriendly { get; set; }


    }
}
