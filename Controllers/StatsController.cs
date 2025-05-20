using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisShopApp.Data;

namespace TennisShopApp.Controllers
{
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var topPlayers = await _context.Joueurs
                .OrderByDescending(j => j.Score)
                .Take(5)
                .Select(j => new { j.NomPrenom, j.Score })
                .ToListAsync();
            ViewBag.TopPlayers = topPlayers;
            return View();
        }
    }
}