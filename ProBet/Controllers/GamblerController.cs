using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProBet.Models;
using ProBet.ViewModels;

namespace ProBet.Controllers
{
    [Authorize(Roles = "Gambler")]
    public class GamblerController : Controller
    {
        private readonly ProBetContext _context;
        private readonly ProBetContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> userManager;

        public GamblerController(ProBetContext context, UserManager<AppUser> userMgr, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            dbContext = context;
            webHostEnvironment = hostEnvironment;
            userManager = userMgr;
        }
        public async Task<IActionResult> GamblerHome(int? id)
        {
            if (id == null)
            {
                AppUser curruser = await userManager.GetUserAsync(User);
                var gamblerce = await _context.Gambler
                    .Include(m => m.Tickets).ThenInclude(m => m.Match)
                    .FirstOrDefaultAsync(m => m.Id == curruser.GamblerId);
                if (gamblerce != null)
                {
                    return View(gamblerce);
                }
                else
                {
                    return NotFound();
                }
            }

            var gambler = await _context.Gambler
                .Include(m => m.Tickets).ThenInclude(m => m.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gambler == null)
            {
                return NotFound();
            }
            return View(gambler);
        }
        public async Task<IActionResult> TopFive()
        {
            var gamblers = from g in _context.Gambler
                           select g;
            gamblers = gamblers.OrderByDescending(g => g.Earnings).Take(5);
            return View(await gamblers.ToListAsync());
        }
        public async Task<IActionResult> Scores()
        {
            var matches = from m in _context.Match
                          select m;
            matches = matches.Where(m => m.HomeGoals.HasValue);
            return View(await matches.ToListAsync());
        }
        public IActionResult BetHomeTeam()
        {
            ViewData["GamblerId"] = new SelectList(_context.Gambler, "Id", "FullName");
            ViewData["MatchId"] = new SelectList(_context.Set<Match>(), "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BetHomeTeam([FromRoute]int id, [Bind("Id,BetMoney")]Ticket tickety)
        {
            AppUser curruser = await userManager.GetUserAsync(User);
            DateTime now = DateTime.Now;
            var match = await _context.Match.FirstOrDefaultAsync(m => m.Id == id);
            Ticket ticket = new Ticket
            {
                BetTime = now,
                MatchId = match.Id,
                BetMoney = tickety.BetMoney,
                GamblerId = (int)curruser.GamblerId,
                Tip = 1
            };
            ticket.WinMoney = ticket.BetMoney * match.HomeOdds * (float)0.85;
            _context.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult BetDraw()
        {
            ViewData["GamblerId"] = new SelectList(_context.Gambler, "Id", "FullName");
            ViewData["MatchId"] = new SelectList(_context.Set<Match>(), "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BetDraw([FromRoute]int id, [Bind("Id,BetMoney,GamblerId")]Ticket tickety)
        {
            AppUser curruser = await userManager.GetUserAsync(User);
            DateTime now = DateTime.Now;
            var match = await _context.Match.FirstOrDefaultAsync(m => m.Id == id);
            Ticket ticket = new Ticket
            {
                BetTime = now,
                MatchId = match.Id,
                BetMoney = tickety.BetMoney,
                GamblerId = (int)curruser.GamblerId,
                Tip = 0
            };
            ticket.WinMoney = ticket.BetMoney * match.DrawOdds * (float)0.85;
            _context.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult BetAwayTeam()
        {
            ViewData["GamblerId"] = new SelectList(_context.Gambler, "Id", "FullName");
            ViewData["MatchId"] = new SelectList(_context.Set<Match>(), "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BetAwayTeam([FromRoute]int id, [Bind("Id,BetMoney,GamblerId")]Ticket tickety)
        {
            AppUser curruser = await userManager.GetUserAsync(User);
            DateTime now = DateTime.Now;
            var match = await _context.Match.FirstOrDefaultAsync(m => m.Id == id);
            Ticket ticket = new Ticket
            {
                BetTime = now,
                MatchId = match.Id,
                BetMoney = tickety.BetMoney,
                GamblerId = (int)curruser.GamblerId,
                Tip = 2
            };
            ticket.WinMoney = ticket.BetMoney * match.AwayOdds * (float)0.85;
            _context.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            var matches = from m in _context.Match
                          select m;
            matches = matches.Where(m => !m.HomeGoals.HasValue);
            return View(await matches.ToListAsync());
        }
        [AllowAnonymous]
        public IActionResult Home()
        {
            return View();
        }
    }
}