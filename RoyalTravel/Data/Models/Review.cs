
using System.ComponentModel.DataAnnotations;

namespace RoyalTravel.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MinLength(5), MaxLength(200)]
        public string Comment { get; set; }

        [Required, Range(1,10)]
        public int Rating { get; set; }
    }
}
