using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.Services;
using FudbalskiTurnir.ViewModels;

namespace Fudbalski_turnir.Controllers
{
    [Authorize]
    public class IgraciController : Controller
    {
        private readonly IIgracService _igraciService;

        public IgraciController(IIgracService igraciService)
        {
            _igraciService = igraciService;
        }

        // GET: Igraci
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Pozivamo servis umesto baze
            var igraci = await _igraciService.GetAllIgraceAsync();

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
        public async Task<IActionResult> Details(int id)
        {
            var igrac = await _igraciService.GetIgracByIdAsync(id);
            if (igrac == null) return NotFound();

            ViewBag.IsAdmin = User.IsInRole("Admin");

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
        public async Task<IActionResult> Create()
        {
            // Pretpostavljamo da si dodao GetAllKluboviAsync u IIgraciService
            var klubovi = await _igraciService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba");
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
                var igrac = new Igrac
                {
                    KlubID = viewModel.KlubID,
                    Ime = viewModel.Ime,
                    Prezime = viewModel.Prezime,
                    DatumRodjenja = viewModel.DatumRodjenja,
                    Nacionalnost = viewModel.Nacionalnost,
                    Pozicija = viewModel.Pozicija,
                    BrojDresa = viewModel.BrojDresa
                };

                await _igraciService.CreateIgracAsync(igrac);
                return RedirectToAction(nameof(Index));
            }

            var klubovi = await _igraciService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        // GET: Igraci/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var igrac = await _igraciService.GetIgracByIdAsync(id);
            if (igrac == null) return NotFound();

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

            var klubovi = await _igraciService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba", igrac.KlubID);
            return View(viewModel);
        }

        // POST: Igraci/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, IgracViewModel viewModel)
        {
            if (id != viewModel.OsobaID) return NotFound();

            if (ModelState.IsValid)
            {
                var igrac = new Igrac
                {
                    OsobaID = viewModel.OsobaID,
                    KlubID = viewModel.KlubID,
                    Ime = viewModel.Ime,
                    Prezime = viewModel.Prezime,
                    DatumRodjenja = viewModel.DatumRodjenja,
                    Nacionalnost = viewModel.Nacionalnost,
                    Pozicija = viewModel.Pozicija,
                    BrojDresa = viewModel.BrojDresa
                };

                await _igraciService.UpdateIgracAsync(igrac);
                return RedirectToAction(nameof(Index));
            }

            var klubovi = await _igraciService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        // GET: Igraci/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var igrac = await _igraciService.GetIgracByIdAsync(id);
            if (igrac == null) return NotFound();

            var viewModel = new IgracViewModel
            {
                OsobaID = igrac.OsobaID,
                Ime = igrac.Ime,
                Prezime = igrac.Prezime,
                ImeKluba = igrac.Klub?.ImeKluba,
                Pozicija = igrac.Pozicija,
                Nacionalnost = igrac.Nacionalnost,
            };

            return View(viewModel);
        }

        // POST: Igraci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _igraciService.DeleteIgracAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}