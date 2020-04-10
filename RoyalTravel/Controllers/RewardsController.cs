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
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public RewardsController(ILogger<HomeController> logger, ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.db = db;
            this.userManager = userManager;
            this.userService = userService;
           
        }

        public IActionResult Index()
        {
            var currentUser = userManager.GetUserAsync(User).Result;
            var rewardsViewModel = new RewardsInputModel();
            var stays = db.Stays
                .Where(s => s.ApplicationUserId == currentUser.Id)
                .OrderBy(s => s.ArrivalDate)
                .ToList();
            int totalPoints = (int)db.Stays.Sum(s => s.Price) * StaticData.PointsMultiplier;
            //get total points of all stays to detirmine the loyalty level (silver, gold, diamond, etc)

            if (totalPoints <= StaticData.RewardsTier1Requirement)
            {
                StaticData.PointsMultiplier = 1.00;
            }
            else if (totalPoints <= StaticData.RewardsTier2Requirement)
            {

            }
            else if (totalPoints <= StaticData.RewardsTier3Requirement)
            {

            }
            //rewardsViewModel.UserDataViewModel.Points = currentUser.Points.ToString();
            //var totalPointsForAllStays = stayViewModel.StayViewModel.Sum(s => s.Price) * ConstData.PointsMultiplier;


            //for testing only before I seed the database

            return View(rewardsViewModel);
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
