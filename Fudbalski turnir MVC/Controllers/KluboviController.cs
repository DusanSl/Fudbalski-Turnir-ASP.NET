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
    public class KluboviController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KluboviController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Klubovi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klub.ToListAsync());
        }

        // GET: Klubovi/Details/5
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Klubovi/Create
        public IActionResult Create()
        {
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira");
            return View();
        }

        // POST: Klubovi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlubID,ImeKluba,GodinaOsnivanja,RankingTima,BrojIgraca,Stadion,BrojOsvojenihTitula")] Klub klub, int TurnirID)
        {
            if (ModelState.IsValid)
            {
                klub.Turniri = new List<Turnir>();

                if (TurnirID > 0)
                {
                    var turnir = await _context.Turnir.FindAsync(TurnirID);
                    if (turnir != null)
                    {
                        klub.Turniri.Add(turnir);
                    }
                }

                _context.Add(klub);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira");
            return View(klub);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Klubovi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klub = await _context.Klub
            .Include(k => k.Turniri) 
            .FirstOrDefaultAsync(k => k.KlubID == id);

            if (klub == null)
            {
                return NotFound();
            }

            var currentTurnirID = klub.Turniri?.FirstOrDefault()?.TurnirID;

            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", currentTurnirID);

            return View(klub);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KlubID,ImeKluba,GodinaOsnivanja,RankingTima,BrojIgraca,Stadion,BrojOsvojenihTitula")] Klub klub, int TurnirID)
        {
            if (id != klub.KlubID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingKlub = await _context.Klub
                        .Include(k => k.Turniri)
                        .FirstOrDefaultAsync(k => k.KlubID == id);

                    if (existingKlub == null)
                    {
                        return NotFound();
                    }

                    existingKlub.ImeKluba = klub.ImeKluba;
                    existingKlub.GodinaOsnivanja = klub.GodinaOsnivanja;
                    existingKlub.RankingTima = klub.RankingTima;
                    existingKlub.BrojIgraca = klub.BrojIgraca;
                    existingKlub.Stadion = klub.Stadion;
                    existingKlub.BrojOsvojenihTitula = klub.BrojOsvojenihTitula;

                    existingKlub.Turniri.Clear(); 

                    if (TurnirID > 0)
                    {
                        var turnir = await _context.Turnir.FindAsync(TurnirID);
                        if (turnir != null)
                        {
                            existingKlub.Turniri.Add(turnir);
                        }
                    }

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

            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", TurnirID);
            return View(klub);
        }

        [Authorize(Roles = "Admin")]
        // GET: Klubovi/Delete/5
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

        [Authorize(Roles = "Admin")]
        // POST: Klubovi/Delete/5
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
