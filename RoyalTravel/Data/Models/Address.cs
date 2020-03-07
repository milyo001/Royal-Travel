
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.Data.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [MinLength(1), MaxLength(30), Required]
        public string City { get; set; }

        [MinLength(1), MaxLength(30), Required]
        public string State { get; set; }

        [MinLength(1), MaxLength(30), Required]
        public string Street { get; set; }

        [MinLength(1), MaxLength(30), Required]
        public string PostalCode { get; set; }
    }
}
