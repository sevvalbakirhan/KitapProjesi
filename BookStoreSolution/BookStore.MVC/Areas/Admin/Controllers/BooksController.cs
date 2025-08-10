using BookStore.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private readonly HttpClient _http;
        private readonly IWebHostEnvironment _env;

        private const string ApiBase = "https://localhost:7218/api/";

        public BooksController(IHttpClientFactory factory, IWebHostEnvironment env)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri(ApiBase);
            _env = env;
        }

     
        private async Task<List<Category>> LoadCategoriesAsync()
        {
            var list = new List<Category>();
            var res = await _http.GetAsync("categories");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Category>>(json) ?? new();
            }
            return list;
        }

        private string? SaveImage(IFormFile? image)
        {
            if (image == null || image.Length == 0) return null;

            var folder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(image.FileName)}";
            var path = Path.Combine(folder, fileName);

            using (var fs = new FileStream(path, FileMode.Create))
                image.CopyTo(fs);

            return $"/images/{fileName}";
        }

   
        public async Task<IActionResult> Index()
        {
            var list = new List<Book>();
            var res = await _http.GetAsync("books");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Book>>(json) ?? new();
            }
            return View(list);
        }

      
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await LoadCategoriesAsync();
            return View(new Book());
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book model, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await LoadCategoriesAsync();
                return View(model);
            }

            var imageUrl = SaveImage(image);
            if (!string.IsNullOrWhiteSpace(imageUrl))
                model.ImageUrl = imageUrl;

            var content = new StringContent(JsonConvert.SerializeObject(model),
                                            System.Text.Encoding.UTF8, "application/json");

            var res = await _http.PostAsync("books", content);
            if (!res.IsSuccessStatusCode)
            {
                TempData["error"] = "Kitap kaydedilemedi.";
                ViewBag.Categories = await LoadCategoriesAsync();
                return View(model);
            }

            TempData["success"] = "Kitap eklendi.";
            return RedirectToAction(nameof(Index));
        }

    
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var res = await _http.GetAsync($"books/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Book>(json);
            if (model == null) return NotFound();

            ViewBag.Categories = await LoadCategoriesAsync();
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book model, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await LoadCategoriesAsync();
                return View(model);
            }

            var imageUrl = SaveImage(image);
            if (!string.IsNullOrWhiteSpace(imageUrl))
                model.ImageUrl = imageUrl; 

            var content = new StringContent(JsonConvert.SerializeObject(model),
                                            System.Text.Encoding.UTF8, "application/json");

            var res = await _http.PutAsync($"books/{model.Id}", content);
            if (!res.IsSuccessStatusCode)
            {
                TempData["error"] = "Kitap güncellenemedi.";
                ViewBag.Categories = await LoadCategoriesAsync();
                return View(model);
            }

            TempData["success"] = "Kitap güncellendi.";
            return RedirectToAction(nameof(Index));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _http.DeleteAsync($"books/{id}");
            TempData[res.IsSuccessStatusCode ? "success" : "error"] =
                res.IsSuccessStatusCode ? "Kitap silindi." : "Kitap silinemedi.";
            return RedirectToAction(nameof(Index));
        }
    }
}