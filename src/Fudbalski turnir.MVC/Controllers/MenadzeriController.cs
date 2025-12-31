using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.DTOs;
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
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var menadzeriDto = await _menadzerService.GetAllMenadzerAsync();

            var viewModel = menadzeriDto.Select(m => new MenadzerViewModel
            {
                OsobaID = m.OsobaID,
                Ime = m.Ime,
                Prezime = m.Prezime,
                GodineIskustva = m.GodineIskustva,
                DatumRodjenja = m.DatumRodjenja,
                Nacionalnost = m.Nacionalnost,
                KlubID = m.KlubID,
                ImeKluba = m.ImeKluba
            }).ToList();

            return View(viewModel);
        }

        // GET: Menadzeri/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var m = await _menadzerService.GetMenadzerByIdAsync(id);
            if (m == null) return NotFound();

            var viewModel = new MenadzerViewModel
            {
                OsobaID = m.OsobaID,
                Ime = m.Ime,
                Prezime = m.Prezime,
                DatumRodjenja = m.DatumRodjenja,
                Nacionalnost = m.Nacionalnost,
                GodineIskustva = m.GodineIskustva,
                KlubID = m.KlubID,
                ImeKluba = m.ImeKluba
            };

            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(viewModel);
        }

        // GET: Menadzeri/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var klubovi = await _menadzerService.GetAllKluboviAsync();
            ViewBag.KlubID = new SelectList(klubovi, "KlubID", "ImeKluba");
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
                var dto = new MenadzerDTO
                {
                    Ime = viewModel.Ime,
                    Prezime = viewModel.Prezime,
                    DatumRodjenja = viewModel.DatumRodjenja,
                    Nacionalnost = viewModel.Nacionalnost,
                    GodineIskustva = viewModel.GodineIskustva,
                    KlubID = viewModel.KlubID
                };

                await _menadzerService.CreateMenadzerAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            var klubovi = await _menadzerService.GetAllKluboviAsync();
            ViewBag.KlubID = new SelectList(klubovi, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        // GET: Menadzeri/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var m = await _menadzerService.GetMenadzerByIdAsync(id);
            if (m == null) return NotFound();

            var viewModel = new MenadzerViewModel
            {
                OsobaID = m.OsobaID,
                Ime = m.Ime,
                Prezime = m.Prezime,
                DatumRodjenja = m.DatumRodjenja,
                Nacionalnost = m.Nacionalnost,
                GodineIskustva = m.GodineIskustva,
                KlubID = m.KlubID
            };

            var klubovi = await _menadzerService.GetAllKluboviAsync();
            ViewBag.ListaKlubova = new SelectList(klubovi, "KlubID", "ImeKluba", viewModel.KlubID);

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
                var dto = new MenadzerDTO
                {
                    OsobaID = viewModel.OsobaID,
                    Ime = viewModel.Ime,
                    Prezime = viewModel.Prezime,
                    DatumRodjenja = viewModel.DatumRodjenja,
                    Nacionalnost = viewModel.Nacionalnost,
                    GodineIskustva = viewModel.GodineIskustva,
                    KlubID = viewModel.KlubID
                };

                await _menadzerService.UpdateMenadzerAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            var klubovi = await _menadzerService.GetAllKluboviAsync();
            ViewBag.KlubID = new SelectList(klubovi, "KlubID", "ImeKluba", viewModel.KlubID);
            return View(viewModel);
        }

        // GET: Menadzeri/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _menadzerService.GetMenadzerByIdAsync(id);
            if (m == null) return NotFound();

            var viewModel = new MenadzerViewModel
            {
                OsobaID = m.OsobaID,
                Ime = m.Ime,
                Prezime = m.Prezime,
                ImeKluba = m.ImeKluba,
                Nacionalnost = m.Nacionalnost,
                GodineIskustva = m.GodineIskustva,
                DatumRodjenja = m.DatumRodjenja,
            };

            return View(viewModel);
        }

        // POST: Menadzeri/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int OsobaID)
        {
            await _menadzerService.DeleteMenadzerAsync(OsobaID);
            return RedirectToAction(nameof(Index));
        }
    }
}