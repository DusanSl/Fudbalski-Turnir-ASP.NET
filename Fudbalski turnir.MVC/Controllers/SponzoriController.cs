using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fudbalski_turnir.Controllers
{
    public class SponzoriController : Controller
    {
        private readonly ISponzorService _sponzorService;

        public SponzoriController(ISponzorService sponzorService)
        {
            _sponzorService = sponzorService;
        }

        // GET: Sponzori
        public async Task<IActionResult> Index()
        {
            var sponzori = await _sponzorService.GetAllSponzoriAsync();

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

            var sponzor = await _sponzorService.GetSponzorByIdAsync(id.Value);
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

        // GET: Sponzori/Create
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var turniri = await _sponzorService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira");
            return View(new SponzorViewModel());
        }

        // POST: Sponzori/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SponzorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.TurnirID <= 0)
                {
                    ModelState.AddModelError("TurnirID", "Morate izabrati turnir.");
                    var turniriList = await _sponzorService.GetAllTurniriAsync();
                    ViewBag.Turniri = new SelectList(turniriList, "TurnirID", "NazivTurnira");
                    return View(viewModel);
                }

                var sponzor = new Sponzor
                {
                    ImeSponzora = viewModel.ImeSponzora,
                    KontaktSponzora = viewModel.KontaktSponzora,
                    VrednostSponzora = viewModel.VrednostSponzora
                };

                await _sponzorService.CreateSponzorAsync(sponzor, viewModel.TurnirID);
                return RedirectToAction(nameof(Index));
            }

            var turniriBack = await _sponzorService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniriBack, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        // GET: Sponzori/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sponzor = await _sponzorService.GetSponzorByIdAsync(id.Value);
            if (sponzor == null) return NotFound();

            var viewModel = new SponzorViewModel
            {
                SponzorID = sponzor.SponzorID,
                ImeSponzora = sponzor.ImeSponzora,
                KontaktSponzora = sponzor.KontaktSponzora,
                VrednostSponzora = sponzor.VrednostSponzora,
                TurnirID = sponzor.Turniri?.FirstOrDefault()?.TurnirID
            };

            var turniri = await _sponzorService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira", viewModel.TurnirID);

            return View(viewModel);
        }

        // POST: Sponzori/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SponzorViewModel viewModel)
        {
            if (id != viewModel.SponzorID) return NotFound();

            if (ModelState.IsValid)
            {
                var sponzor = new Sponzor
                {
                    SponzorID = viewModel.SponzorID,
                    ImeSponzora = viewModel.ImeSponzora,
                    KontaktSponzora = viewModel.KontaktSponzora,
                    VrednostSponzora = viewModel.VrednostSponzora
                };

                await _sponzorService.UpdateSponzorAsync(sponzor, viewModel.TurnirID);
                return RedirectToAction(nameof(Index));
            }

            var turniri = await _sponzorService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        // GET: Sponzori/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sponzor = await _sponzorService.GetSponzorByIdAsync(id.Value);
            if (sponzor == null) return NotFound();

            var viewModel = new SponzorViewModel
            {
                SponzorID = sponzor.SponzorID,
                ImeSponzora = sponzor.ImeSponzora,
                KontaktSponzora = sponzor.KontaktSponzora,
                VrednostSponzora = sponzor.VrednostSponzora,
                Turniri = sponzor.Turniri?.ToList()
            };

            return View(viewModel);
        }

        // POST: Sponzori/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sponzorService.DeleteSponzorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}