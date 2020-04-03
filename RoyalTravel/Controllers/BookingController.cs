
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

        
        
        public IActionResult Index(List<BookingOutputViewModel> hotelViewModels = null)
        {  
            return View(hotelViewModels);
        }

        
        public async Task<IActionResult> SearchHotels(string searchInput, string checkIn, string checkOut, int adults, int kids)
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

            return this.View("Index", hotelViewModel);

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
                AirportShuttle = hotel.Amenity.AirportShuttle,
                AllowPets = hotel.Amenity.AllowPets,
                Breakfast = hotel.Amenity.Breakfast,
                FitnessCenter = hotel.Amenity.Fitness,
                LocalShuttle = hotel.Amenity.LocalShuttle,
                Parking = hotel.Amenity.Parking,
                Pool = hotel.Amenity.Pool,
                WiFi = hotel.Amenity.WiFi,
                Restaurant = hotel.Amenity.Restaurant,
                Description = hotel.Info.PropertyDescription,
                Information = hotel.Info.AreaInfo + Environment.NewLine + hotel.Info.AmenitiesInfo,
                CheckInTime = hotel.Info.CheckIn,
                CheckOutTime = hotel.Info.CheckOut,
                MinCheckInAge = hotel.Info.CheckIn,
                Policies = hotel.Info.Policies,
                RoomTypes = hotel.Rooms
                    .GroupBy(r => new {r.RoomType, r.Price, r.Smoking})
                    .Select(g => g.First())
            };
            return this.View(hotelViewModel);
        }

    }
}