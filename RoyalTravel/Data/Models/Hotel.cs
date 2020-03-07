
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

        [ForeignKey(nameof(Amenity)), Required]
        public int AmenityId { get; set; }

        public Amenity Amenity { get; set; }

        [ForeignKey(nameof(Review))]
        public int? ReviewId { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        [ForeignKey(nameof(Info)), Required]
        public int InfoId { get; set; }

        public Info Info { get; set; }

        [ForeignKey(nameof(Room)), Required]
        public int RoomId { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        [ForeignKey(nameof(Address)), Required]
        public int AddressId { get; set; }

        [ForeignKey(nameof(Stay))]
        public int? StayId { get; set; }

        public ICollection<Stay> Stays { get; set; } = new List<Stay>();
        public Address Address { get; set; }
    }
}
