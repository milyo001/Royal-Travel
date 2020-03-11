
using Microsoft.AspNetCore.Identity;
using RoyalTravel.Data;
using RoyalTravel.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RoyalTravel.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        
        public UserService(ApplicationDbContext db)
        {
            this._db = db;
        }
        public ApplicationUser GetUser(string id)
        {
            var user = _db.Users
                .FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}
