using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProBet.Models;
using ProBet.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ProBet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GamblersController : Controller
    {
        private readonly ProBetContext _context;
        private readonly ProBetContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public GamblersController(ProBetContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Gamblers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gambler.ToListAsync());
        }

        // GET: Gamblers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gambler = await _context.Gambler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gambler == null)
            {
                return NotFound();
            }

            return View(gambler);
        }

        // GET: Gamblers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gamblers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GamblerViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName1 = UploadedFile(model);
                string uniqueFileName2 = UploadedFile2(model);
                Gambler gambler = new Gambler
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = model.FirstName + " " + model.LastName,
                    Nationality = model.Nationality,
                    Earnings = 0,
                    ProfilePicture = uniqueFileName1,
                    CoverPhoto = uniqueFileName2,
                };
                dbContext.Add(gambler);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public string UploadedFile(GamblerViewModel model)
        {
            string uniqueFileName = null;
            if (model.ProfilePictureVM != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfilePictureVM.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePictureVM.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public string UploadedFile2(GamblerViewModel model)
        {
            string uniqueFileName = null;
            if (model.CoverPhotoVM != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.CoverPhotoVM.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverPhotoVM.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: Gamblers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gambler = await _context.Gambler.FindAsync(id);
            if (gambler == null)
            {
                return NotFound();
            }
            return View(gambler);
        }

        // POST: Gamblers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,FullName,Nationality,Earnings,ProfilePicture,CoverPhoto")] Gambler gambler)
        {
            if (id != gambler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gambler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamblerExists(gambler.Id))
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
            return View(gambler);
        }

        // GET: Gamblers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gambler = await _context.Gambler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gambler == null)
            {
                return NotFound();
            }

            return View(gambler);
        }

        // POST: Gamblers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gambler = await _context.Gambler.FindAsync(id);
            _context.Gambler.Remove(gambler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GamblerHome(int? id)
        {
            if (id == null)
            {
                return NotFound();
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
        private bool GamblerExists(int id)
        {
            return _context.Gambler.Any(e => e.Id == id);
        }
        public async Task<IActionResult> TopFive()
        {
            var gamblers = from g in _context.Gambler
                           select g;
            gamblers = gamblers.OrderByDescending(g => g.Earnings).Take(5);
            return View(await gamblers.ToListAsync());
        }
    }
}
