using BookStore.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.MVC.Controllers
{

    public class FavoritesController : Controller
    {
        private readonly HttpClient _http;
        private const string ApiBase = "https://localhost:7218/api/"; 

        public FavoritesController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri(ApiBase);
        }

        int GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            return int.TryParse(claim, out var id) ? id : 0;
        }

      
        public async Task<IActionResult> Index()
        {
            var uid = GetUserId();
            if (uid == 0) return RedirectToAction("Login", "Auth");

            var res = await _http.GetAsync($"favorites/user/{uid}");
            var list = new List<Favorite>();
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Favorite>>(json) ?? new();
            }
            ViewBag.Title = "Favorilerim";
            return View(list);
        }

       
        [HttpPost]
        public async Task<IActionResult> Add(int id) 
        {
            var uid = GetUserId();
            if (uid == 0) return RedirectToAction("Login", "Auth");

            var payload = new { userId = uid, bookId = id };
            var content = new StringContent(JsonConvert.SerializeObject(payload),
                                            System.Text.Encoding.UTF8, "application/json");
            var res = await _http.PostAsync("favorites", content);

            TempData[res.IsSuccessStatusCode ? "success" : "error"] =
                res.IsSuccessStatusCode ? "Favorilere eklendi." : "Favorilere eklenemedi.";
            return Redirect(Request.Headers["Referer"].ToString());
        }


        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var res = await _http.DeleteAsync($"favorites/{id}");
            TempData[res.IsSuccessStatusCode ? "success" : "error"] =
                res.IsSuccessStatusCode ? "Favoriden çıkarıldı." : "İşlem başarısız.";
            return RedirectToAction(nameof(Index));
        }
    }
}