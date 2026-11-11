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
        // Zmieniony klucz, aby wszêdzie by³ taki sam ("Cart")
        private const string CartSessionKey = "Cart";

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

        [HttpGet]
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost] // Zmienione na HttpPost, bo u¿ywamy <form method="post">
        public async Task<IActionResult> AddToCart(int id, int ilosc = 1)
        {
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }

            if (ilosc < 1)
            {
                ilosc = 1;
            }

            // U¿ywamy ujednoliconej metody GetCart
            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(c => c.IdProdukt == id);
            int obecnaIloscWKoszyku = existingItem != null ? existingItem.Ilosc : 0;

            if (obecnaIloscWKoszyku + ilosc > produkt.IloscWMagazynie)
            {
                TempData["Error"] = $"Nie mo¿na dodaæ tylu sztuk. W magazynie mamy tylko {produkt.IloscWMagazynie} szt. (w koszyku masz ju¿ {obecnaIloscWKoszyku} szt.).";
            }
            else
            {
                if (existingItem != null)
                {
                    existingItem.Ilosc += ilosc;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        IdProdukt = produkt.IdProdukt,
                        Nazwa = produkt.Nazwa,
                        Cena = produkt.Cena,
                        Ilosc = ilosc
                    });
                }

                // U¿ywamy ujednoliconej metody SaveCart
                SaveCart(cart);
                TempData["Success"] = $"Dodano {produkt.Nazwa} do koszyka!";
            }

            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.IdProdukt == id);
            SaveCart(cart);
            TempData["Success"] = "Usuniêto produkt z koszyka.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
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

            foreach (var item in cart)
            {
                var produktWBazie = await _context.Produkty.FindAsync(item.IdProdukt);
                if (produktWBazie != null)
                {
                    produktWBazie.IloscWMagazynie -= item.Ilosc;

                    if (produktWBazie.IloscWMagazynie < 0)
                    {
                        produktWBazie.IloscWMagazynie = 0;
                    }
                }
            }

            await _context.SaveChangesAsync();

            decimal total = cart.Sum(x => x.Suma);
            int itemCount = cart.Sum(x => x.Ilosc);

            string orderNumber = $"ORD-{DateTime.Now:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";

            HttpContext.Session.Remove(CartSessionKey);

            TempData["OrderNumber"] = orderNumber;
            TempData["OrderTotal"] = total.ToString("C");
            TempData["OrderItemCount"] = itemCount;

            return RedirectToAction("OrderSuccess");
        }

        [HttpGet]
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