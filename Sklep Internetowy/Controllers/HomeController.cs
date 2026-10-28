using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;
using System.Diagnostics;

namespace Sklep_Internetowy.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var produkty = await _context.Produkty.ToListAsync();
            return View(produkty);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}