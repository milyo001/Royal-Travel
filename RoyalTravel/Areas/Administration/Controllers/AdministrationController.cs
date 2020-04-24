
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoyalTravel.Areas.Administration.ViewModels;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using RoyalTravel.Services.Hotel;
using RoyalTravel.Services.Stays;
using RoyalTravel.Services.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RoyalTravel.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    [Route("/[controller]/[action]")]
    [Authorize(Policy = "RequireAdmin")]
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;
        private readonly IStaysService staysService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHotelService hotelService;

        public AdministrationController(ApplicationDbContext dbContext, IUserService userService, IStaysService staysService, UserManager<ApplicationUser> userManager, IHotelService hotelService)
        {
            this.db = dbContext;
            this.userService = userService;
            this.staysService = staysService;
            this.userManager = userManager;
            this.hotelService = hotelService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AllUsers()
        {
            var users = db.Users.ToList();
            var usersViewModel = new List<AllUsersViewModel>();

            foreach (var user in users)
            {
                var currentUser = user;
                usersViewModel.Add(new AllUsersViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.Email,
                    EmailIsConfirmed = user.EmailConfirmed ? "Yes" : "No",
                    UsedPoints = user.UsedPoints.ToString(),
                }); 
            }
            return View(usersViewModel);
        }

        public IActionResult Earnings()
        {
            var earningStays = staysService.GetEarning(userManager.GetUserId(User));

            if (earningStays == null || earningStays.Count == 0)
            {
                return PartialView("_NoDataPartial");
            }

            var earningsViewModel = new EarningsViewModel
            {
                TotalReservations = earningStays.Count,
                Commission = StaticData.Commission.ToString(),
                TotalEarnings = earningStays.Sum(s => s.TotalPrice).ToString("N"),
                EarningsAfterTaxAndCommission = ((earningStays.Sum(s => s.TotalPrice) * StaticData.Commission) / 100).ToString("N")
            };

            return View(earningsViewModel);
        }

        public IActionResult AllReservations()
        {
            var allReservations = staysService.AllReservations();
            if (allReservations == null || allReservations.Count <= 0)
            {
                return PartialView("_NoDataPartial");
            }
            var reservationsViewModel = new List<ReservationsViewModel>();

            foreach (var stay in allReservations)
            {
                var viewModel = new ReservationsViewModel
                {
                    RoomType = stay.RoomType,
                    MoneySpend = stay.MoneySpend.ToString(),
                    ArrivalDate = stay.ArrivalDate.ToString("MM/dd/yyyy"),
                    DepartureDate = stay.DepartureDate.ToString("MM/dd/yyyy"),
                    PricePerNight = stay.Price.ToString(),
                    TotalPrice = stay.TotalPrice.ToString(),
                    PointsEarned = stay.PointsEarned.ToString(),
                    HotelName = hotelService.FindSingleHotelById(stay.HotelId).Result.Name,
                    UserName = userService.GetUser(stay.ApplicationUserId).UserName,
                    Adults = stay.Adults.ToString(),
                    Children = stay.Children.ToString(),
                    ConfirmationNumber = stay.ConfirmationNumber,
                    BookedOn = stay.BookedOn
                };
                reservationsViewModel.Add(viewModel);
            }
            return View(reservationsViewModel);
        }

    }
}
