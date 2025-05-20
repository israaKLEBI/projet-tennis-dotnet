using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisShopApp.Data;
using TennisShopApp.Data.Models;

namespace TennisShopApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GainController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GainController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gains = await _context.Gains
                .Include(g => g.Tournoi)
                .Include(g => g.Joueur)
                .Include(g => g.Phase)
                .ToListAsync();
            return View(gains);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Tournois = await _context.Tournois.ToListAsync();
            ViewBag.Joueurs = await _context.Joueurs.ToListAsync();
            ViewBag.Phases = await _context.Phases.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gain gain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Tournois = await _context.Tournois.ToListAsync();
            ViewBag.Joueurs = await _context.Joueurs.ToListAsync();
            ViewBag.Phases = await _context.Phases.ToListAsync();
            return View(gain);
        }
    }
}