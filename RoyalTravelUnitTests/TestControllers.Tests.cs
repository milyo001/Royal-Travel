

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RoyalTravel.Controllers;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using RoyalTravel.Services.Hotel;
using RoyalTravel.Services.Room;
using RoyalTravel.Services.Stays;
using RoyalTravel.Services.User;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RoyalTravel.Tests
{
    public class TestControllers
    {
        [Fact]
        public void TestIfHomeControllerIndexActionReturnsIActionResult()
        {
            var testController = new HomeController();
            var result = testController.Index();
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void TestIfHomeControllerPrivacyActionReturnsIActionResult()
        {
            var testController = new HomeController();
            var result = testController.Privacy();
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void TestIfBookingControllerIndexActionReturnsIActionResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(options);
            var mockRoomService = new Mock<IRoomService>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            var result = testController.Index(null);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void TestIfBookingControllerSearchHotelsActionReturnsTaskIActionResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(options);
            var mockRoomService = new Mock<IRoomService>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            var result = testController.SearchHotels(null);

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestIfBookingControllerSelectHotelActionReturnsTaskIActionResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(options);
            var mockRoomService = new Mock<IRoomService>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            var result = testController.SelectHotel(null);

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestIfBookingControllerBookHotelActionReturnsTaskIActionResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(options);
            var mockRoomService = new Mock<IRoomService>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            var result = testController.BookHotel(null, "test", "test", 1, 1);

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestIfBookingControllerConfirmActionReturnsTaskIActionResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(options);
            var mockRoomService = new Mock<IRoomService>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            var result = testController.Confirm(null, "test", "test", 1, 1, "test");

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestIfRewardsControllerIndexActionReturnsIActionResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var mockUserService = new Mock<IUserService>();
            var applicationDbcontext = new ApplicationDbContext(options);

            applicationDbcontext.Rooms.Add(new Room
            {
                Id =1,
                MaxOccupancy =3,
                Description = "test",
                RoomType = "test"
            });

            applicationDbcontext.Users.Add(new ApplicationUser
            {
                Id = "TestUser",
                UserName = "justpichaga@gmail.com",
                NormalizedEmail = "JUSTPICHAGA@GMAIL.COM",
                Email = "justpichaga@gmail.com",
                NormalizedUserName = "JUSTPICHAGA@GMAIL.COM",
                EmailConfirmed = false,
                PhoneNumber = null,
                FirstName = "just",
                LastName = "pichaga",
            });

            applicationDbcontext.Stays.Add(new Stay
            {
                HotelId = 1,
                Id = 291328,
                Adults = 1,
                ArrivalDate = new DateTime(2020, 12, 25),
                DepartureDate = new DateTime(2020, 12, 26),
                ConfirmationNumber = "TestTestTest",
                Children = 0,
                BookedOn = DateTime.Today,
                ApplicationUserId = "TestUser",
                IsCanceled = false,
                MoneySpend = 23m,
                PointsEarned = 2340,
                PointsSpend = 222,
                Price = 23m,
                RoomId = 1,
                RoomType = "testReservation",
                TotalPrice = 23m
            });

            var mockStayService = new Mock<IStaysService>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new RewardsController(applicationDbcontext, mgr.Object, mockUserService.Object, mockStayService.Object);

            IActionResult result = testController.Index();

            Assert.IsAssignableFrom<IActionResult>(result);

            
        }
    }
}
