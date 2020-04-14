
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.ViewModels.Booking
{
    
    public class BookingInputViewModel 
    {
        [Required(ErrorMessage = "Please enter destination!"), StringLength(30)]
        public string Destination { get; set; }

        [Required(ErrorMessage = "The arrival date is required.")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "The departure date is required.")]
        public DateTime CheckOut { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public int Adults { get; set; }

        public int Children { get; set; }

    }
}
