
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoyalTravel.Data;

namespace RoyalTravel.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Policy = "RequireAdmin")]
    public class AdministrationController : Controller
    {

        
        public AdministrationController(ApplicationDbContext dbContext)
        {
           
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
