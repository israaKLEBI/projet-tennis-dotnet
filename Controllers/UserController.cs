using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisShopApp.Data;
using TennisShopApp.Models;

namespace TennisShopApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var viewModel = new UserDashboardViewModel
            {
                UserEmail = User.Identity?.Name ?? string.Empty,
                TotalPlayers = await _context.Joueurs.CountAsync(),
                TotalTournaments = await _context.Tournois.CountAsync(),
                TotalGains = await _context.Gains.CountAsync()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Tournaments()
        {
            var tournaments = await _context.Tournois
                .Select(t => new
                {
                    t.CodeT,
                    t.NomT,
                    t.Ville,
                    GainCount = t.Gains.Count
                })
                .ToListAsync();
            return View(tournaments);
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var viewModel = new UserProfileViewModel
            {
                Email = user.Email ?? string.Empty,
                DisplayName = user.UserName ?? string.Empty,
                RegistrationDate = user.Id // Placeholder
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return NotFound();

                if (user.UserName != model.DisplayName)
                {
                    user.UserName = model.DisplayName;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }
                return RedirectToAction(nameof(Dashboard));
            }
            return View(model);
        }
    }

    public class UserDashboardViewModel
    {
        public string UserEmail { get; set; } = string.Empty;
        public int TotalPlayers { get; set; }
        public int TotalTournaments { get; set; }
        public int TotalGains { get; set; }
    }
}