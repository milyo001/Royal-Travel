
using System.Collections.Generic;

namespace RoyalTravel.ViewModels.Booking
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            this.BookingOutputViewModels = new List<BookingOutputViewModel>();
            this.InputModel = new BookingInputViewModel();
        }

        public List<BookingOutputViewModel> BookingOutputViewModels { get; set; }

        public BookingInputViewModel InputModel { get; set; }
    }
}
