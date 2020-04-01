using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalTravel.Data.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string RoomType { get; set; }

        public bool Smoking { get; set; }

        public bool Luxury { get; set; }

        public bool WithBreakfast { get; set; }

        public bool AC { get; set; }

        [Required, MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<Stay> Stays { get; set; }
    }
}
