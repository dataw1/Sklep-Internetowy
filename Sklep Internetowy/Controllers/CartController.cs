using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Extensions;
using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private const string CartSessionKey = "UserCart";

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        private List<CartItem> GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
        }


        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
        }


        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }


        public async Task<IActionResult> AddToCart(int id)
        {
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null) return NotFound();

            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.IdProdukt == id);

            if (item != null)
            {
                item.Ilosc++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    IdProdukt = produkt.IdProdukt,
                    Nazwa = produkt.Nazwa,
                    Cena = produkt.Cena,
                    Ilosc = 1
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.IdProdukt == id);
            SaveCart(cart);
            return RedirectToAction("Index");
        }


        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                TempData["CartWarning"] = "Twój koszyk jest pusty!";
                return RedirectToAction("Index");
            }


            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                TempData["AuthRequired"] = "Aby z³o¿yæ zamówienie i przejœæ do kasy, musisz najpierw za³o¿yæ konto lub siê zalogowaæ.";
                return RedirectToAction("Register", "Account");
            }


            decimal total = cart.Sum(x => x.Suma);
            int itemCount = cart.Sum(x => x.Ilosc);


            string orderNumber = $"ORD-{DateTime.Now:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";


            HttpContext.Session.Remove(CartSessionKey);


            TempData["OrderNumber"] = orderNumber;
            TempData["OrderTotal"] = total.ToString("C");
            TempData["OrderItemCount"] = itemCount;

            return RedirectToAction("OrderSuccess");
        }


        public IActionResult OrderSuccess()
        {
            if (TempData["OrderNumber"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}