
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalTravel.Data.Models
{
    public class Stay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Range(0.01d, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")] 
        
        public decimal? MoneySpend { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }
        public double Rate { get; set; }

        public int? PointsEarned { get; set; }

        public int? PointsSpend { get; set; }

        [ForeignKey(nameof(Hotel)), Required]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}
