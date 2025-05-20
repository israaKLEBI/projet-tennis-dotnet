using Microsoft.AspNetCore.Mvc;

namespace TennisShopApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}