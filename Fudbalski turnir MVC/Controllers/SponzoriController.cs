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
    public class SponzoriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponzoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sponzori
        public async Task<IActionResult> Index()
        {
            var sponzori = await _context.Sponzor
                .Include(s => s.Turniri)
                .ToListAsync();

            var viewModel = sponzori.Select(s => new SponzorViewModel
            {
                SponzorID = s.SponzorID,
                ImeSponzora = s.ImeSponzora,
                KontaktSponzora = s.KontaktSponzora,
                VrednostSponzora = s.VrednostSponzora,
                Turniri = s.Turniri?.ToList()
            }).ToList();

            return View(viewModel);
        }

        // GET: Sponzori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sponzor = await _context.Sponzor
                .Include(s => s.Turniri)
                .FirstOrDefaultAsync(s => s.SponzorID == id);

            if (sponzor == null) return NotFound();

            ViewBag.IsAdmin = User.IsInRole("Admin");
            var viewModel = new SponzorViewModel
            {
                SponzorID = sponzor.SponzorID,
                ImeSponzora = sponzor.ImeSponzora,
                KontaktSponzora = sponzor.KontaktSponzora,
                VrednostSponzora = sponzor.VrednostSponzora,
                Turniri = sponzor.Turniri 
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Sponzori/Create
        public IActionResult Create()
        {
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira");
            return View(new SponzorViewModel());
        }

        [Authorize(Roles = "Admin")]
        // POST: Sponzori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SponzorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var sponzor = new Sponzor
                {
                    ImeSponzora = viewModel.ImeSponzora,
                    KontaktSponzora = viewModel.KontaktSponzora,
                    VrednostSponzora = viewModel.VrednostSponzora,
                    Turniri = new List<Turnir>()
                };

                if (viewModel.TurnirID.HasValue && viewModel.TurnirID.Value > 0)
                {
                    var turnir = await _context.Turnir.FindAsync(viewModel.TurnirID.Value);
                    if (turnir != null) sponzor.Turniri.Add(turnir);
                }

                _context.Add(sponzor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Sponzori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sponzor = await _context.Sponzor
                .Include(s => s.Turniri)
                .FirstOrDefaultAsync(s => s.SponzorID == id);

            if (sponzor == null) return NotFound();

            var viewModel = new SponzorViewModel
            {
                SponzorID = sponzor.SponzorID,
                ImeSponzora = sponzor.ImeSponzora,
                KontaktSponzora = sponzor.KontaktSponzora,
                VrednostSponzora = sponzor.VrednostSponzora,
                TurnirID = sponzor.Turniri?.FirstOrDefault()?.TurnirID
            };

            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Sponzori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SponzorViewModel viewModel)
        {
            if (id != viewModel.SponzorID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingSponzor = await _context.Sponzor
                        .Include(s => s.Turniri)
                        .FirstOrDefaultAsync(s => s.SponzorID == id);

                    if (existingSponzor == null) return NotFound();

                    existingSponzor.ImeSponzora = viewModel.ImeSponzora;
                    existingSponzor.KontaktSponzora = viewModel.KontaktSponzora;
                    existingSponzor.VrednostSponzora = viewModel.VrednostSponzora;

                    existingSponzor.Turniri?.Clear();
                    if (viewModel.TurnirID.HasValue && viewModel.TurnirID.Value > 0)
                    {
                        var turnir = await _context.Turnir.FindAsync(viewModel.TurnirID.Value);
                        if (turnir != null) existingSponzor.Turniri?.Add(turnir);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponzorExists(viewModel.SponzorID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Turniri = new SelectList(_context.Turnir, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Sponzori/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sponzor = await _context.Sponzor
                .Include(s => s.Turniri) 
                .FirstOrDefaultAsync(m => m.SponzorID == id);

            if (sponzor == null) return NotFound();

            var viewModel = new SponzorViewModel
            {
                SponzorID = sponzor.SponzorID,
                ImeSponzora = sponzor.ImeSponzora,
                KontaktSponzora = sponzor.KontaktSponzora,
                VrednostSponzora = sponzor.VrednostSponzora,

                TurnirID = sponzor.Turniri?.FirstOrDefault()?.TurnirID,

                Turniri = sponzor.Turniri?.ToList()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Sponzori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponzor = await _context.Sponzor.FindAsync(id);
            if (sponzor != null)
            {
                _context.Sponzor.Remove(sponzor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SponzorExists(int id)
        {
            return _context.Sponzor.Any(e => e.SponzorID == id);
        }
    }
}
