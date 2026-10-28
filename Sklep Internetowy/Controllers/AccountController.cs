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