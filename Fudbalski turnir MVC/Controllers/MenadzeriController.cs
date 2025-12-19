using Fudbalski_turnir.Data;
using Fudbalski_turnir.Models;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.Services;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fudbalski_turnir.Controllers
{
    public class MenadzeriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenadzeriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Menadzeri
        public async Task<IActionResult> Index()
        {
            var menadzeri = await _context.Menadzer.Include(m => m.Klub).ToListAsync();
            var viewModel = menadzeri.Select(m => new MenadzerViewModel
            {
                OsobaID = m.OsobaID,
                Ime = m.Ime,
                Prezime = m.Prezime,
                GodineIskustva = m.GodineIskustva,
                DatumRodjenja = m.DatumRodjenja,
                Nacionalnost = m.Nacionalnost,
                KlubID = m.KlubID,
                ImeKluba = m.Klub?.ImeKluba
            }).ToList();

            return View(viewModel);
        }

        // GET: Menadzeri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var menadzer = await _context.Menadzer
                .Include(m => m.Klub)
                .FirstOrDefaultAsync(m => m.OsobaID == id); 
            ViewBag.IsAdmin = User.IsInRole("Admin");

            if (menadzer == null) return NotFound();

            var viewModel = new MenadzerViewModel
            {
                OsobaID = menadzer.OsobaID,
                KlubID = menadzer.KlubID,
                Ime = menadzer.Ime,
                Prezime = menadzer.Prezime,
                DatumRodjenja = menadzer.DatumRodjenja,
                Nacionalnost = menadzer.Nacionalnost,
                GodineIskustva = menadzer.GodineIskustva,
                ImeKluba = menadzer.Klub?.ImeKluba
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Menadzeri/Create
        public IActionResult Create()
        {
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Menadzeri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenadzerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var menadzer = new Menadzer
                {
                    Ime = viewModel.Ime,
                    Prezime = viewModel.Prezime,
                    DatumRodjenja = viewModel.DatumRodjenja,
                    Nacionalnost = viewModel.Nacionalnost,
                    GodineIskustva = viewModel.GodineIskustva,
                    KlubID = viewModel.KlubID
                };

                _context.Add(menadzer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Menadzeri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var menadzer = await _context.Menadzer.FindAsync(id);
            if (menadzer == null) return NotFound();

            var viewModel = new MenadzerViewModel
            {
                OsobaID = menadzer.OsobaID,
                Ime = menadzer.Ime,
                Prezime = menadzer.Prezime,
                DatumRodjenja = menadzer.DatumRodjenja,
                Nacionalnost = menadzer.Nacionalnost,
                GodineIskustva = menadzer.GodineIskustva,
                KlubID = menadzer.KlubID
            };

            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Menadzeri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenadzerViewModel viewModel)
        {
            if (id != viewModel.OsobaID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMenadzer = await _context.Menadzer.FindAsync(id);
                    if (existingMenadzer == null) return NotFound();

                    existingMenadzer.Ime = viewModel.Ime;
                    existingMenadzer.Prezime = viewModel.Prezime;
                    existingMenadzer.DatumRodjenja = viewModel.DatumRodjenja;
                    existingMenadzer.Nacionalnost = viewModel.Nacionalnost;
                    existingMenadzer.GodineIskustva = viewModel.GodineIskustva;
                    existingMenadzer.KlubID = viewModel.KlubID;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenadzerExists(viewModel.OsobaID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Menadzeri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var menadzer = await _context.Menadzer
                .Include(m => m.Klub)
                .FirstOrDefaultAsync(m => m.OsobaID == id);

            if (menadzer == null) return NotFound();

            var viewModel = new MenadzerViewModel
            {
                OsobaID = menadzer.OsobaID,
                Ime = menadzer.Ime,
                Prezime = menadzer.Prezime,
                ImeKluba = menadzer.Klub?.ImeKluba,
                GodineIskustva = menadzer.GodineIskustva,
                Nacionalnost = menadzer.Nacionalnost,
                KlubID = menadzer.KlubID,
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Menadzeri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menadzer = await _context.Menadzer.FindAsync(id);
            if (menadzer != null)
            {
                _context.Menadzer.Remove(menadzer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MenadzerExists(int id)
        {
            return _context.Menadzer.Any(e => e.OsobaID == id);
        }
    }
}
