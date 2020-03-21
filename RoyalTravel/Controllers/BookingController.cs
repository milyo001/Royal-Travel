
using Microsoft.AspNetCore.Mvc;
using RoyalTravel.Data;
using RoyalTravel.Services.Hotel;
using RoyalTravel.ViewModels.Booking;
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

            var hotelViewModel = new List<BookingOutputViewModel>();

            foreach (var hotel in searchResultList)
            {
                var currentViewModelHotel = new BookingOutputViewModel();
                currentViewModelHotel.HotelName = hotel.Name;
                currentViewModelHotel.Address = hotel.Address.Street
                 + ", " + hotel.Address.City + ", " + hotel.Address.State + ", " + hotel.Address.PostalCode;
                currentViewModelHotel.TotalRooms = hotel.TotalRooms.ToString();
                currentViewModelHotel.PetFriendly = hotel.Amenity.AllowPets == true ? "Yes" : "No";
                currentViewModelHotel.Wifi = hotel.Amenity.WiFi == true ? "Yes" : "No";
                currentViewModelHotel.Pool = hotel.Amenity.Pool == true ? "Yes" : "No";
                currentViewModelHotel.Rating = hotel.Rating.ToString();
                //currentViewModelHotel.AveragePrice = hotel.Rooms.Average(r => r.Price).ToString();
                hotelViewModel.Add(currentViewModelHotel);
            }
            return this.View(hotelViewModel);

        }
    }
}