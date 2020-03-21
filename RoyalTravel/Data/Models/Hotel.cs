﻿
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalTravel.Data.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }

        [Range(1, 10)]
        public int? Rating { get; set; }

        [Required, Range(1,5)]
        public int Stars { get; set; }

        [Required]
        public int TotalRooms { get; set; }

        public Amenity Amenity { get; set; } = new Amenity();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public Info Info { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public Address Address { get; set; } = new Address();

        public ICollection<Stay> Stays { get; set; } = new List<Stay>();
        
    }
}
