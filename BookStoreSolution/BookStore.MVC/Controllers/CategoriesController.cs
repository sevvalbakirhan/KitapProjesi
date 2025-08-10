using BookStore.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class CategoriesController : Controller
{
    private readonly HttpClient _httpClient;

    public CategoriesController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("categories");
        if (!response.IsSuccessStatusCode)
        {
            return View(new List<Category>());
        }

        var json = await response.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<List<Category>>(json);

        ViewBag.Title = "Kategoriler";
        return View(categories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"categories/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        var json = await response.Content.ReadAsStringAsync();
        var category = JsonConvert.DeserializeObject<Category>(json);

        ViewBag.Title = "Kategori Detayı";
        return View(category);
    }
}