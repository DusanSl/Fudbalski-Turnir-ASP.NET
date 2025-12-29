using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fudbalski_turnir.Controllers
{
    public class KluboviController : Controller
    {
        private readonly IKlubService _kluboviService;

        public KluboviController(IKlubService kluboviService)
        {
            _kluboviService = kluboviService;
        }

        // GET: Klubovi
        public async Task<IActionResult> Index()
        {
            // Koristimo servis umesto direktnog konteksta
            var klubovi = await _kluboviService.GetAllKluboviAsync();

            var viewModel = klubovi.Select(k => new KlubViewModel
            {
                KlubID = k.KlubID,
                ImeKluba = k.ImeKluba,
                GodinaOsnivanja = k.GodinaOsnivanja,
                RankingTima = k.RankingTima,
                BrojIgraca = k.BrojIgraca,
                Stadion = k.Stadion,
                BrojOsvojenihTitula = k.BrojOsvojenihTitula
            }).ToList();

            return View(viewModel);
        }

        // GET: Klubovi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var klub = await _kluboviService.GetKlubByIdAsync(id.Value);

            if (klub == null) return NotFound();

            ViewBag.IsAdmin = User.IsInRole("Admin");
            var viewModel = new KlubViewModel
            {
                KlubID = klub.KlubID,
                ImeKluba = klub.ImeKluba,
                RankingTima = klub.RankingTima,
                Stadion = klub.Stadion,
                Turniri = klub.Turniri, // Ovo je sada napunjeno jer servis radi .Include()
                BrojOsvojenihTitula = klub.BrojOsvojenihTitula,
                GodinaOsnivanja = klub.GodinaOsnivanja,
                BrojIgraca = klub.BrojIgraca
            };

            return View(viewModel);
        }

        // GET: Klubovi/Create
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var turniri = await _kluboviService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira");
            return View(new KlubViewModel());
        }

        // POST: Klubovi/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KlubViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.TurnirID <= 0)
                {
                    ModelState.AddModelError("TurnirID", "Morate izabrati turnir.");
                    var turniriList = await _kluboviService.GetAllTurniriAsync();
                    ViewBag.Turniri = new SelectList(turniriList, "TurnirID", "NazivTurnira");
                    return View(viewModel);
                }

                var klub = new Klub
                {
                    ImeKluba = viewModel.ImeKluba,
                    GodinaOsnivanja = viewModel.GodinaOsnivanja,
                    RankingTima = viewModel.RankingTima,
                    BrojIgraca = viewModel.BrojIgraca,
                    Stadion = viewModel.Stadion,
                    BrojOsvojenihTitula = viewModel.BrojOsvojenihTitula
                };

                await _kluboviService.CreateKlubAsync(klub, viewModel.TurnirID);
                return RedirectToAction(nameof(Index));
            }

            var turniriBack = await _kluboviService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniriBack, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var klub = await _kluboviService.GetKlubByIdAsync(id.Value);

            if (klub == null) return NotFound();

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

            var turniri = await _kluboviService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira", viewModel.TurnirID);

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KlubViewModel viewModel)
        {
            if (id != viewModel.KlubID) return NotFound();

            if (ModelState.IsValid)
            {
                var klub = new Klub
                {
                    KlubID = viewModel.KlubID,
                    ImeKluba = viewModel.ImeKluba,
                    GodinaOsnivanja = viewModel.GodinaOsnivanja,
                    RankingTima = viewModel.RankingTima,
                    BrojIgraca = viewModel.BrojIgraca,
                    Stadion = viewModel.Stadion,
                    BrojOsvojenihTitula = viewModel.BrojOsvojenihTitula
                };

                await _kluboviService.UpdateKlubAsync(klub, viewModel.TurnirID);
                return RedirectToAction(nameof(Index));
            }

            var turniri = await _kluboviService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var klub = await _kluboviService.GetKlubByIdAsync(id.Value);

            if (klub == null) return NotFound();

            var viewModel = new KlubViewModel
            {
                KlubID = klub.KlubID,
                ImeKluba = klub.ImeKluba,
                GodinaOsnivanja = klub.GodinaOsnivanja,
                RankingTima = klub.RankingTima,
                BrojIgraca = klub.BrojIgraca,
                Stadion = klub.Stadion,
                BrojOsvojenihTitula = klub.BrojOsvojenihTitula,
                Turniri = klub.Turniri?.ToList(),
                TurnirID = klub.Turniri?.FirstOrDefault()?.TurnirID,
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _kluboviService.DeleteKlubAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}