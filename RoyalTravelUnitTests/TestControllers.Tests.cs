

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RoyalTravel.Controllers;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using RoyalTravel.Services.Hotel;
using RoyalTravel.Services.Room;
using RoyalTravel.Services.Stays;
using RoyalTravel.Services.User;
using RoyalTravel.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace RoyalTravel.Tests
{
    public class TestControllers
    {

        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("Test Db")
                   .UseInternalServiceProvider(serviceProvider);       
            return builder.Options;
        }

        public static IEnumerable<object[]> TestBookingInputViewModelInvalidData
        {
            get
            {
                //Load the sample data from some source like JSON or CSV here.
                var sampleDataList = new List<BookingInputViewModel>
                {
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(0001, 01, 01),
                        CheckOut = new DateTime(2020, 12, 12), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(2020, 12, 12),
                        CheckOut = new DateTime(0001, 01, 01), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "TESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTEST", CheckIn = new DateTime(2020, 12, 15),
                        CheckOut = new DateTime(2020, 12, 16), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "&*(@!*)(!@*#()@!&^(@&^(!*(#@(*#@()#@()@(*)#@(*)@!#(*)#@!(*!@#(*)@#(", CheckIn = new DateTime(2020, 12, 15),
                        CheckOut = new DateTime(2020, 12, 16), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "", CheckIn = new DateTime(2020, 12, 15),
                        CheckOut = new DateTime(2020, 12, 16), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = " ", CheckIn = new DateTime(2020, 12, 15),
                        CheckOut = new DateTime(2020, 12, 16), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(2025, 12, 15),
                        CheckOut = new DateTime(2025, 12, 12), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(2025, 12, 15),
                        CheckOut = new DateTime(2025, 12, 17), Adults = 0, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(2000, 12, 12),
                        CheckOut = new DateTime(2025, 12, 12), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(2000, 12, 12),
                        CheckOut = new DateTime(2000, 12, 13), Adults = 1, Children = 1
                    },
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(2025, 12, 12),
                        CheckOut = new DateTime(2025, 12, 15), Adults = 0, Children = 0
                    },
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(2025, 12, 12),
                        CheckOut = new DateTime(2025, 12, 15), Adults = -1, Children = 0
                    },
                    new BookingInputViewModel
                    {
                        Destination = "bansko", CheckIn = new DateTime(2025, 12, 12),
                        CheckOut = new DateTime(2025, 12, 15), Adults = 1, Children = -1
                    },
             };

                var returnVal = new List<object[]>();
                foreach (var sampleData in sampleDataList)
                {
                    //Add the strongly typed data to an array of objects with one element. This is what xUnit expects.
                    returnVal.Add(new object[] { sampleData });
                }
                return returnVal;
            }
        }


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
        public void TestIfRewardsControllerCancelReservationActionReturnsRedirectTo()
        {

            var mockUserService = new Mock<IUserService>();
            var applicationDbcontext = new ApplicationDbContext(CreateNewContextOptions());
            applicationDbcontext.Database.EnsureCreated();

            var mockStayService = new Mock<IStaysService>();

            var store = new Mock<IUserStore<ApplicationUser>>();

            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new RewardsController(applicationDbcontext, mgr.Object, mockUserService.Object, mockStayService.Object);

            var result = testController.CancelReservation(0);


            Assert.IsAssignableFrom<Task<IActionResult>>(result);

        }

        [Fact]
        public void TestIfRewardsControllerUsePointActionReturnsRedirectTo()
        {

            var mockUserService = new Mock<IUserService>();
            var applicationDbcontext = new ApplicationDbContext(CreateNewContextOptions());
            applicationDbcontext.Database.EnsureCreated();

            var mockStayService = new Mock<IStaysService>();

            var store = new Mock<IUserStore<ApplicationUser>>();

            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new RewardsController(applicationDbcontext, mgr.Object, mockUserService.Object, mockStayService.Object);

            var result = testController.UsePoints(0);


            Assert.IsAssignableFrom<Task<IActionResult>>(result);

        }


        [Theory, MemberData(nameof(TestBookingInputViewModelInvalidData))]
        public void TestBookingControllerSearchHotelsActionSearchHotelsInvalidData(BookingInputViewModel testInputViewModel)
        {
            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(CreateNewContextOptions());
            var mockRoomService = new Mock<IRoomService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => testController.SearchHotels(testInputViewModel));
        }


        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        public void TestBookingControllerSearchHotelsActionSelectHotelIdInvalidData(int? id)
        {
            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(CreateNewContextOptions());
            var mockRoomService = new Mock<IRoomService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            var result = testController.SelectHotel(id);
            
            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
        }

        [Theory]
        [InlineData("21--22-2204", "cool date")]
        [InlineData("04-223-2204", "04232033")]
        [InlineData("04052033", "04272020")]
        public void TestBookingControllerBookHotelActionBookHotelInvalidDates(string checkIn, string checkOut)
        {
            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(CreateNewContextOptions());
            var mockRoomService = new Mock<IRoomService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => testController.BookHotel(1, checkIn, checkOut, 1, 0));
        }


        [Theory]
        [InlineData("04/01/2020", "03/01/2020")]
        [InlineData("04/01/2020", "02/01/2020")]
        [InlineData("04/01/2020", "03/01/2019")]
        public void TestBookingControllerSearchHotelsActionBookHotelInvalidData(string checkIn, string checkOut)
        {
            var mockHotelService = new Mock<IHotelService>();
            var applicationDbcontext = new ApplicationDbContext(CreateNewContextOptions());
            var mockRoomService = new Mock<IRoomService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            var testController = new BookingController(applicationDbcontext, mockHotelService.Object, mockRoomService.Object, mgr.Object);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => testController.BookHotel(1, checkIn,
            checkOut, 1, 0));
        }



    }
}
