
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.Data.Models
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }

        public bool WiFi { get; set; }

        public bool AllowPets { get; set; }

        public bool Parking { get; set; }

        public bool AirportShuttle { get; set; }

        public bool LocalShuttle { get; set; }

        public bool Breakfast { get; set; }

        public bool Pool { get; set; }

        public bool Fitness { get; set; }

        public bool Restaurant { get; set; }
    }
}
