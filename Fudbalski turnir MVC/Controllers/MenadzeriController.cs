using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Fudbalski_turnir.Controllers
{
    public class MenadzeriController : Controller
    {
        private readonly IMenadzerService _menadzerService;

        public MenadzeriController(IMenadzerService menadzerService)
        {
            _menadzerService = menadzerService;
        }

        // GET: Menadzeri
        public async Task<IActionResult> Index()
        {
            var menadzeri = await _menadzerService.GetAllMenadzerAsync();

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

            var menadzer = await _menadzerService.GetMenadzerByIdAsync(id.Value);
            if (menadzer == null) return NotFound();

            ViewBag.IsAdmin = User.IsInRole("Admin");

            var viewModel = new MenadzerViewModel
            {
                OsobaID = menadzer.OsobaID,
                Ime = menadzer.Ime,
                Prezime = menadzer.Prezime,
                DatumRodjenja = menadzer.DatumRodjenja,
                Nacionalnost = menadzer.Nacionalnost,
                GodineIskustva = menadzer.GodineIskustva,
                KlubID = menadzer.KlubID,
                ImeKluba = menadzer.Klub?.ImeKluba
            };

            return View(viewModel);
        }

        // GET: Menadzeri/Create
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var klubovi = await _menadzerService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba");
            return View(new MenadzerViewModel());
        }

        // POST: Menadzeri/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenadzerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.KlubID <= 0)
                {
                    ModelState.AddModelError("KlubID", "Morate izabrati klub.");
                    var kluboviList = await _menadzerService.GetAllKluboviAsync();
                    ViewBag.Klub = new SelectList(kluboviList, "KlubID", "ImeKluba");
                    return View(viewModel);
                }

                var menadzer = new Menadzer
                {
                    Ime = viewModel.Ime,
                    Prezime = viewModel.Prezime,
                    DatumRodjenja = viewModel.DatumRodjenja,
                    Nacionalnost = viewModel.Nacionalnost,
                    GodineIskustva = viewModel.GodineIskustva,
                    KlubID = viewModel.KlubID
                };

                await _menadzerService.CreateMenadzerAsync(menadzer);
                return RedirectToAction(nameof(Index));
            }

            var kluboviBack = await _menadzerService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(kluboviBack, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        // GET: Menadzeri/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var menadzer = await _menadzerService.GetMenadzerByIdAsync(id.Value);
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

            var klubovi = await _menadzerService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        // POST: Menadzeri/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenadzerViewModel viewModel)
        {
            if (id != viewModel.OsobaID) return NotFound();

            if (ModelState.IsValid)
            {
                var menadzer = new Menadzer
                {
                    OsobaID = viewModel.OsobaID,
                    Ime = viewModel.Ime,
                    Prezime = viewModel.Prezime,
                    DatumRodjenja = viewModel.DatumRodjenja,
                    Nacionalnost = viewModel.Nacionalnost,
                    GodineIskustva = viewModel.GodineIskustva,
                    KlubID = viewModel.KlubID
                };

                await _menadzerService.UpdateMenadzerAsync(menadzer);
                return RedirectToAction(nameof(Index));
            }

            var klubovi = await _menadzerService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        // GET: Menadzeri/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var menadzer = await _menadzerService.GetMenadzerByIdAsync(id.Value);
            if (menadzer == null) return NotFound();

            var viewModel = new MenadzerViewModel
            {
                OsobaID = menadzer.OsobaID,
                Ime = menadzer.Ime,
                Prezime = menadzer.Prezime,
                ImeKluba = menadzer.Klub?.ImeKluba,
                GodineIskustva = menadzer.GodineIskustva,
                Nacionalnost = menadzer.Nacionalnost
            };

            return View(viewModel);
        }

        // POST: Menadzeri/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _menadzerService.DeleteMenadzerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}