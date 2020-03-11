
using System.Collections.Generic;

namespace RoyalTravel.ViewModels.Rewards
{
    public class RewardsInputModel
    {
        public RewardsInputModel()
        {
            this.StayViewModel = new List<StayViewModel>();
            this.UserDataViewModel = new UserDataViewModel();
        }
       public List<StayViewModel> StayViewModel { get; set; }

        public UserDataViewModel UserDataViewModel { get; set; }
    }
}
