
using Microsoft.AspNetCore.Mvc;
using RoyalTravel.Data;
using RoyalTravel.Services.Hotel;
using RoyalTravel.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTravel.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHotelService hotelService;

        public BookingController(ApplicationDbContext db, IHotelService hotelService)
        {
            this.db = db;
            this.hotelService = hotelService;
        }

        
        [HttpGet]
        public IActionResult Index()
        {  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchInput, string checkIn, string checkOut, int adults, int kids)
        {
            var searchedHotelsByCity =   await this.hotelService.SearchWithLocationAsync(searchInput);
            var searchedHotelsByName = await this.hotelService.SearchWithHotelNameAsync(searchInput);
            
            var searchResultList = new List<Data.Models.Hotel>();
            searchResultList = searchedHotelsByCity.Count == 0 ? searchedHotelsByName : searchedHotelsByCity;
            //Searching with city is with priority and searching with name is optional
            

            var hotelViewModel = new List<BookingOutputViewModel>();

            foreach (var hotel in searchResultList)
            {
                var currentViewModelHotel = new BookingOutputViewModel();
                currentViewModelHotel.HotelId = hotel.Id;
                currentViewModelHotel.HotelName = hotel.Name;
                currentViewModelHotel.Address = hotel.Address.Street
                 + ", " + hotel.Address.City + ", " + hotel.Address.State + ", " + hotel.Address.PostalCode;
                currentViewModelHotel.TotalRooms = hotel.TotalRooms.ToString();
                currentViewModelHotel.PetFriendly = hotel.Amenity.AllowPets == true ? "Yes" : "No";
                currentViewModelHotel.Wifi = hotel.Amenity.WiFi == true ? "Yes" : "No";
                currentViewModelHotel.Pool = hotel.Amenity.Pool == true ? "Yes" : "No";
                currentViewModelHotel.Rating = hotel.Rating.ToString();
                hotelViewModel.Add(currentViewModelHotel);
            }
            //TODO Implement Paging 

            return this.View(hotelViewModel);

        }

        public async Task<IActionResult> SelectHotel(int? id)
        {
            var hotelQuery = await hotelService.FindHotelById(id);
            var hotel = hotelQuery.FirstOrDefault();

            var hotelViewModel = new SelectedHotelViewModel()
            {
                HotelName = hotel.Name,
                Address = hotel.Address.Street
                 + ", " + hotel.Address.City + ", " + hotel.Address.State + ", " + hotel.Address.PostalCode,
                AirportShuttle = IsAmenityAvailable(hotel.Amenity.AirportShuttle),
                AllowPets = IsAmenityAvailable(hotel.Amenity.AllowPets),
                Breakfast = IsAmenityAvailable(hotel.Amenity.Breakfast),
                FitnessCenter = IsAmenityAvailable(hotel.Amenity.Fitness),
                LocalShuttle = IsAmenityAvailable(hotel.Amenity.LocalShuttle),
                Parking = IsAmenityAvailable(hotel.Amenity.Parking),
                Pool = IsAmenityAvailable(hotel.Amenity.Pool),
                WiFi = IsAmenityAvailable(hotel.Amenity.WiFi),
                Restaurant = IsAmenityAvailable(hotel.Amenity.Restaurant),
                Description = hotel.Info.PropertyDescription,
                Information = hotel.Info.AreaInfo + Environment.NewLine + hotel.Info.AmenitiesInfo,
                CheckInTime = hotel.Info.CheckIn,
                CheckOutTime = hotel.Info.CheckOut,
                MinCheckInAge = hotel.Info.CheckIn,
                Policies = hotel.Info.Policies

            };
            return this.View(hotelViewModel);
        }

        private string IsAmenityAvailable(bool amenity)
        {
            if (amenity == false)
            {
                return "No";
            }

            return "Yes";
        }

        

    }
}