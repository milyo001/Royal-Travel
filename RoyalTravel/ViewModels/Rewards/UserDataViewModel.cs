

namespace RoyalTravel.ViewModels.Rewards
{
    public class UserDataViewModel
    {
        public int Points { get; set; }

        public enum Loyalty
        {
            Silver = 0,
            Gold = 1,
            Platinium = 2
        }

    }
}
