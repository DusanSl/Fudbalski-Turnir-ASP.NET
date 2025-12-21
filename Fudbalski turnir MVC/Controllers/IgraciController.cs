using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fudbalski_turnir.Data;
using Fudbalski_turnir.Models;
using Microsoft.AspNetCore.Authorization;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.Services;
using FudbalskiTurnir.ViewModels; 

namespace Fudbalski_turnir.Controllers
{
    public class IgraciController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IgraciController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Igraci
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var igraci = await _context.Igrac.Include(i => i.Klub).ToListAsync();
            var viewModel = igraci.Select(i => new IgracViewModel
            {
                OsobaID = i.OsobaID,
                Ime = i.Ime,
                Prezime = i.Prezime,
                Pozicija = i.Pozicija,
                BrojDresa = i.BrojDresa,
                DatumRodjenja = i.DatumRodjenja,
                Nacionalnost = i.Nacionalnost,
                KlubID = i.KlubID,
                ImeKluba = i.Klub?.ImeKluba
            }).ToList();

            return View(viewModel); 
        }

        // GET: Igraci/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var igrac = await _context.Igrac
                .Include(i => i.Klub)
                .FirstOrDefaultAsync(m => m.OsobaID == id);
            ViewBag.IsAdmin = User.IsInRole("Admin");
            if (igrac == null) return NotFound();

            var viewModel = new IgracViewModel
            {
                OsobaID = igrac.OsobaID,
                Ime = igrac.Ime,
                Prezime = igrac.Prezime,
                Pozicija = igrac.Pozicija,
                BrojDresa = igrac.BrojDresa,
                DatumRodjenja = igrac.DatumRodjenja,
                Nacionalnost = igrac.Nacionalnost,
                KlubID = igrac.KlubID,
                ImeKluba = igrac.Klub?.ImeKluba 
            };

            return View(viewModel);
        }
        // GET: Igraci/Create
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba");
            return View(new IgracViewModel()); 
        }
        // POST: Igraci/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(IgracViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.KlubID <= 0)
                {
                    ModelState.AddModelError("KlubID", "Morate izabrati klub.");
                    ViewBag.Klubovi = new SelectList(_context.Klub, "KlubID", "ImeKluba");
                    return View(viewModel);
                }
                var igrac = new Igrac
                {
                    KlubID = viewModel.KlubID,
                    Ime = viewModel.Ime,
                    Prezime = viewModel.Prezime,
                    DatumRodjenja = viewModel.DatumRodjenja,
                    Nacionalnost = viewModel.Nacionalnost,
                    Pozicija = viewModel.Pozicija,
                    BrojDresa = viewModel.BrojDresa,
                };

                _context.Add(igrac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba", viewModel.KlubID);
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

            var igrac = await _context.Igrac
                .FirstOrDefaultAsync(i => i.OsobaID == id);

            if (igrac == null)
            {
                return NotFound();
            }

            var viewModel = new IgracViewModel
            {
                OsobaID = igrac.OsobaID,
                KlubID = igrac.KlubID,
                Ime = igrac.Ime,
                Prezime = igrac.Prezime,
                DatumRodjenja = igrac.DatumRodjenja,
                Nacionalnost = igrac.Nacionalnost,
                Pozicija = igrac.Pozicija,
                BrojDresa = igrac.BrojDresa
            };

            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba", igrac.KlubID);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, IgracViewModel viewModel)
        {
            if (id != viewModel.OsobaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var igrac = await _context.Igrac.FindAsync(id);
                    if (igrac == null)
                    {
                        return NotFound();
                    }

                    igrac.KlubID = viewModel.KlubID;
                    igrac.Ime = viewModel.Ime;
                    igrac.Prezime = viewModel.Prezime;
                    igrac.DatumRodjenja = viewModel.DatumRodjenja;
                    igrac.Nacionalnost = viewModel.Nacionalnost;
                    igrac.Pozicija = viewModel.Pozicija;
                    igrac.BrojDresa = viewModel.BrojDresa;

                    _context.Update(igrac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgracExists(viewModel.OsobaID))
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
            ViewBag.Klub = new SelectList(_context.Klub, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Igraci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var igrac = await _context.Igrac
                .Include(i => i.Klub)
                .FirstOrDefaultAsync(m => m.OsobaID == id);

            if (igrac == null) return NotFound();

            var viewModel = new IgracViewModel
            {
                OsobaID = igrac.OsobaID,
                Ime = igrac.Ime,
                Prezime = igrac.Prezime,
                Pozicija = igrac.Pozicija,
                BrojDresa = igrac.BrojDresa,
                DatumRodjenja = igrac.DatumRodjenja,
                Nacionalnost = igrac.Nacionalnost,
                KlubID = igrac.KlubID,
                ImeKluba = igrac.Klub?.ImeKluba
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        // POST: Igraci/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var igrac = await _context.Igrac.FindAsync(id);
            if (igrac != null)
            {
                _context.Igrac.Remove(igrac);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool IgracExists(int id)
        {
            return _context.Igrac.Any(e => e.OsobaID == id);
        }
    }
}