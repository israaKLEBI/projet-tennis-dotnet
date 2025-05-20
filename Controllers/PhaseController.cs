using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisShopApp.Data;
using TennisShopApp.Data.Models;

namespace TennisShopApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Phase
        public async Task<IActionResult> Index()
        {
            var phases = await _context.Phases.ToListAsync();
            return View(phases);
        }

        // GET: /Phase/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Phase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Phase phase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phase);
        }
    }
}