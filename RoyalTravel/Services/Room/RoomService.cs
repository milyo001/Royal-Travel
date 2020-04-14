using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTravel.Services.Room
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext db;

        public RoomService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddReservation(Stay stay)
        {
            db.Stays.Add(stay);
            db.SaveChanges();
        }

        public string GenerateConfirmationNumber(string hotelName)
        {
            var confirmationNumber = string.Empty;
            var sb = new StringBuilder();
            for (int i = 0; i < StaticData.ConfirmationNumberLetterLenght; i++)
            {
                if (hotelName.Length >= StaticData.ConfirmationNumberLetterLenght)
                {
                    if (!char.IsWhiteSpace(hotelName[i]))
                    {
                        sb.Append(hotelName[i]);
                    }
                    
                }
            }
            //Add the first letters of the hotel name
            sb.Append(DateTime.Today.Month.ToString("00"));

            //Add the month of booking to the conf. number
            sb.Append(DateTime.Today.Day.ToString("00"));
            //Add the year of booking to the conf. number
            sb.Append(DateTime.Today.Year.ToString());
            //Add the year of booking to the conf. number


            var random = new Random();
            sb.Append(random.Next(10000, 99999).ToString()); 
            //Add 5 more random numbers to the conf number
            confirmationNumber = sb.ToString().ToUpper();

            if (db.Stays.Any(s => s.ConfirmationNumber.Contains(confirmationNumber)))
            {
                GenerateConfirmationNumber(hotelName);
                //If the present conf. number is already in the database, enter recursion and generate new unique one
            }
            return confirmationNumber;
        }
    }
}
