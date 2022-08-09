
using System.Collections.Generic;

namespace RoyalTravel.ViewModels.Booking
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            this.SearchResults = new List<SearchResultsViewModel>();
            this.UserSearchInput = new UserSearchInputViewModel();
        }
        
        public List<SearchResultsViewModel> SearchResults { get; set; }

        public UserSearchInputViewModel UserSearchInput { get; set; }
    }
}
