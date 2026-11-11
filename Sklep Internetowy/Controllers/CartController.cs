using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Extensions;
using Sklep_Internetowy.Models;
using System.Security.Claims;

namespace Sklep_Internetowy.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
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

        [HttpPost]
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

            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity.Name;
            var klient = await _context.Klienci.FirstOrDefaultAsync(k => k.Email == userEmail);

            if (klient == null)
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdClaim, out int parsedId))
                {
                    klient = await _context.Klienci.FirstOrDefaultAsync(k => k.IdUzytkownik == parsedId);
                }
            }

            if (klient == null)
            {
                TempData["Error"] = "Nie znaleziono profilu klienta dla Twojego konta.";
                return RedirectToAction("Index");
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

            decimal total = cart.Sum(x => x.Suma);
            int itemCount = cart.Sum(x => x.Ilosc);

            var noweZamowienie = new Zamowienie
            {
                DataZlozenia = DateTime.UtcNow,
                Status = StatusZamowienia.OCZEKUJACE,
                LacznyKoszt = total,
                MetodaPlatnosci = "Przelew online",
                MetodaDostawy = "Kurier Standard",
                KlientId = klient.IdUzytkownik,
                AdresDostawyUlica = klient.Ulica ?? "Brak",
                AdresDostawyKod = klient.KodPocztowy ?? "00-000",
                AdresDostawyMiasto = klient.Miasto ?? "Nie podano"
            };

            _context.Zamowienia.Add(noweZamowienie);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove(CartSessionKey);

            TempData["OrderNumber"] = $"ORD-{DateTime.Now:yyyyMMdd}-{noweZamowienie.IdZamowienia:D4}";
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