using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fudbalski_turnir.Controllers
{
    public class TurniriController : Controller
    {
        private readonly ITurnirService _turnirService;

        public TurniriController(ITurnirService turnirService)
        {
            _turnirService = turnirService;
        }

        // GET: Turniri
        public async Task<IActionResult> Index()
        {
            var turniri = await _turnirService.GetAllTurniriAsync();

            var viewModel = turniri.Select(t => new TurnirViewModel
            {
                TurnirID = t.TurnirID,
                NazivTurnira = t.NazivTurnira,
                Lokacija = t.Lokacija,
                DatumPocetka = t.DatumPocetka,
                DatumZavrsetka = t.DatumZavrsetka,
                TipTurnira = t.TipTurnira
            }).ToList();

            return View(viewModel);
        }

        // GET: Turniri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var turnir = await _turnirService.GetTurnirByIdAsync(id.Value);
            if (turnir == null) return NotFound();

            ViewBag.IsAdmin = User.IsInRole("Admin");
            var viewModel = new TurnirViewModel
            {
                TurnirID = turnir.TurnirID,
                NazivTurnira = turnir.NazivTurnira,
                Lokacija = turnir.Lokacija,
                DatumPocetka = turnir.DatumPocetka,
                DatumZavrsetka = turnir.DatumZavrsetka,
                TipTurnira = turnir.TipTurnira
            };

            return View(viewModel);
        }

        // GET: Turniri/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new TurnirViewModel());
        }

        // POST: Turniri/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurnirViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var turnir = new Turnir
                {
                    NazivTurnira = viewModel.NazivTurnira,
                    Lokacija = viewModel.Lokacija,
                    DatumPocetka = viewModel.DatumPocetka,
                    DatumZavrsetka = viewModel.DatumZavrsetka,
                    TipTurnira = viewModel.TipTurnira
                };

                await _turnirService.CreateTurnirAsync(turnir);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Turniri/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var turnir = await _turnirService.GetTurnirByIdAsync(id.Value);
            if (turnir == null) return NotFound();

            var viewModel = new TurnirViewModel
            {
                TurnirID = turnir.TurnirID,
                NazivTurnira = turnir.NazivTurnira,
                Lokacija = turnir.Lokacija,
                DatumPocetka = turnir.DatumPocetka,
                DatumZavrsetka = turnir.DatumZavrsetka,
                TipTurnira = turnir.TipTurnira
            };

            return View(viewModel);
        }

        // POST: Turniri/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TurnirViewModel viewModel)
        {
            if (id != viewModel.TurnirID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var turnir = new Turnir
                    {
                        TurnirID = viewModel.TurnirID,
                        NazivTurnira = viewModel.NazivTurnira,
                        Lokacija = viewModel.Lokacija,
                        DatumPocetka = viewModel.DatumPocetka,
                        DatumZavrsetka = viewModel.DatumZavrsetka,
                        TipTurnira = viewModel.TipTurnira
                    };

                    await _turnirService.UpdateTurnirAsync(turnir);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _turnirService.TurnirExistsAsync(viewModel.TurnirID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Turniri/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var turnir = await _turnirService.GetTurnirByIdAsync(id.Value);
            if (turnir == null) return NotFound();

            var viewModel = new TurnirViewModel
            {
                TurnirID = turnir.TurnirID,
                NazivTurnira = turnir.NazivTurnira,
                Lokacija = turnir.Lokacija,
                DatumPocetka = turnir.DatumPocetka,
                DatumZavrsetka = turnir.DatumZavrsetka,
                TipTurnira = turnir.TipTurnira
            };

            return View(viewModel);
        }

        // POST: Turniri/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _turnirService.DeleteTurnirAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}