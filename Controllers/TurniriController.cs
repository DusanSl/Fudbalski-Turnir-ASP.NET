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
    public class TurniriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TurniriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Turnir
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turnir.ToListAsync());
        }

        // GET: Turnir/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnir = await _context.Turnir
                .FirstOrDefaultAsync(m => m.TurnirID == id);
            if (turnir == null)
            {
                return NotFound();
            }

            return View(turnir);
        }

        // GET: Turnir/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turnir/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurnirID,NazivTurnira,Lokacija,DatumPocetka,DatumZavrsetka,TipTurnira")] Turnir turnir)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turnir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turnir);
        }

        // GET: Turnir/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnir = await _context.Turnir.FindAsync(id);
            if (turnir == null)
            {
                return NotFound();
            }
            return View(turnir);
        }

        // POST: Turnir/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurnirID,NazivTurnira,Lokacija,DatumPocetka,DatumZavrsetka,TipTurnira")] Turnir turnir)
        {
            if (id != turnir.TurnirID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turnir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnirExists(turnir.TurnirID))
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
            return View(turnir);
        }

        // GET: Turnir/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnir = await _context.Turnir
                .FirstOrDefaultAsync(m => m.TurnirID == id);
            if (turnir == null)
            {
                return NotFound();
            }

            return View(turnir);
        }

        // POST: Turnir/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turnir = await _context.Turnir.FindAsync(id);
            if (turnir != null)
            {
                _context.Turnir.Remove(turnir);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnirExists(int id)
        {
            return _context.Turnir.Any(e => e.TurnirID == id);
        }
    }
}
