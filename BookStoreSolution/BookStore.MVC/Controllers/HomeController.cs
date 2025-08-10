using Microsoft.AspNetCore.Mvc;

namespace BookStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Anasayfa";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "Hakk�m�zda";
            ViewData["Mission"] = "Uygun fiyatl�, geni� yelpazede kitaplar.";
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ViewBag.Title = "Bize Ula��n";
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string name, string email, string message)
        {
            TempData["success"] = "Mesaj�n�z al�nd�. En k�sa s�rede d�n�� yapaca��z.";
            return RedirectToAction(nameof(Contact));
        }
    }
}