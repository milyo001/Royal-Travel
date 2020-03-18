
using Microsoft.AspNetCore.Identity;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTravel.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ApplicationUser GetUser(string id)
        {
             var user = this.db.Users
                .FirstOrDefault(u => u.Id == id);
            return user;
        }

        public bool EmailExists(string email)
        {
            return this.db.Users.Any(x => x.Email == email);
        }
    }
}
