

namespace RoyalTravel.Data
{
    public static class StaticData
    {
        public static int PointsMultiplier = 10;
        //For each dollar the user will get * 10 points

        public static int RewardsTier1Requirement = 15000;

        public static int RewardsTier2Requirement = 30000;

        public static int RewardsTier3Requirement = 40000;

        public static int ConfirmationNumberLetterLenght = 2;
        //Default value for the lenght of confirmation number, which will take the first letters of the hotel name,
        //check the GenerateConfirmationNumber method in RoomService

        public static int ConfirmationNumberLenght = 8;
        //Default value for the lenght of confirmation number for reservation after the other elements, 
        //check the GenerateConfirmationNumber method in RoomService

        public enum Tiers
        {
            Silver = 0,
            Gold = 1,
            Platinum = 2
        }

        public static int FreeNightPoints = 15000;

        public static int Commission = 8;

    }
}
