using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
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
                .Include(s => s.Hotel)
                .OrderBy(s => s.ArrivalDate)
                .ToList();
            int totalPoints = (int)db.Stays.Sum(s => s.Price) * StaticData.PointsMultiplier;
            //get total points of all stays to detirmine the loyalty level (silver, gold, diamond, etc)
            var userTier = string.Empty;

            if (totalPoints >= StaticData.RewardsTier1Requirement)
            {
                userTier = StaticData.Tiers.Silver.ToString();
            }
            else if (totalPoints >= StaticData.RewardsTier2Requirement)
            {
                userTier = StaticData.Tiers.Gold.ToString();
            }
            else if (totalPoints >= StaticData.RewardsTier3Requirement)
            {
                userTier = StaticData.Tiers.Platinum.ToString();
            }

            rewardsViewModel.UserDataViewModel.Points = currentUser.Points.ToString();

            foreach (var stay in stays)
            {
                var stayViewModel = new StayViewModel()
                {
                    Hotel = stay.Hotel.Name,
                    RoomType = stay.RoomType,
                    ArrivalDate = stay.ArrivalDate.ToString("MM/dd/yyyy"),
                    DepartureDate = stay.DepartureDate.ToString("MM/dd/yyyy"),
                    PointsSpend = stay.PointsSpend.ToString(),
                    PointsEarned = stay.PointsEarned.ToString(),
                    Price = stay.Price.ToString(),
                    TotalPrice = (stay.Price * (stay.DepartureDate - stay.ArrivalDate).Days).ToString()
                };
                rewardsViewModel.StayViewModels.Add(stayViewModel);
            }

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
