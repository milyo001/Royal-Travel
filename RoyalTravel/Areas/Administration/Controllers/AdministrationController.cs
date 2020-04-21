
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
        private readonly ApplicationDbContext dbContext;

        public AdministrationController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    
    }
}
