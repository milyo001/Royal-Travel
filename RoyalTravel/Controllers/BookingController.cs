
namespace RoyalTravel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RoyalTravel.Data;
    using RoyalTravel.Data.Models;
    using RoyalTravel.Services.Hotel;
    using RoyalTravel.Services.Room;
    using RoyalTravel.ViewModels;
    using RoyalTravel.ViewModels.Booking;

    public class BookingController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly IRoomService roomService;
        private readonly UserManager<ApplicationUser> userManager;
        public BookingController(IHotelService hotelService, IRoomService roomService, UserManager<ApplicationUser> userManager)
        {
            this.hotelService = hotelService;
            this.roomService = roomService;
            this.userManager = userManager;
        }

        public IActionResult Index(BookingViewModel viewModel)
        {
            return View(viewModel);
        }

        public async Task<IActionResult> SearchHotels([Bind]BookingInputViewModel inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.Destination) || inputModel.Destination.Length > 30|| inputModel.CheckOut <= inputModel.CheckIn || inputModel.CheckIn == new DateTime(0001, 01, 01) || inputModel.CheckOut == new DateTime(0001, 01, 01) || inputModel.Adults <= 0 || inputModel.Children < 0 || inputModel.Destination.Length > 30 || inputModel.CheckIn < DateTime.Today || inputModel.CheckOut < DateTime.Today)
            {
                throw new ArgumentOutOfRangeException("User Input data is incorrect! Make sure that departure date is greater than arrival date! Make sure that number of adults is not empty!");
            }

            //Check if datetime is with default value, additional checks for other parameters

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
                currentViewModelHotel.Stars = hotel.Stars;
                currentViewModelHotel.Image = hotel.Image;
                
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
            if (id == null || id == 0)
            {
                return NotFound("Hotel not found!");
            }

            var hotelQuery = await this.hotelService.FindHotelById(id);
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
                    .Select(g => g.First()),
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
            }

            // Validate the dates parsing if the user modified the URL
            var checkInDate = DateTime.ParseExact(checkIn, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var checkOutDate = DateTime.ParseExact(checkOut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            int nightsStay = (checkOutDate - checkInDate).Days;

            if (checkOutDate <= checkInDate || id == null || adults == 0 || checkInDate == checkOutDate || checkInDate == null || checkOutDate == null)
            {
                throw new ArgumentOutOfRangeException("Invalid input data! Check out date should be after the check in date! Minimum number of adults is one! Hotel indentifier is required!");
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

                // Sorting and grouping the rooms to filter all rooms by room type, price and if they are smoking or non smoking
                RoomsAvailability = selectedHotel.Rooms
                    .Where(room => room.Stays.All(res => res.DepartureDate <= checkInDate || res.ArrivalDate >= checkOutDate)),

                // Gets all the rooms which are available
            };


            return this.View(bookHotelViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Confirm(int? id, string checkIn, string checkOut, int adults, int children, string roomType)
        {
            var checkInDate = DateTime.ParseExact(checkIn, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var checkOutDate = DateTime.ParseExact(checkOut, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var selectedHotel = this.hotelService.FindSingleHotelById(id).Result;

            // Will get first available room by given type
            var selectedAvailableRoom = selectedHotel.Rooms
                .Where(room => room.Stays.All(res => res.DepartureDate <= checkInDate || res.ArrivalDate >= checkOutDate))
                .FirstOrDefault(r => r.RoomType == roomType);

            // Additional validation for all required parameters
            if (checkOutDate <= checkInDate || adults < 1 || id == null ||
                selectedAvailableRoom.MaxOccupancy < adults + children)
            {
                throw new ArgumentOutOfRangeException("Error: Invalid input data!");
            }

            var reservation = new Stay
            {
                RoomType = roomType,
                ArrivalDate = checkInDate,
                DepartureDate = checkOutDate,
                Price = selectedAvailableRoom.Price,
                TotalPrice = selectedAvailableRoom.Price * (checkOutDate - checkInDate).Days,
                HotelId = (int)id,
                RoomId = selectedAvailableRoom.Id,
                MoneySpend = selectedAvailableRoom.Price * (checkOutDate - checkInDate).Days,
                ApplicationUserId = this.userManager.GetUserId(User),
                ConfirmationNumber = this.roomService.GenerateConfirmationNumber(selectedHotel.Name),
                Adults = adults,
                Children = children,
                BookedOn = DateTime.Now,
                IsCanceled = false,
                PointsSpend = 0,
                PointsEarned = (int)selectedAvailableRoom.Price * (checkOutDate - checkInDate).Days * StaticData.PointsMultiplier,
            };
            //var user = await userManager.GetUserAsync(User);
            //user.Points += reservation.PointsEarned;
            //Add the points to the user account, so he/she can use it later on

            roomService.AddReservation(reservation);
            var confirmResViewModel = new ConfrimResViewModel
            {
                StayId = reservation.Id,
                HotelName = reservation.Hotel.Name,
                ConfirmationNumber = reservation.ConfirmationNumber,
                CheckInTime = reservation.Hotel.Info.CheckIn,
                CheckOutTime = reservation.Hotel.Info.CheckOut,
                Nights = (checkOutDate - checkInDate).Days.ToString(),
                Adults = reservation.Adults.ToString(),
                Children = reservation.Children.ToString(),
                CheckIn = reservation.ArrivalDate.ToString("MM/dd/yyyy"),
                CheckOut = reservation.DepartureDate.ToString("MM/dd/yyyy"),
                BookedOn = reservation.BookedOn,
                IsCanceled = false,
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