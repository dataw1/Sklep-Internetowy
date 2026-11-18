using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ZamowieniaAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ZamowieniaAdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var zamowienia = await _context.Zamowienia
                .OrderByDescending(z => z.DataZlozenia)
                .ToListAsync();

            return View(zamowienia);
        }
    }
}