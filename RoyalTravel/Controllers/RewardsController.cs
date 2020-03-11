using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using RoyalTravel.Models;
using RoyalTravel.Services.User;
using RoyalTravel.ViewModels;
using RoyalTravel.ViewModels.Rewards;

namespace RoyalTravel.Controllers
{
    public class RewardsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public RewardsController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext, 
            UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.context = applicationDbContext;
            _logger = logger;
            this.userManager = userManager;
            this.userService = userService;
           
        }

        public IActionResult Index()
        {
            var stayViewModel = new RewardsInputModel();
           
            var stayViewModelOne = new StayViewModel
            {
                Hotel = "Royal Marina",
                ArrivalDate = new DateTime(2020, 5, 22).ToString("dd/MM/yyyy"),
                DepartureDate = new DateTime(2020, 5, 25).ToString("dd/MM/yyyy"),
                RoomType = "2 Queen Beds",
                Rate = 234.55,
                Price = 679.33m,
                PointsSpend = 0
            };
            var stayViewModelTwo = new StayViewModel
            {
                Hotel = "Royal Jungle",
                ArrivalDate = new DateTime(2020, 5, 28).ToString("dd/MM/yyyy"),
                DepartureDate = new DateTime(2020, 5, 29).ToString("dd/MM/yyyy"),
                RoomType = "1 King Bed",
                Rate = 144.55,
                Price = 425.33m,
                PointsSpend = 0,
            };

            

            stayViewModel.StayViewModel.Add(stayViewModelOne);
            stayViewModel.StayViewModel.Add(stayViewModelTwo);

            stayViewModel.UserDataViewModel.Points = 
                userService.GetUser(this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Points.ToString();
            //for testing only

            return View(stayViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
