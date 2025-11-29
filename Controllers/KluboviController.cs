using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fudbalski_turnir.Data;
using Fudbalski_turnir.Models;

namespace Fudbalski_turnir.Controllers
{
    public class KluboviController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KluboviController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Klub
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klub.ToListAsync());
        }

        // GET: Klub/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klub = await _context.Klub
                .FirstOrDefaultAsync(m => m.KlubID == id);
            if (klub == null)
            {
                return NotFound();
            }

            return View(klub);
        }

        // GET: Klub/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klub/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlubID,ImeKluba,GodinaOsnivanja,RankingTima,BrojIgraca,Stadion,BrojOsvojenihTitula")] Klub klub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klub);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klub);
        }

        // GET: Klub/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klub = await _context.Klub.FindAsync(id);
            if (klub == null)
            {
                return NotFound();
            }
            return View(klub);
        }

        // POST: Klub/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KlubID,ImeKluba,GodinaOsnivanja,RankingTima,BrojIgraca,Stadion,BrojOsvojenihTitula")] Klub klub)
        {
            if (id != klub.KlubID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlubExists(klub.KlubID))
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
            return View(klub);
        }

        // GET: Klub/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klub = await _context.Klub
                .FirstOrDefaultAsync(m => m.KlubID == id);
            if (klub == null)
            {
                return NotFound();
            }

            return View(klub);
        }

        // POST: Klub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klub = await _context.Klub.FindAsync(id);
            if (klub != null)
            {
                _context.Klub.Remove(klub);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlubExists(int id)
        {
            return _context.Klub.Any(e => e.KlubID == id);
        }
    }
}
