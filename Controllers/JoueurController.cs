using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisShopApp.Data;
using TennisShopApp.Data.Models;

namespace TennisShopApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class JoueurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JoueurController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Joueurs.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomPrenom,Score")] Joueur joueur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joueur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(joueur);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var joueur = await _context.Joueurs.FindAsync(id);
            if (joueur == null) return NotFound();
            return View(joueur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodeJ,NomPrenom,Score")] Joueur joueur)
        {
            if (id != joueur.CodeJ) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joueur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Joueurs.Any(e => e.CodeJ == joueur.CodeJ)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(joueur);
        }
    }
}