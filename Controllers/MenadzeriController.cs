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
            return View(await _context.Menadzer.ToListAsync());
        }

        // GET: Menadzeri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menadzer = await _context.Menadzer
                .FirstOrDefaultAsync(m => m.OsobaID == id);
            if (menadzer == null)
            {
                return NotFound();
            }

            return View(menadzer);
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
        public async Task<IActionResult> Create([Bind("MenadzerID,KlubID,GodineIskustva,OsobaID,Ime,Prezime,DatumRodjenja,Nacionalnost,UKlubuOd,UKlubuDo")] Menadzer menadzer, int KlubID)
        {
            if (ModelState.IsValid)
            {
                menadzer.KlubID = KlubID;

                _context.Add(menadzer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba", KlubID);
            return View(menadzer);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Menadzeri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menadzer = await _context.Menadzer.FindAsync(id);
            if (menadzer == null)
            {
                return NotFound();
            }
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba");
            return View(menadzer);
        }

        [Authorize(Roles = "Admin")]
        // POST: Menadzeri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenadzerID,KlubID,GodineIskustva,OsobaID,Ime,Prezime,DatumRodjenja,Nacionalnost,UKlubuOd,UKlubuDo")] Menadzer menadzer)
        {
            if (id != menadzer.OsobaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menadzer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenadzerExists(menadzer.OsobaID))
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
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba");
            return View(menadzer);
        }

        [Authorize(Roles = "Admin")]
        // GET: Menadzeri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menadzer = await _context.Menadzer
                .FirstOrDefaultAsync(m => m.OsobaID == id);
            if (menadzer == null)
            {
                return NotFound();
            }

            return View(menadzer);
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
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenadzerExists(int id)
        {
            return _context.Menadzer.Any(e => e.OsobaID == id);
        }
    }
}
