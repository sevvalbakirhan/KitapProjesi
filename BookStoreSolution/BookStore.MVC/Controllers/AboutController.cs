using Microsoft.AspNetCore.Mvc;

namespace BookStore.MVC.Controllers
{
    public class AboutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Hakkımızda";
            ViewData["Mission"] = "Uygun fiyatlı, geniş yelpazede kitaplar.";
            return View();
        }
    }
}