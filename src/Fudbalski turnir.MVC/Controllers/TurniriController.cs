using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var turniriDto = await _turnirService.GetAllTurniriAsync();

            var viewModel = turniriDto.Select(t => new TurnirViewModel
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
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var t = await _turnirService.GetTurnirByIdAsync(id.Value);
            if (t == null) return NotFound();

            var viewModel = new TurnirViewModel
            {
                TurnirID = t.TurnirID,
                NazivTurnira = t.NazivTurnira,
                Lokacija = t.Lokacija,
                DatumPocetka = t.DatumPocetka,
                DatumZavrsetka = t.DatumZavrsetka,
                TipTurnira = t.TipTurnira
            };

            ViewBag.IsAdmin = User.IsInRole("Admin");
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
                var dto = new TurnirDTO
                {
                    NazivTurnira = viewModel.NazivTurnira,
                    Lokacija = viewModel.Lokacija,
                    DatumPocetka = viewModel.DatumPocetka,
                    DatumZavrsetka = viewModel.DatumZavrsetka,
                    TipTurnira = viewModel.TipTurnira
                };

                await _turnirService.CreateTurnirAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Turniri/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var t = await _turnirService.GetTurnirByIdAsync(id.Value);
            if (t == null) return NotFound();

            var viewModel = new TurnirViewModel
            {
                TurnirID = t.TurnirID,
                NazivTurnira = t.NazivTurnira,
                Lokacija = t.Lokacija,
                DatumPocetka = t.DatumPocetka,
                DatumZavrsetka = t.DatumZavrsetka,
                TipTurnira = t.TipTurnira
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
                    var dto = new TurnirDTO
                    {
                        TurnirID = viewModel.TurnirID,
                        NazivTurnira = viewModel.NazivTurnira,
                        Lokacija = viewModel.Lokacija,
                        DatumPocetka = viewModel.DatumPocetka,
                        DatumZavrsetka = viewModel.DatumZavrsetka,
                        TipTurnira = viewModel.TipTurnira
                    };

                    await _turnirService.UpdateTurnirAsync(dto);
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

            var t = await _turnirService.GetTurnirByIdAsync(id.Value);
            if (t == null) return NotFound();

            var viewModel = new TurnirViewModel
            {
                TurnirID = t.TurnirID,
                NazivTurnira = t.NazivTurnira,
                Lokacija = t.Lokacija,
                DatumPocetka = t.DatumPocetka,
                DatumZavrsetka = t.DatumZavrsetka,
                TipTurnira = t.TipTurnira
            };

            return View(viewModel);
        }

        // POST: Turniri/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int TurnirID)
        {
            await _turnirService.DeleteTurnirAsync(TurnirID);
            return RedirectToAction(nameof(Index));
        }
    }
}