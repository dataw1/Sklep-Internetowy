using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sklep_Internetowy.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RejestracjaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _context.Uzytkownicy.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Taki adres email jest już zarejestrowany.");
                return View(model);
            }

            var nowyKlient = new Klient
            {
                Email = model.Email,
                HasloHash = HashPassword(model.Haslo),
                Imie = model.Imie,
                Nazwisko = model.Nazwisko,
                Telefon = model.Telefon,
                Ulica = model.Ulica,
                NumerDomu = model.NumerDomu,
                KodPocztowy = model.KodPocztowy,
                Miasto = model.Miasto,
                DataRejestracji = DateTime.UtcNow
            };

            _context.Klienci.Add(nowyKlient);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LogowanieViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string hash = HashPassword(model.Haslo);
            var user = await _context.Uzytkownicy
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.HasloHash == hash);

            if (user == null)
            {
                ModelState.AddModelError("", "Błędny adres email lub hasło.");
                return View(model);
            }

            var zablokowanyKlient = await _context.Klienci.FirstOrDefaultAsync(k => k.IdUzytkownik == user.IdUzytkownik);

            if (zablokowanyKlient != null && zablokowanyKlient.CzyAktywny == false)
            {
                ModelState.AddModelError("", "Konto zostało zablokowane przez administratora.");
                return View(model);
            }

            string rola = user is Administrator ? "Administrator" : "Klient";

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.IdUzytkownik.ToString()),
        new Claim(ClaimTypes.Name, $"{user.Imie} {user.Nazwisko}"),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, rola)
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity.Name;

            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(k => k.Email == userEmail);

            if (uzytkownik == null)
            {
                var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(idClaim, out int parsedId))
                {
                    uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(k => k.IdUzytkownik == parsedId);
                }
            }

            if (uzytkownik == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new ProfilViewModel
            {
                Email = uzytkownik.Email,
                Imie = uzytkownik.Imie,
                Nazwisko = uzytkownik.Nazwisko
            };

            if (uzytkownik is Klient k)
            {
                model.Telefon = k.Telefon;
                model.Ulica = k.Ulica;
                model.NumerDomu = k.NumerDomu;
                model.KodPocztowy = k.KodPocztowy;
                model.Miasto = k.Miasto;
            }

            model.HistoriaZamowien = await _context.Zamowienia
                .Where(z => z.KlientId == uzytkownik.IdUzytkownik)
                .OrderByDescending(z => z.DataZlozenia)
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfilViewModel model)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity.Name;

            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(k => k.Email == userEmail);

            if (uzytkownik == null)
            {
                var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(idClaim, out int parsedId))
                {
                    uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(k => k.IdUzytkownik == parsedId);
                }
            }

            if (uzytkownik == null)
            {
                return NotFound();
            }

            uzytkownik.Imie = model.Imie;
            uzytkownik.Nazwisko = model.Nazwisko;

            if (uzytkownik is Klient k)
            {
                k.Telefon = model.Telefon;
                k.Ulica = model.Ulica;
                k.NumerDomu = model.NumerDomu;
                k.KodPocztowy = model.KodPocztowy;
                k.Miasto = model.Miasto;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Dane profilu zostały pomyślnie zaktualizowane!";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnulujZamowienie(int idZamowienia)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
                return RedirectToAction("Login");

            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity.Name;
            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (uzytkownik == null) return NotFound();

            var zamowienie = await _context.Zamowienia
                .FirstOrDefaultAsync(z => z.IdZamowienia == idZamowienia && z.KlientId == uzytkownik.IdUzytkownik);

            if (zamowienie != null)
            {
                if (zamowienie.Status == StatusZamowienia.OCZEKUJACE || zamowienie.Status == StatusZamowienia.OPLACONE)
                {
                    zamowienie.Status = StatusZamowienia.ANULOWANE;
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Zamówienie #{idZamowienia} zostało pomyślnie anulowane.";
                }
                else
                {
                    TempData["Error"] = "Tego zamówienia nie można już anulować.";
                }
            }

            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}