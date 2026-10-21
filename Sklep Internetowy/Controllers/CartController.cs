using Microsoft.AspNetCore.Mvc;

namespace Sklep_internetowy.Controllers
{
    // Prosty model pozycji w koszyku
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }

    public class CartController : Controller
    {
        public IActionResult Index()
        {
            // Przyk³adowe dane produktów w koszyku
            var cartItems = new List<CartItem>
            {
                new CartItem { Id = 1, Name = "Klawiatura mechaniczna", Price = 299.99m, Quantity = 1 },
                new CartItem { Id = 2, Name = "Mysz bezprzewodowa", Price = 149.50m, Quantity = 2 }
            };

            return View(cartItems);
        }
    }
}