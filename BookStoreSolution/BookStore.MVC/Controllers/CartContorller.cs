using BookStore.MVC.Helpers;
using BookStore.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _http;
        private const string ApiBase = "https://localhost:7218/api/";
        private const string CartKey = "CART";

        public CartController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri(ApiBase);
        }

       
        private List<CartItem> GetCart()
            => HttpContext.Session.GetObject<List<CartItem>>(CartKey) ?? new List<CartItem>();
        private void SaveCart(List<CartItem> cart)
            => HttpContext.Session.SetObject(CartKey, cart);

        public IActionResult Index()
        {
            var cart = GetCart();
            ViewBag.CartCount = cart.Sum(x => x.Quantity);
            return View(cart);
        }

    
        [HttpPost]
        public async Task<IActionResult> Add(int id, int qty = 1)
        {
          
            var res = await _http.GetAsync($"books/{id}");
            if (!res.IsSuccessStatusCode)
            {
                TempData["error"] = "Kitap bulunamadı.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var json = await res.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(json);
            if (book == null)
            {
                TempData["error"] = "Kitap verisi okunamadı.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item == null)
            {
                cart.Add(new CartItem
                {
                    BookId = id,
                    Title = book.Title,
                    Price = book.Price,
                    ImageUrl = book.ImageUrl,
                    Quantity = Math.Max(1, qty)
                });
            }
            else
            {
                item.Quantity += Math.Max(1, qty);
            }

            SaveCart(cart);
            TempData["success"] = "Ürün sepete eklendi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id, int qty)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item != null)
            {
                item.Quantity = Math.Max(1, qty);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

  
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.BookId == id);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

    
        [Authorize] 
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any()) { TempData["error"] = "Sepetiniz boş."; return RedirectToAction("Index"); }
            ViewBag.Total = cart.Sum(x => x.Price * x.Quantity);
            return View(cart);
        }

   
        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirm()
        {
            var cart = GetCart();
            if (!cart.Any()) { TempData["error"] = "Sepetiniz boş."; return RedirectToAction("Index"); }

        
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value ?? "0";
            int.TryParse(userIdStr, out var userId);

            foreach (var item in cart)
            {
                var order = new
                {
                    userId = userId,
                    bookId = item.BookId,
                    quantity = item.Quantity,
                    orderDate = DateTime.UtcNow
                };

                var content = new StringContent(
                    JsonConvert.SerializeObject(order),
                    System.Text.Encoding.UTF8, "application/json");

                var res = await _http.PostAsync("orders", content);
                if (!res.IsSuccessStatusCode)
                {
                    TempData["error"] = "Sipariş kaydı sırasında hata oluştu.";
                    return RedirectToAction("Index");
                }
            }

           
            SaveCart(new List<CartItem>());
            TempData["success"] = "Siparişiniz oluşturuldu.";
            return RedirectToAction("Index", "Home");
        }
    }
}