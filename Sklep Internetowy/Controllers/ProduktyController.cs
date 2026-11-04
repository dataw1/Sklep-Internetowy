using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProduktyController : Controller
    {
        private readonly AppDbContext _context;

        public ProduktyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produkty
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var produkty = await _context.Produkty
                .OrderByDescending(p => p.IdProdukt) // Zmieniono na IdProdukt
                .ToListAsync();
            return View(produkty);
        }

        // GET: Produkty/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produkty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                _context.Produkty.Add(produkt);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Pomyślnie dodano produkt: {produkt.Nazwa}";
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        // GET: Produkty/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }
            return View(produkt);
        }

        // POST: Produkty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produkt daneProduktu)
        {
            if (id != daneProduktu.IdProdukt) // Zmieniono na IdProdukt
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var produktWBazie = await _context.Produkty.FindAsync(id);
                if (produktWBazie == null)
                {
                    return NotFound();
                }

                produktWBazie.Nazwa = daneProduktu.Nazwa;
                produktWBazie.Opis = daneProduktu.Opis;
                produktWBazie.Cena = daneProduktu.Cena; // Zmieniono na Cena
                produktWBazie.IloscWMagazynie = daneProduktu.IloscWMagazynie; // Zmieniono na IloscWMagazynie
                produktWBazie.ZdjecieUrl = daneProduktu.ZdjecieUrl; // Zmieniono na ZdjecieUrl
                produktWBazie.Kategoria = daneProduktu.Kategoria;

                await _context.SaveChangesAsync();
                TempData["Success"] = $"Zaktualizowano produkt: {produktWBazie.Nazwa}";
                return RedirectToAction(nameof(Index));
            }
            return View(daneProduktu);
        }

        // POST: Produkty/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int idProduktu) // Parametr z widoku
        {
            var produkt = await _context.Produkty.FindAsync(idProduktu);
            if (produkt != null)
            {
                _context.Produkty.Remove(produkt);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Usunięto produkt: {produkt.Nazwa}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}