using BookStore.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class BooksController : Controller
{
    private readonly HttpClient _httpClient;  

    public BooksController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7218/api/"); 
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("books");
        if (!response.IsSuccessStatusCode)
        {
            return View(new List<Book>());
        }

        var json = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<Book>>(json);

        ViewBag.Title = "Kitap Listesi";
        return View(books);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"books/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        var json = await response.Content.ReadAsStringAsync();
        var book = JsonConvert.DeserializeObject<Book>(json);

        ViewBag.Category = book.Category?.Name;
        return View(book);
    }
}