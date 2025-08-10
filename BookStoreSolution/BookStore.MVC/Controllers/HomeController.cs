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
            ViewBag.Title = "Hakkýmýzda";
            ViewData["Mission"] = "Uygun fiyatlý, geniþ yelpazede kitaplar.";
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ViewBag.Title = "Bize Ulaþýn";
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string name, string email, string message)
        {
            TempData["success"] = "Mesajýnýz alýndý. En kýsa sürede dönüþ yapacaðýz.";
            return RedirectToAction(nameof(Contact));
        }
    }
}