using Microsoft.AspNetCore.Identity;
using RoyalTravel.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalTravel.Data.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required, MaxLength(30), MinLength(3)]
        public string FirstName { get; set; }

        [Required, MaxLength(30), MinLength(3)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string Country { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        public PhoneType PhoneType { get; set; }

        public int UsedPoints { get; set; }

        [ForeignKey(nameof(Stay))]
        public int? StayId { get; set; }

        public ICollection<Stay> Stays { get; set; } = new List<Stay>();

        [ForeignKey(nameof(Payment))]
        public int? PaymentId { get; set; }

        public Payment Payment { get; set; }

        [ForeignKey(nameof(Address))]
        public int? AddressId { get; set; }

        public Address Address { get; set; }

    }
}
