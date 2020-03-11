using RoyalTravel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTravel.Services.User
{
    public interface IUserService
    {
        public ApplicationUser GetUser(string id);
    }
}
