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
                .Include(k => k.Turniri)
                .FirstOrDefaultAsync(m => m.KlubID == id);
            if (klub == null)
            {
                return NotFound();
            }
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(klub);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira");
            return View(new KlubViewModel()); 
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KlubViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var klub = new Klub
                {
                    ImeKluba = viewModel.ImeKluba,
                    GodinaOsnivanja = viewModel.GodinaOsnivanja,
                    RankingTima = viewModel.RankingTima,
                    BrojIgraca = viewModel.BrojIgraca,
                    Stadion = viewModel.Stadion,
                    BrojOsvojenihTitula = viewModel.BrojOsvojenihTitula,
                    Turniri = new List<Turnir>()
                };

                if (viewModel.TurnirID.HasValue && viewModel.TurnirID.Value > 0)
                {
                    var turnir = await _context.Turnir.FindAsync(viewModel.TurnirID.Value);
                    if (turnir != null)
                    {
                        klub.Turniri.Add(turnir);
                    }
                }

                _context.Add(klub);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
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

            var viewModel = new KlubViewModel
            {
                KlubID = klub.KlubID,
                ImeKluba = klub.ImeKluba,
                GodinaOsnivanja = klub.GodinaOsnivanja,
                RankingTima = klub.RankingTima,
                BrojIgraca = klub.BrojIgraca,
                Stadion = klub.Stadion,
                BrojOsvojenihTitula = klub.BrojOsvojenihTitula,
                TurnirID = klub.Turniri?.FirstOrDefault()?.TurnirID
            };

            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", viewModel.TurnirID);

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KlubViewModel viewModel) 
        {
            if (id != viewModel.KlubID)
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

                    existingKlub.ImeKluba = viewModel.ImeKluba;
                    existingKlub.GodinaOsnivanja = viewModel.GodinaOsnivanja;
                    existingKlub.RankingTima = viewModel.RankingTima;
                    existingKlub.BrojIgraca = viewModel.BrojIgraca;
                    existingKlub.Stadion = viewModel.Stadion;
                    existingKlub.BrojOsvojenihTitula = viewModel.BrojOsvojenihTitula;

                    existingKlub.Turniri.Clear();

                    if (viewModel.TurnirID.HasValue && viewModel.TurnirID.Value > 0)
                    {
                        var turnir = await _context.Turnir.FindAsync(viewModel.TurnirID.Value);
                        if (turnir != null)
                        {
                            existingKlub.Turniri.Add(turnir);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlubExists(viewModel.KlubID))
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

            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
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