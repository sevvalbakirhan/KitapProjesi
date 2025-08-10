using BookStore.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly HttpClient _http;
        private const string ApiBase = "https://localhost:7218/api/";

        public OrdersController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri(ApiBase);
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<OrderRow>();
            var res = await _http.GetAsync("orders"); 
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<OrderRow>>(json) ?? new();
            }
            return View(list);
        }
    }

    public class OrderRow
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public Book? Book { get; set; }
        public User? User { get; set; }
    }
}