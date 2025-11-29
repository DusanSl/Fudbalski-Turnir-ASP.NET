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
    public class SponzoriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponzoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sponzor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sponzor.ToListAsync());
        }

        // GET: Sponzor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponzor = await _context.Sponzor
                .FirstOrDefaultAsync(m => m.SponzorID == id);
            if (sponzor == null)
            {
                return NotFound();
            }

            return View(sponzor);
        }

        // GET: Sponzor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sponzor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SponzorID,ImeSponzora,KontaktSponzora,VrednostSponzora")] Sponzor sponzor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponzor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sponzor);
        }

        // GET: Sponzor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponzor = await _context.Sponzor.FindAsync(id);
            if (sponzor == null)
            {
                return NotFound();
            }
            return View(sponzor);
        }

        // POST: Sponzor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SponzorID,ImeSponzora,KontaktSponzora,VrednostSponzora")] Sponzor sponzor)
        {
            if (id != sponzor.SponzorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponzor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponzorExists(sponzor.SponzorID))
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
            return View(sponzor);
        }

        // GET: Sponzor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponzor = await _context.Sponzor
                .FirstOrDefaultAsync(m => m.SponzorID == id);
            if (sponzor == null)
            {
                return NotFound();
            }

            return View(sponzor);
        }

        // POST: Sponzor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponzor = await _context.Sponzor.FindAsync(id);
            if (sponzor != null)
            {
                _context.Sponzor.Remove(sponzor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponzorExists(int id)
        {
            return _context.Sponzor.Any(e => e.SponzorID == id);
        }
    }
}
