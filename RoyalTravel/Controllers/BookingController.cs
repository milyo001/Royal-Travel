
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using RoyalTravel.Services.Hotel;
using RoyalTravel.Services.Room;
using RoyalTravel.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTravel.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHotelService hotelService;
        private readonly IRoomService roomService;
        private readonly UserManager<ApplicationUser> userManager;
        public BookingController(ApplicationDbContext db, IHotelService hotelService, IRoomService roomService, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.hotelService = hotelService;
            this.roomService = roomService;
            this.userManager = userManager;
        }



        public IActionResult Index(List<BookingOutputViewModel> hotelViewModels = null)
        {
            return View(hotelViewModels);
        }


        public async Task<IActionResult> SearchHotels(string searchInput, string checkIn, string checkOut, int adults, int children)
        {
            var searchedHotelsByCity = await this.hotelService.SearchWithLocationAsync(searchInput);
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
                    .GroupBy(r => new { r.RoomType, r.Price, r.Smoking })
                    .Select(g => g.First())
            };
            return this.View(hotelViewModel);
        }


        public async Task<IActionResult> BookHotel(int? id, string checkIn, string checkOut, int adults, int children)
        {
            //TODO Validation
            var checkInDate = DateTime.ParseExact(checkIn, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var checkOutDate = DateTime.ParseExact(checkOut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            int nightsStay = (checkOutDate - checkInDate).Days;

            var hotelQuery = await hotelService.FindHotelById(id);
            var selectedHotel = hotelQuery.FirstOrDefault();

            var bookHotelViewModel = new MakeBookingViewModel()
            {
                HotelId = selectedHotel.Id,
                HotelName = selectedHotel.Name,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Adults = adults,
                Children = children,
                NumberOfNights = nightsStay,
                RoomsGroup = selectedHotel.Rooms
                    .GroupBy(r => new { r.RoomType, r.Price, r.Smoking })
                    .Select(g => g.First()),
                //Sorting and grouping the rooms to filter all rooms by room type, price and if they are smoking or non smoking
                RoomsAvailability = selectedHotel.Rooms
                    .Where(room => room.Stays.All(res => res.DepartureDate <= checkInDate || res.ArrivalDate >= checkOutDate))
                //Gets all the rooms which are available
            };


            return this.View(bookHotelViewModel);

        }

        [Authorize]
        public async Task<IActionResult> Confirm(int? id, string checkIn, string checkOut, int adults, int children, string roomType)
        {
            var checkInDate = DateTime.ParseExact(checkIn, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var checkOutDate = DateTime.ParseExact(checkOut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var selectedHotel = hotelService.FindSingleHotelById(id).Result;
            var selectedAvailableRoom = selectedHotel.Rooms
                .Where(room => room.Stays.All(res => res.DepartureDate <= checkInDate || res.ArrivalDate >= checkOutDate))
                .FirstOrDefault(r => r.RoomType == roomType);

            //Will get first available room by type since all rooms types added by admin are the same

            if (checkOutDate <= checkInDate || adults < 1 || id == null)
            {
                throw new ArgumentOutOfRangeException("Error: Invalid input data(check in, check out or number of adults).");

                //Additional validation for all required parameters if someone is trying to modify the parameters in the URL
            }

            var reservation = new Stay
            {
                RoomType = roomType,
                ArrivalDate = checkInDate,
                DepartureDate = checkOutDate,
                Price = selectedAvailableRoom.Price,
                HotelId = (int)id,
                RoomId = selectedAvailableRoom.Id,
                MoneySpend = selectedAvailableRoom.Price * (checkOutDate - checkInDate).Days,
                ApplicationUserId = userManager.GetUserId(User),
            };

            roomService.AddReservation(reservation);

            return this.View();
        }

    }
}