
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.ViewModels.Booking
{
    
    public class BookingInputModel
    {
        [Required, StringLength(30)]
        public string Destination { get; set; }

        [Required]
        public string CheckIn { get; set; }

        [Required]
        public string CheckOut { get; set; }

        [Required]
        public int Adults { get; set; }

        public int Kids { get; set; }

    }
}
