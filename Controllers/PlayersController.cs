using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LPLWebApp.Data;
using LPLWebApp.Models;

namespace LPLWebApp.Controllers
{
    public class PlayersController : Controller
    {
        private readonly LPLWebAppContext _context;

        public PlayersController(LPLWebAppContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            return View(await _context.Player.ToListAsync());
        }

        public async Task<IActionResult> ShowSearch(string searchString)
        {
            List<string> teams = new List<string>() 
            {"BLG", "JDG", "TES", "FPX", "NIP", "IG", "WE", "AL", "WBG", "OMG", "LNG", "TT", "RA", "RNG", "LGD", "EDG", "UP"};

            ViewBag.Teams = new SelectList(teams);

            List<string> playerIgn = _context.Player.Select(p => p.Ign).ToList();

            ViewBag.SearchString = searchString;

            string temp = searchString as string ?? "";

            ViewBag.filteredIgns = new List<string>();

            foreach (string Ign in playerIgn)
            {
                if (Ign.StartsWith(temp, StringComparison.OrdinalIgnoreCase))
                {
                    ViewBag.filteredIgns.Add(Ign);
                }
            }

            return View(searchString);
        }

        public async Task<IActionResult> DisplayPlayerSearchResult(String searchString)
        {
            return View("Index", await _context.Player.Where(j => j.Ign.StartsWith(searchString)).ToListAsync());
        }

        public async Task<IActionResult> DisplayTeamSearchResult(String SearchPhrase)
        {
            return View("Index", await _context.Player.Where(j => j.Team.StartsWith(SearchPhrase)).ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            List<string> roles = new List<string>()
            {
                "Top",
                "Jungle",
                "Mid",
                "Bot",
                "Support"
            };

            ViewBag.Roles = new SelectList(roles);

            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Ign,Name,Age,Team,Position")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<string> roles = new List<string>()
            {
                "Top",
                "Jungle",
                "Mid",
                "Bot",
                "Support"
            };

            ViewBag.Roles = new SelectList(roles);

            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Ign,Name,Age,Team,Position")] Player player)
        {
            if (id != player.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.id == id);
        }

    }
}
