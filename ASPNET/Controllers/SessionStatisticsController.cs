using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNET.Migrations;
using ASPNET.Models;

namespace ASPNET.Controllers
{
    public class SessionStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SessionStatistics
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SessionStatistics.Include(s => s.Dictionary).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SessionStatistics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionStatistics = await _context.SessionStatistics
                .Include(s => s.Dictionary)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessionStatistics == null)
            {
                return NotFound();
            }

            return View(sessionStatistics);
        }

        // GET: SessionStatistics/Create
        public IActionResult Create()
        {
            ViewData["DictionaryId"] = new SelectList(_context.Dictionaries, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SessionStatistics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SessionDate,GoodAnswers,AllAnswers,Percentage,DictionaryId,UserId")] SessionStatistics sessionStatistics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessionStatistics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DictionaryId"] = new SelectList(_context.Dictionaries, "Id", "Id", sessionStatistics.DictionaryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sessionStatistics.UserId);
            return View(sessionStatistics);
        }

        // GET: SessionStatistics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionStatistics = await _context.SessionStatistics.FindAsync(id);
            if (sessionStatistics == null)
            {
                return NotFound();
            }
            ViewData["DictionaryId"] = new SelectList(_context.Dictionaries, "Id", "Id", sessionStatistics.DictionaryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sessionStatistics.UserId);
            return View(sessionStatistics);
        }

        // POST: SessionStatistics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionDate,GoodAnswers,AllAnswers,Percentage,DictionaryId,UserId")] SessionStatistics sessionStatistics)
        {
            if (id != sessionStatistics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionStatistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionStatisticsExists(sessionStatistics.Id))
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
            ViewData["DictionaryId"] = new SelectList(_context.Dictionaries, "Id", "Id", sessionStatistics.DictionaryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sessionStatistics.UserId);
            return View(sessionStatistics);
        }

        // GET: SessionStatistics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionStatistics = await _context.SessionStatistics
                .Include(s => s.Dictionary)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessionStatistics == null)
            {
                return NotFound();
            }

            return View(sessionStatistics);
        }

        // POST: SessionStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionStatistics = await _context.SessionStatistics.FindAsync(id);
            if (sessionStatistics != null)
            {
                _context.SessionStatistics.Remove(sessionStatistics);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionStatisticsExists(int id)
        {
            return _context.SessionStatistics.Any(e => e.Id == id);
        }
    }
}
