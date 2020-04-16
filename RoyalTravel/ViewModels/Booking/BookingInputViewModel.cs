
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.ViewModels.Booking
{
    
    public class BookingInputViewModel 
    {
        [Required, StringLength(30)]
        public string Destination { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        [Required]
        public int Adults { get; set; }

        public int Children { get; set; }

    }
}
