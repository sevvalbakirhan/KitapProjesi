using Microsoft.AspNetCore.Mvc;

namespace BookStore.MVC.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Bize Ulaşın";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string email, string message)
        {
            TempData["success"] = "Mesajınız başarıyla alındı. Teşekkür ederiz!";
            return RedirectToAction(nameof(Index));
        }
    }
}