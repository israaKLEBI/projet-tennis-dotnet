using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisShopApp.Data;
using TennisShopApp.Data.Models;

namespace TennisShopApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TournoiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournoiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tournois.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomT,Ville")] Tournoi tournoi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tournoi);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var tournoi = await _context.Tournois.FindAsync(id);
            if (tournoi == null) return NotFound();
            return View(tournoi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodeT,NomT,Ville")] Tournoi tournoi)
        {
            if (id != tournoi.CodeT) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tournois.Any(e => e.CodeT == tournoi.CodeT)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tournoi);
        }
    }
}