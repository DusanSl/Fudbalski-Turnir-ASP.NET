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
using FudbalskiTurnir.ViewModels;

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
            var utakmice = await _context.Utakmica.ToListAsync();

            var viewModel = utakmice.Select(u => new UtakmicaViewModel
            {
                UtakmicaID = u.UtakmicaID,
                TurnirID = u.TurnirID,
                Datum = u.Datum,
                Mesto = u.Mesto,
                PrviKlubNaziv = u.PrviKlubNaziv,
                DrugiKlubNaziv = u.DrugiKlubNaziv,
                Kolo = u.Kolo,
                PrviKlubGolovi = u.PrviKlubGolovi,
                DrugiKlubGolovi = u.DrugiKlubGolovi,
                Produzeci = u.Produzeci,
                Penali = u.Penali,
                PrviKlubPenali = u.PrviKlubPenali,
                DrugiKlubPenali = u.DrugiKlubPenali
            }).ToList();

            return View(viewModel);
        }

        // GET: Utakmice/Details/5
        // GET: Utakmice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var u = await _context.Utakmica
                .Include(u => u.Turnir)  
                .FirstOrDefaultAsync(m => m.UtakmicaID == id);

            if (u == null) return NotFound();

            ViewBag.IsAdmin = User.IsInRole("Admin");
            var viewModel = new UtakmicaViewModel
            {
                UtakmicaID = u.UtakmicaID,
                TurnirID = u.TurnirID,
                Datum = u.Datum,
                Mesto = u.Mesto,
                PrviKlubNaziv = u.PrviKlubNaziv,
                DrugiKlubNaziv = u.DrugiKlubNaziv,
                Kolo = u.Kolo,
                PrviKlubGolovi = u.PrviKlubGolovi,
                DrugiKlubGolovi = u.DrugiKlubGolovi,
                Produzeci = u.Produzeci,
                Penali = u.Penali,
                PrviKlubPenali = u.PrviKlubPenali,
                DrugiKlubPenali = u.DrugiKlubPenali,
                NazivTurnira = u.Turnir?.NazivTurnira 
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Utakmice/Create
        public IActionResult Create()
        {
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira");
            return View(new UtakmicaViewModel());
        }

        [Authorize(Roles = "Admin")]
        // POST: Utakmice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UtakmicaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var utakmica = new Utakmica
                {
                    TurnirID = viewModel.TurnirID,
                    Datum = viewModel.Datum,
                    Mesto = viewModel.Mesto,
                    PrviKlubNaziv = viewModel.PrviKlubNaziv,
                    DrugiKlubNaziv = viewModel.DrugiKlubNaziv,
                    Kolo = viewModel.Kolo,
                    PrviKlubGolovi = viewModel.PrviKlubGolovi,
                    DrugiKlubGolovi = viewModel.DrugiKlubGolovi,
                    Produzeci = viewModel.Produzeci,
                    Penali = viewModel.Penali,
                    PrviKlubPenali = viewModel.PrviKlubPenali,
                    DrugiKlubPenali = viewModel.DrugiKlubPenali
                };
                _context.Add(utakmica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira");
            return View(viewModel);
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

            var viewModel = new UtakmicaViewModel
            {
                UtakmicaID = utakmica.UtakmicaID,
                TurnirID = utakmica.TurnirID,
                Datum = utakmica.Datum,
                Mesto = utakmica.Mesto,
                PrviKlubNaziv = utakmica.PrviKlubNaziv,
                DrugiKlubNaziv = utakmica.DrugiKlubNaziv,
                Kolo = utakmica.Kolo,
                PrviKlubGolovi = utakmica.PrviKlubGolovi,
                DrugiKlubGolovi = utakmica.DrugiKlubGolovi,
                Produzeci = utakmica.Produzeci,
                Penali = utakmica.Penali,
                PrviKlubPenali = utakmica.PrviKlubPenali,
                DrugiKlubPenali = utakmica.DrugiKlubPenali
            };

            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", utakmica.TurnirID);

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Utakmice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UtakmicaViewModel viewModel)
        {
            if (id != viewModel.UtakmicaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var utakmica = await _context.Utakmica.FindAsync(id);
                    if (utakmica == null)
                    {
                        return NotFound();
                    }

                    utakmica.TurnirID = viewModel.TurnirID;
                    utakmica.Datum = viewModel.Datum;
                    utakmica.Mesto = viewModel.Mesto;
                    utakmica.PrviKlubNaziv = viewModel.PrviKlubNaziv;
                    utakmica.DrugiKlubNaziv = viewModel.DrugiKlubNaziv;
                    utakmica.Kolo = viewModel.Kolo;
                    utakmica.PrviKlubGolovi = viewModel.PrviKlubGolovi;
                    utakmica.DrugiKlubGolovi = viewModel.DrugiKlubGolovi;
                    utakmica.Produzeci = viewModel.Produzeci;
                    utakmica.Penali = viewModel.Penali;
                    utakmica.PrviKlubPenali = viewModel.PrviKlubPenali;
                    utakmica.DrugiKlubPenali = viewModel.DrugiKlubPenali;

                    _context.Update(utakmica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtakmicaExists(viewModel.UtakmicaID))
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

            ViewBag.Turnir = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Utakmice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var u = await _context.Utakmica
                .Include(m => m.Turnir)
                .FirstOrDefaultAsync(m => m.UtakmicaID == id);

            if (u == null) return NotFound();

            var viewModel = new UtakmicaViewModel
            {
                UtakmicaID = u.UtakmicaID,
                Datum = u.Datum,
                Mesto = u.Mesto,
                PrviKlubNaziv = u.PrviKlubNaziv,
                DrugiKlubNaziv = u.DrugiKlubNaziv,
                Kolo = u.Kolo,
                PrviKlubGolovi = u.PrviKlubGolovi,
                DrugiKlubGolovi = u.DrugiKlubGolovi,
                Produzeci = u.Produzeci,
                Penali = u.Penali,
                PrviKlubPenali = u.PrviKlubPenali,
                DrugiKlubPenali = u.DrugiKlubPenali,
                NazivTurnira = u.Turnir?.NazivTurnira 
            };

            return View(viewModel);
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
                await _context.SaveChangesAsync();
            }
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
