
using System.Collections.Generic;

namespace RoyalTravel.ViewModels.Rewards
{
    public class RewardsInputModel
    {
        public RewardsInputModel()
        {
            this.StayViewModels = new List<StayViewModel>();
            this.UserDataViewModel = new UserDataViewModel();
        }
       public List<StayViewModel> StayViewModels { get; set; }

        public UserDataViewModel UserDataViewModel { get; set; }
    }
}
