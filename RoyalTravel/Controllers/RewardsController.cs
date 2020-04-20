using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using RoyalTravel.Services.Stays;
using RoyalTravel.Services.User;
using RoyalTravel.ViewModels;
using RoyalTravel.ViewModels.Rewards;

namespace RoyalTravel.Controllers
{
    [Authorize]
    public class RewardsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IStaysService staysService;

        public RewardsController(ILogger<HomeController> logger, ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager, IUserService userService, IStaysService staysService)
        {
            this.db = db;
            this.userManager = userManager;
            this.userService = userService;
            this.staysService = staysService;
        }

        public IActionResult Index()
        {
            var currentUser = userManager.GetUserAsync(User).Result;
            var rewardsViewModel = new RewardsInputModel();
            var stays = db.Stays
                .Where(s => s.ApplicationUserId == currentUser.Id)
                .Include(s => s.Hotel)
                .OrderByDescending(s => s.BookedOn)
                .ToList();

            int totalPoints = (int)db.Stays
                .Where(s => s.ApplicationUserId == currentUser.Id 
                && s.DepartureDate < DateTime.Today && s.IsCanceled == false)
                .Sum(s => s.TotalPrice * StaticData.PointsMultiplier);
            //Get total points of all stays, which are not canceled. User will get points only if departure date is 
            //less or equal to today's date

            var userTier = string.Empty;

            if (totalPoints <= StaticData.RewardsTier1Requirement)
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
            //Get the user tier acording to total points earned

            totalPoints -= currentUser.UsedPoints;
            //Gets the remaining points for the user

            rewardsViewModel.UserDataViewModel.TotalPoints = totalPoints;
            rewardsViewModel.UserDataViewModel.RewardsTier = userTier;

            foreach (var stay in stays)
            {
                var stayViewModel = new StayViewModel()
                {
                    ConfirmationNumber = stay.ConfirmationNumber,
                    Hotel = stay.Hotel.Name,
                    RoomType = stay.RoomType,
                    ArrivalDate = stay.ArrivalDate,
                    DepartureDate = stay.DepartureDate,
                    PointsSpend = stay.PointsSpend,
                    PointsEarned = stay.PointsEarned,
                    Price = stay.Price,
                    TotalPrice = stay.TotalPrice,
                    StayId = stay.Id,
                    IsCanceled = stay.IsCanceled,
                    BookedOn = stay.BookedOn
                    
                };
                rewardsViewModel.StayViewModels.Add(stayViewModel);
            }

            return View(rewardsViewModel);
        }

        public async Task<IActionResult> CancelReservation(int stayId)
        {
            if (stayId == 0)
            {
                return this.NotFound("Reservation not found!");
            }
            staysService.CancelReservation(stayId);

            //Will mark the reservation as canceled

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UsePoints(int stayId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var selectedStay = staysService.FindStayById(stayId);
            if (stayId == 0)
            {
                return NotFound("Reservation not found!");
            }
            if (selectedStay.TotalPrice < selectedStay.Price)
            {
                throw new InvalidOperationException("Invalid points usage!");
            }
            currentUser.UsedPoints += StaticData.FreeNightPoints;
            //Add the default points per night value to the user
            staysService.UsePoints(stayId);
            
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
