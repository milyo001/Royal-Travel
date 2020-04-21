using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoyalTravel.Data.Models;

namespace RoyalTravel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Info> Infos { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Stay> Stays { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            builder.Entity<Amenity>().HasData(
                new Amenity { Id = 2, WiFi = true, AllowPets = true, Parking = true, AirportShuttle = true, 
                    LocalShuttle = false, Breakfast = true, Pool = false, Fitness = true, Restaurant = true },

                new Amenity { Id = 3, WiFi = true, AllowPets = true, Parking = true, AirportShuttle = false,
                    LocalShuttle = true, Breakfast = true, Pool = false, Fitness = true, Restaurant = true }
                ) ;
        }


    }
}



