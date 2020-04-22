

using Microsoft.EntityFrameworkCore;
using RoyalTravel.Data.Models;

namespace RoyalTravel.Data
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 2,
                    WiFi = true,
                    AllowPets = true,
                    Parking = true,
                    AirportShuttle = true,
                    LocalShuttle = false,
                    Breakfast = true,
                    Pool = false,
                    Fitness = true,
                    Restaurant = true
                },

                new Amenity
                {
                    Id = 3,
                    WiFi = true,
                    AllowPets = true,
                    Parking = true,
                    AirportShuttle = false,
                    LocalShuttle = true,
                    Breakfast = true,
                    Pool = false,
                    Fitness = true,
                    Restaurant = true
                },

                new Amenity
                {
                    Id = 4,
                    WiFi = true,
                    AllowPets = true,
                    Parking = true,
                    AirportShuttle = true,
                    LocalShuttle = true,
                    Breakfast = true,
                    Pool = true,
                    Fitness = true,
                    Restaurant = true
                }
                );
            builder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    City = "Sofia",
                    State = "Sofia",
                    Street = "Karamanoleva Strasse 55",
                    PostalCode = "1500"
                },
                new Address
                {
                    Id = 2,
                    City = "Bansko",
                    State = "Blagoevgrad",
                    Street = "Pirin Street 22A",
                    PostalCode = "2477"
                },
                new Address
                {
                    Id = 3,
                    City = "Bansko",
                    State = "Blagoevgrad",
                    Street = "kapitan Kiril Stoyanov 44A",
                    PostalCode = "1800"
                });
            builder.Entity<Info>().HasData(
                new Info
                {
                    Id = 1,
                    CheckIn = "14:00",
                    CheckOut = "12:00",
                    MinAge = 18,
                    AreaInfo = "When traveling to one of Bulgaria most beautiful mountain cities, stay at our The Monastery by Royal Bansko hotel . We are located off interstate E-79  with easy access to Bansko Cabin lift and within easy reach of the historic district, Place Royale, and the old-world charms of our town: Bansko Church.",
                    PaymentOptions = "Cash, Mastercard",
                    Policies = "Cancel 24 hours prior to 14:00h day of arrival.",
                    PropertyDescription = "The Monastery is one of the top boutique holiday complexes in Bansko located in quiet area only a few minutes’ walk from the main ski road and 5 to 7 minutes’ walk to the Gondola lift station. We have over 50 fully equipped and serviced apartments of various type all fitted with comfortable beds and kitchens. Larger apartments have sofa-beds in the living room as an additional sleeping area for 2 children or one adult. The Monastery boasts a 4 star facilities with large SPA, including 12 meter heated swimming pool, large Jacuzzi, Steam room, Fitness and Sauna. Massages are also available to book at an additional charge. Our restaurant is opened for breakfast, lunch and dinner, serving freshly prepared local and traditional Bulgarian dishes. The complex is fully serviced with friendly English speaking staff that will go out of their way to help you with anything you need and make your stay pleasant.",
                    AmenitiesInfo = "List with additional amenities...here."
                },
                new Info
                {
                    Id = 2,
                    CheckIn = "14:00",
                    CheckOut = "12:00",
                    MinAge = 18,
                    AreaInfo = "Mountain View is a luxurious hotel complex of 5-star functionality that offers the full range of services for full-value relax or business travel. The hotel is situated in the central part of Bansko, at only 15-minutes’ walk on foot from the ski lift.",

                    PaymentOptions = "Cash, Mastercard",

                    Policies = "Cancel 24 hours prior to 14:00h day of arrival.",

                    PropertyDescription = @"Mountain View by Royal works year-round – during the winter the complex is skiers’ favourite destination and offers ski wardrobe as well as free transport that regularly carries the skiers from the hotel to the lift and back, and during the summer the guests have the opportunity to take advantage of the outdoor swimming-pools with pool bar to the hotel, as well as versatile activities in and around Bansko, such as picnic up in the mountain, excursion to the Park of dancing bears, walks along eco pathways, cycling, off-road and many more.",

                    AmenitiesInfo = "List with additional amenities...here."
                },
                new Info
                {
                    Id = 3,
                    CheckIn = "14:00",
                    CheckOut = "12:00",
                    MinAge = 18,
                    AreaInfo = "Located in the Sofia's wealthy Boyana neighborhood at the foot of the Vitosha Mountain, the Hotel Villa Boyana offers free private parking and free Wi-Fi.All rooms at the Villa Boyana feature an elegant bathroom with toiletries,a mini - bar and a TV.",
                    PaymentOptions = "Cash, Mastercard",
                    Policies = "Cancel 24 hours prior to 14:00h day of arrival.",
                    PropertyDescription = @"An Italian and a Bulgarian restaurant can be found next to the hotel. The famous Boyana Church is 400 m away and the National History Museum is 0.7 mi away.
                      The center of Sofia can be reached by car in 15 minutes and the Simeonovo Ski Elevator in 10 minutes.",

                    AmenitiesInfo = "List with additional amenities...here."
                },
                new Info
                {
                    Id = 4,
                    CheckIn = "14:00",
                    CheckOut = "12:00",
                    MinAge = 18,
                    AreaInfo = "Located in the Sofia's wealthy Boyana neighborhood at the foot of the Vitosha Mountain, the Hotel Villa Boyana offers free private parking and free Wi-Fi.All rooms at the Villa Boyana feature an elegant bathroom with toiletries,a mini - bar and a TV.",
                    PaymentOptions = "Cash, Mastercard",
                    Policies = "Cancel 24 hours prior to 14:00h day of arrival.",
                    PropertyDescription = @"An Italian and a Bulgarian restaurant can be found next to the hotel. The famous Boyana Church is 400 m away and the National History Museum is 0.7 mi away.
                      The center of Sofia can be reached by car in 15 minutes and the Simeonovo Ski Elevator in 10 minutes.",

                    AmenitiesInfo = "List with additional amenities...here."
                }
                );

            builder.Entity<Room>().HasData(
            new Room
            {
                Id = 1,
                RoomType = "One Queen Bed",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 2,
                Price = 43.55m,
                HotelId = 1
            },
            new Room
            {
                Id = 2,
                RoomType = "One Queen Bed",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 2,
                Price = 43.55m,
                HotelId = 1
            },
            new Room
            {
                Id = 3,
                RoomType = "One Queen Bed",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 2,
                Price = 43.55m,
                HotelId = 1
            },
            new Room
            {
                Id = 4,
                RoomType = "One Queen Bed",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 2,
                Price = 43.55m,
                HotelId = 2
            },
            new Room
            {
                Id = 5,
                RoomType = "One Queen Bed",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 2,
                Price = 43.55m,
                HotelId = 2
            },
            new Room
            {
                Id = 6,
                RoomType = "Two Queen Beds",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 47.55m,
                HotelId = 1
            },
            new Room
            {
                Id = 7,
                RoomType = "Two Queen Beds",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 47.55m,
                HotelId = 1
            },
            new Room
            {
                Id = 8,
                RoomType = "Two Queen Beds",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 47.55m,
                HotelId = 1
            },
            new Room
            {
                Id = 9,
                RoomType = "Two Queen Beds",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 47.55m,
                HotelId = 2
            },
            new Room
            {
                Id = 10,
                RoomType = "Two Queen Beds",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 47.55m,
                HotelId = 2
            },
            new Room
            {

                Id = 11,
                RoomType = "One Queen Bed",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 48.55m,
                HotelId = 1
            },
            new Room
            {

                Id = 12,
                RoomType = "One Queen Bed",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 48.55m,
                HotelId = 1
            },
            new Room
            {
                Id = 13,
                RoomType = "One Queen Bed",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 48.55m,
                HotelId = 1
            },
            new Room
            {

                Id = 14,
                RoomType = "One Queen Bed",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 48.55m,
                HotelId = 2
            },
            new Room
            {
                Id = 15,
                RoomType = "One Queen Bed",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 48.55m,
                HotelId = 2
            },
            new Room
            {
                Id = 16,
                RoomType = "Two Queen Beds",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 55.39m,
                HotelId = 1
            },
            new Room
            {
                Id = 17,
                RoomType = "Two Queen Beds",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 55.39m,
                HotelId = 1
            },
            new Room
            {
                Id = 18,
                RoomType = "Two Queen Beds",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 55.39m,
                HotelId = 1
            },
            new Room
            {
                Id = 19,
                RoomType = "Two Queen Beds",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 55.39m,
                HotelId = 2
            },
            new Room
            {
                Id = 20,
                RoomType = "Two Queen Beds",
                Smoking = true,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, mini-refrigerator, microwave, air conditioning, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarm clock, desk, iron, ironing board",
                MaxOccupancy = 4,
                Price = 55.39m,
                HotelId = 2
            },
            new Room
            {
                Id = 21,
                RoomType = "Two Bedroom Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 120.61m,
                HotelId = 1
            },
            new Room
            {
                Id = 22,
                RoomType = "Two Bedroom Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 120.61m,
                HotelId = 1
            },
            new Room
            {
                Id = 23,
                RoomType = "Two Bedroom Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 120.61m,
                HotelId = 1
            },
            new Room
            {
                Id = 24,
                RoomType = "Two Bedroom Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 120.61m,
                HotelId = 2
            },
            new Room
            {
                Id = 25,
                RoomType = "Two Bedroom Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 120.61m,
                HotelId = 2
            },
            new Room
            {
                Id = 26,
                RoomType = "Mountain View Junior Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 5,
                Price = 173.23m,
                HotelId = 3
            },
            new Room
            {
                Id = 27,
                RoomType = "Mountain View Junior Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 5,
                Price = 173.23m,
                HotelId = 3
            },
            new Room
            {
                Id = 28,
                RoomType = "Mountain View Junior Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 5,
                Price = 173.23m,
                HotelId = 3
            },
            new Room
            {
                Id = 29,
                RoomType = "Mountain View Junior Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 5,
                Price = 173.23m,
                HotelId = 3
            },
            new Room
            {
                Id = 30,
                RoomType = "Mountain View Junior Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 5,
                Price = 173.23m,
                HotelId = 3
            },
            new Room
            {
                Id = 31,
                RoomType = "Mountain View Grand Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            },
            new Room
            {
                Id = 32,
                RoomType = "Mountain View Grand Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            },
            new Room
            {
                Id = 33,
                RoomType = "Mountain View Grand Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            },
            new Room
            {
                Id = 34,
                RoomType = "Mountain View Grand Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            },
            new Room
            {
                Id = 35,
                RoomType = "Mountain View Grand Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free WiFi, free continental breakfast, sofa bed,refrigerator, microwave, airconditioning, hot tub/    jacuzzi coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            },
            new Room
            {
                Id = 36,
                RoomType = "Presidential Tower Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free high-speed WiFi, free hot breakfast,refrigerator, microwave, air conditioning, hot tub/    jacuzzi, VIP garage parking, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board, huge warderobe, ",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            },
            new Room
            {
                Id = 37,
                RoomType = "Presidential Tower Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free high-speed WiFi, free hot breakfast,refrigerator, microwave, air conditioning, hot tub/    jacuzzi, VIP garage parking, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board, huge warderobe, ",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            },
            new Room
            {
                Id = 38,
                RoomType = "Presidential Tower Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free high-speed WiFi, free hot breakfast,refrigerator, microwave, air conditioning, hot tub/    jacuzzi, VIP garage parking, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board, huge warderobe, ",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            },
            new Room
            {
                Id = 39,
                RoomType = "Presidential Tower Suite",
                Smoking = false,
                WithBreakfast = true,
                Description = "Free high-speed WiFi, free hot breakfast,refrigerator, microwave, air conditioning, hot tub/    jacuzzi, VIP garage parking, coffee/tea maker, hair dryer, HDTV, bathroom amenities, alarmclock, desk, iron, ironing board, huge warderobe, ",
                MaxOccupancy = 6,
                Price = 273.73m,
                HotelId = 3
            });
            // Seed 25 rooms for testing, 5 of each room type, Smoking rooms are considered as a diffrent room type by default
            builder.Entity<Hotel>().HasData(
                new
                {
                    Id = 1,
                    Name = "The Monastery",
                    Rating = 9,
                    Stars = 5,
                    AddressId = 2,
                    TotalRooms = 15,
                    AmenityId = 2,
                    InfoId = 1,
                    LocationLink = @"https://www.openstreetmap.org/export/embed.html?bbox=23.468127250671387%2C41.82734700208549%2C23.481924533843994%2C41.83267922868568&amp;layer=mapnik&amp;marker=41.83001317090565%2C23.47502589225769"
                },
                new
                {
                    Id = 2,
                    Name = "Royal Palace",
                    Rating = 8,
                    Stars = 3,
                    AddressId = 1,
                    TotalRooms = 10,
                    AmenityId = 3,
                    InfoId = 3,
                    LocationLink = @"https://www.openstreetmap.org/export/embed.html?bbox=23.30752730369568%2C42.703512202193544%2C23.31756949424744%2C42.70877082903225&amp;layer=mapnik&amp;marker=42.70614157129563%2C23.312548398971558"
                },
                new
                {
                    Id = 3,
                    Name = "Mountain Garden by Royal",
                    Rating = 10,
                    Stars = 5,
                    AddressId = 3,
                    TotalRooms = 20,
                    AmenityId = 4,
                    InfoId = 2,
                    LocationLink = @"https://www.openstreetmap.org/export/embed.html?bbox=23.47535848617554%2C41.82475655017072%2C23.485400676727295%2C41.830088992534364&amp;layer=mapnik&amp;marker=41.82742282687206%2C23.480379581451416"
                });

        }
    }
}
