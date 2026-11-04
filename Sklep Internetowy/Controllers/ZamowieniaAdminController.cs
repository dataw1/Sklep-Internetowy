using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UzytkownicyAdminController : Controller
    {
        private readonly AppDbContext _context;

        public UzytkownicyAdminController(AppDbContext context)
        {
            _context = context;
        }

        // Wyświetlenie wszystkich klientów (z pominięciem administratorów)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var klienci = await _context.Klienci
                .OrderByDescending(k => k.DataRejestracji)
                .ToListAsync();

            return View(klienci);
        }

        // POST: zarzadzajUzytkownikiem(int, bool) z diagramu UML (aktywacja / blokada konta)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZarzadzajUzytkownikiem(int idUzytkownika, bool czyAktywny)
        {
            var klient = await _context.Klienci.FindAsync(idUzytkownika);
            if (klient == null)
            {
                return NotFound();
            }

            klient.CzyAktywny = czyAktywny;
            await _context.SaveChangesAsync();

            string stan = czyAktywny ? "odblokowano" : "zablokowano";
            TempData["Success"] = $"Pomyślnie {stan} konto klienta: {klient.Imie} {klient.Nazwisko}.";
            return RedirectToAction(nameof(Index));
        }

        // Dodatkowa akcja całkowitego usunięcia konta klienta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UsunKlienta(int idUzytkownika)
        {
            var klient = await _context.Klienci.FindAsync(idUzytkownika);
            if (klient != null)
            {
                _context.Klienci.Remove(klient);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Usunięto konto klienta: {klient.Email}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}