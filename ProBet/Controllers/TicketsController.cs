using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProBet.Models;

namespace ProBet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TicketsController : Controller
    {
        private readonly ProBetContext _context;

        public TicketsController(ProBetContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var proBetContext = _context.Ticket.Include(t => t.Gambler).Include(t => t.Match);
            return View(await proBetContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Gambler)
                .Include(t => t.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["GamblerId"] = new SelectList(_context.Gambler, "Id", "FullName");
            ViewData["MatchId"] = new SelectList(_context.Set<Match>(), "Id", "Id");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BetMoney,Tip,GamblerId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                ticket.BetTime = now;
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GamblerId"] = new SelectList(_context.Gambler, "Id", "FullName", ticket.GamblerId);
            ViewData["MatchId"] = new SelectList(_context.Set<Match>(), "Id", "Id", ticket.MatchId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["GamblerId"] = new SelectList(_context.Gambler, "Id", "FullName", ticket.GamblerId);
            ViewData["MatchId"] = new SelectList(_context.Set<Match>(), "Id", "Id", ticket.MatchId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BetTime,BetMoney,WinMoney,Tip,MatchId,GamblerId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            ViewData["GamblerId"] = new SelectList(_context.Gambler, "Id", "Id", ticket.GamblerId);
            ViewData["MatchId"] = new SelectList(_context.Set<Match>(), "Id", "Id", ticket.MatchId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Gambler)
                .Include(t => t.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
        public IActionResult BetHomeTeam()
        {
            ViewData["GamblerId"] = new SelectList(_context.Gambler, "Id", "FullName");
            ViewData["MatchId"] = new SelectList(_context.Set<Match>(), "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BetHomeTeam([FromRoute]int id, [Bind("Id,BetMoney,GamblerId")]Ticket tickety)
        {
            DateTime now = DateTime.Now;
            var match = await _context.Match.FirstOrDefaultAsync(m=>m.Id == id);
            Ticket ticket = new Ticket
            {
                BetTime = now,
                MatchId = match.Id,
                BetMoney=tickety.BetMoney,
                GamblerId=tickety.GamblerId,
                Tip=1
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
            DateTime now = DateTime.Now;
            var match = await _context.Match.FirstOrDefaultAsync(m => m.Id == id);
            Ticket ticket = new Ticket
            {
                BetTime = now,
                MatchId = match.Id,
                BetMoney = tickety.BetMoney,
                GamblerId = tickety.GamblerId,
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
            DateTime now = DateTime.Now;
            var match = await _context.Match.FirstOrDefaultAsync(m => m.Id == id);
            Ticket ticket = new Ticket
            {
                BetTime = now,
                MatchId = match.Id,
                BetMoney = tickety.BetMoney,
                GamblerId = tickety.GamblerId,
                Tip = 2
            };
            ticket.WinMoney = ticket.BetMoney * match.AwayOdds * (float)0.85;
            _context.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
    }

