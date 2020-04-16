
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using RoyalTravel.Services.Hotel;
using RoyalTravel.Services.Room;
using RoyalTravel.ViewModels;
using RoyalTravel.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        
        public IActionResult Index(BookingViewModel viewModel)
        {
            return View(viewModel);
        }

        public async Task<IActionResult> SearchHotels([Bind]BookingInputViewModel inputModel)

        /*(string searchInput, string checkIn, string checkOut, int adults, int children)*/
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentOutOfRangeException("User Input data is incorrect! Make sure that departure date is greater than arrival date! Make sure that number of adults is not empty!");
            }
            var searchedHotelsByCity = await this.hotelService.SearchWithLocationAsync(inputModel.Destination);
            var searchedHotelsByName = await this.hotelService.SearchWithHotelNameAsync(inputModel.Destination);

            var searchResultList = new List<Hotel>();
            searchResultList = searchedHotelsByCity.Count == 0 ? searchedHotelsByName : searchedHotelsByCity;
            //Searching with city is with priority and searching with name is optional

            var bookingViewModel = new BookingViewModel();
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
            bookingViewModel.BookingOutputViewModels = hotelViewModel;
            //Contains information about the searched hotels;
            bookingViewModel.InputModel = inputModel;
            //Contains information about the input, including validation

            //TODO Implement Paging 

            return this.View("Index", bookingViewModel);

        }

        public async Task<IActionResult> SelectHotel(int? id)
        {
            if (id == null)
            {
                throw new NullReferenceException("Hotel Id cannot be empty!");
            }
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
                LocationLink = hotel.LocationLink,
                Policies = hotel.Info.Policies,
                RoomTypes = hotel.Rooms
                    .GroupBy(r => new { r.RoomType, r.Price, r.Smoking })
                    .Select(g => g.First())
            };
            return this.View(hotelViewModel);
        }


        public async Task<IActionResult> BookHotel(int? id, string checkIn, string checkOut, int adults, int children)
        {
            try
            {
                var checkInDateTemp = DateTime.ParseExact(checkIn, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var checkOutDateTemp = DateTime.ParseExact(checkOut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

                throw new ArgumentOutOfRangeException("Invalid dates!");
            };
            //Validate the dates parsing if the user modified the URL

            var checkInDate = DateTime.ParseExact(checkIn, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var checkOutDate = DateTime.ParseExact(checkOut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            int nightsStay = (checkOutDate - checkInDate).Days;

            if (checkOutDate <= checkInDate || id == null || adults == 0 || checkInDate == checkOutDate)
            {
                throw new ArgumentOutOfRangeException("Invalid input data! Check out date should be after the check in! Minimum number of adults is one! Hotel indentifier is required!");
                //Additional validation for all required parameters
            }

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

            if (checkOutDate <= checkInDate || adults < 1 || id == null ||
                selectedAvailableRoom.MaxOccupancy < adults + children)
            {
                throw new ArgumentOutOfRangeException("Error: Invalid input data!");
                //Additional validation for all required parameters
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
                ConfirmationNumber = roomService.GenerateConfirmationNumber(selectedHotel.Name),
                Adults = adults,
                Children = children
            };
            roomService.AddReservation(reservation);

            var confirmResViewModel = new ConfrimResViewModel
            {
                HotelName = reservation.Hotel.Name,
                ConfirmationNumber = reservation.ConfirmationNumber,
                CheckInTime = reservation.Hotel.Info.CheckIn,
                CheckOutTime = reservation.Hotel.Info.CheckOut,
                Nights = (checkOutDate - checkInDate).Days.ToString(),
                Adults = reservation.Adults.ToString(),
                Children = reservation.Children.ToString(),
                CheckIn = reservation.ArrivalDate.ToString("MM/dd/yyyy"),
                CheckOut = reservation.DepartureDate.ToString("MM/dd/yyyy")
            };

            return this.View(confirmResViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}