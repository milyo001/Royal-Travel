
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.Data.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(5), MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(16)]
        public string CardNumber { get; set; }

        [Required, MaxLength(5)]
        public string ExpirationDate { get; set; }

        [Required, MinLength(3)]
        public string CVV { get; set; }
    }
}
