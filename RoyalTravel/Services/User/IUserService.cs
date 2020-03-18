using RoyalTravel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTravel.Services.User
{
    public interface IUserService
    {
       ApplicationUser GetUser(string id);

        bool EmailExists(string email);
        
    }
}
