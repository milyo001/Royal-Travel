
using RoyalTravel.Data.Models;
using System.Collections.Generic;

namespace RoyalTravel.ViewModels.Booking
{
    public class SelectedHotelViewModel
    {
        public string HotelName { get; set; }

        public bool WiFi { get; set; }
               
        public bool AllowPets { get; set; }
               
        public bool Parking { get; set; }
               
        public bool AirportShuttle { get; set; }
               
        public bool LocalShuttle { get; set; }
               
        public bool Breakfast { get; set; }
               
        public bool Pool { get; set; }
               
        public bool FitnessCenter { get; set; }
               
        public bool Restaurant { get; set; }
               
        public string Information { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string MinCheckInAge { get; set; }

        public string CheckInTime { get; set; }

        public string CheckOutTime { get; set; }

        public string Policies { get; set; }

        public string LocationLink { get; set; }

        public IEnumerable<Room> RoomTypes { get; set; }

    }
}
