
using Microsoft.AspNetCore.Mvc;
using RoyalTravel.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.ViewModels.Booking
{
    
    public class BookingOutputViewModel
    {

        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public string Address { get; set; }

        public List<Room> AvailableRooms { get; set; }

        public string AveragePrice { get; set; }

        public string PointsCost { get; set; }

        public string Wifi { get; set; }

        public string Pool { get; set; }

        public string PetFriendly { get; set; }

        public int Stars { get; set; }

        public byte[] Image { get; set; }


    }
}
