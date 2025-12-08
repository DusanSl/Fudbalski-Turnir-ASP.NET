using Fudbalski_turnir.Data;
using Fudbalski_turnir.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.Services;

namespace Fudbalski_turnir.Controllers
{
    public class UtakmiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtakmiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utakmice
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utakmica.ToListAsync());
        }

        // GET: Utakmice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utakmica = await _context.Utakmica
                .FirstOrDefaultAsync(m => m.UtakmicaID == id);
            if (utakmica == null)
            {
                return NotFound();
            }

            return View(utakmica);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Utakmice/Create
        public IActionResult Create()
        {
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Utakmice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtakmicaID,TurnirID,Datum,Mesto,PrviKlubNaziv,DrugiKlubNaziv,Kolo,PrviKlubGolovi,DrugiKlubGolovi,Produzeci,Penali,PrviKlubPenali,DrugiKlubPenali,TipUcesca")] Utakmica utakmica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utakmica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira");
            return View(utakmica);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Utakmice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utakmica = await _context.Utakmica
            .Include(u => u.Turnir)
            .FirstOrDefaultAsync(u => u.UtakmicaID == id);

            if (utakmica == null)
            {
                return NotFound();
            }

            // Populate dropdown with all tournaments
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", utakmica.TurnirID);

            return View(utakmica);
        }

        [Authorize(Roles = "Admin")]
        // POST: Utakmice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtakmicaID,TurnirID,Datum,Mesto,PrviKlubNaziv,DrugiKlubNaziv,Kolo,PrviKlubGolovi,DrugiKlubGolovi,Produzeci,Penali,PrviKlubPenali,DrugiKlubPenali,TipUcesca")] Utakmica utakmica)
        {
            if (id != utakmica.UtakmicaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utakmica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtakmicaExists(utakmica.UtakmicaID))
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

            ViewBag.Turnir = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", utakmica.TurnirID);
            return View(utakmica);
        }

        [Authorize(Roles = "Admin")]
        // GET: Utakmice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utakmica = await _context.Utakmica
                .FirstOrDefaultAsync(m => m.UtakmicaID == id);
            if (utakmica == null)
            {
                return NotFound();
            }

            return View(utakmica);
        }

        [Authorize(Roles = "Admin")]
        // POST: Utakmice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utakmica = await _context.Utakmica.FindAsync(id);
            if (utakmica != null)
            {
                _context.Utakmica.Remove(utakmica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtakmicaExists(int id)
        {
            return _context.Utakmica.Any(e => e.UtakmicaID == id);
        }
        [HttpGet]
        public async Task<IActionResult> GetKluboviByTurnir(int turnirId)
        {
            var klubovi = await _context.Klub
                .Where(k => k.Turniri.Any(t => t.TurnirID == turnirId))
                .Select(k => new { k.KlubID, k.ImeKluba })
                .ToListAsync();
            return Json(klubovi);
        }
    }
}
