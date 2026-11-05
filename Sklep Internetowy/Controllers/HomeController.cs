using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, string? kategoria, string? sortOrder)
        {
            ViewBag.Kategorie = await _context.Produkty
                .Select(p => p.Kategoria)
                .Where(k => !string.IsNullOrEmpty(k))
                .Distinct()
                .ToListAsync();

            ViewBag.CurrentSearch = search;
            ViewBag.CurrentCategory = kategoria;
            ViewBag.CurrentSort = sortOrder;

            var query = _context.Produkty.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Nazwa.Contains(search) || (p.Opis != null && p.Opis.Contains(search)));
            }

            if (!string.IsNullOrWhiteSpace(kategoria))
            {
                query = query.Where(p => p.Kategoria == kategoria);
            }

            query = sortOrder switch
            {
                "cena_rosnaco" => query.OrderBy(p => p.Cena),
                "cena_malejaco" => query.OrderByDescending(p => p.Cena),
                "nazwa_desc" => query.OrderByDescending(p => p.Nazwa),
                _ => query.OrderBy(p => p.Nazwa) 
            };

            var produkty = await query.ToListAsync();
            return View(produkty);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}