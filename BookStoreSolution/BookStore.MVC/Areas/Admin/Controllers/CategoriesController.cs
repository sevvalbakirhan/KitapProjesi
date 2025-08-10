using BookStore.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly HttpClient _http;
        private const string ApiBase = "https://localhost:7218/api/"; 

        public CategoriesController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri(ApiBase);
        }

   
        public async Task<IActionResult> Index()
        {
            var list = new List<Category>();
            var res = await _http.GetAsync("categories");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Category>>(json) ?? new();
            }
            return View(list);
        }

        
        [HttpGet]
        public IActionResult Create() => View(new Category());

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid) return View(model);

            var content = new StringContent(
                JsonConvert.SerializeObject(model),
                System.Text.Encoding.UTF8,
                "application/json");

            var res = await _http.PostAsync("categories", content);
            if (!res.IsSuccessStatusCode)
            {
                TempData["error"] = "Kategori kaydedilemedi.";
                return View(model);
            }

            TempData["success"] = "Kategori eklendi.";
            return RedirectToAction(nameof(Index));
        }

    
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var res = await _http.GetAsync($"categories/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Category>(json);
            if (model == null) return NotFound();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid) return View(model);

            var content = new StringContent(
                JsonConvert.SerializeObject(model),
                System.Text.Encoding.UTF8,
                "application/json");

            var res = await _http.PutAsync($"categories/{model.Id}", content);
            if (!res.IsSuccessStatusCode)
            {
                TempData["error"] = "Kategori güncellenemedi.";
                return View(model);
            }

            TempData["success"] = "Kategori güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _http.GetAsync($"categories/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Category>(json);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var res = await _http.DeleteAsync($"categories/{id}");
            TempData[res.IsSuccessStatusCode ? "success" : "error"] =
                res.IsSuccessStatusCode ? "Kategori silindi." : "Kategori silinemedi.";
            return RedirectToAction(nameof(Index));
        }
    }
}