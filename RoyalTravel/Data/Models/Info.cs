
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.Data.Models
{
    public class Info
    {
        [Key]
        public int Id { get; set; }

        [MinLength(1), MaxLength(10)]
        public string CheckIn { get; set; }

        [MinLength(1), MaxLength(10)]
        public string CheckOut { get; set; }

        [Range(1,30)]
        public int MinAge { get; set; }

        [MinLength(1), MaxLength(500)]
        public string AreaInfo { get; set; }

        [MinLength(1), MaxLength(500)]
        public string PaymentOptions { get; set; }

        [MinLength(1), MaxLength(500)]
        public string Policies { get; set; }

        [MinLength(1), MaxLength(1000)]
        public string PropertyDescription { get; set; }

        [MinLength(1), MaxLength(500)]
        public string AmenitiesInfo { get; set; }


    }
}
