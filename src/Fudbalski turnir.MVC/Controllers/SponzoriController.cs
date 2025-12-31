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
    public class SponzoriController : Controller
    {
        private readonly ISponzorService _sponzorService;

        public SponzoriController(ISponzorService sponzorService)
        {
            _sponzorService = sponzorService;
        }

        // GET: Sponzori
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var sponzoriDto = await _sponzorService.GetAllSponzoriAsync();

            var viewModel = sponzoriDto.Select(s => new SponzorViewModel
            {
                SponzorID = s.SponzorID,
                ImeSponzora = s.ImeSponzora,
                KontaktSponzora = s.KontaktSponzora,
                VrednostSponzora = s.VrednostSponzora,
                NazivTurnira = s.NaziviTurnira.Any()
                               ? string.Join(", ", s.NaziviTurnira)
                               : "Nema dodeljenih turnira"
            }).ToList();

            return View(viewModel);
        }

        // GET: Sponzori/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var s = await _sponzorService.GetSponzorByIdAsync(id);
            if (s == null) return NotFound();

            var viewModel = new SponzorViewModel
            {
                SponzorID = s.SponzorID,
                ImeSponzora = s.ImeSponzora,
                KontaktSponzora = s.KontaktSponzora,
                VrednostSponzora = s.VrednostSponzora,
                TurnirID = s.PrimarniTurnirID,
                NazivTurnira = s.NaziviTurnira.Any()
                               ? string.Join(", ", s.NaziviTurnira)
                               : "Nema dodeljenih turnira"
            };

            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(viewModel);
        }

        // GET: Sponzori/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var turniri = await _sponzorService.GetAllTurniriAsync();
            ViewBag.ListaTurnira = new SelectList(turniri, "TurnirID", "NazivTurnira");
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
                var dto = new SponzorDTO
                {
                    ImeSponzora = viewModel.ImeSponzora,
                    KontaktSponzora = viewModel.KontaktSponzora,
                    VrednostSponzora = viewModel.VrednostSponzora,
                    PrimarniTurnirID = viewModel.TurnirID
                };

                await _sponzorService.CreateSponzorAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            var turniri = await _sponzorService.GetAllTurniriAsync();
            ViewBag.ListaTurnira = new SelectList(turniri, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        // GET: Sponzori/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var s = await _sponzorService.GetSponzorByIdAsync(id);
            if (s == null) return NotFound();

            var viewModel = new SponzorViewModel
            {
                SponzorID = s.SponzorID,
                ImeSponzora = s.ImeSponzora,
                KontaktSponzora = s.KontaktSponzora,
                VrednostSponzora = s.VrednostSponzora,
                TurnirID = s.PrimarniTurnirID
            };

            var turniri = await _sponzorService.GetAllTurniriAsync();
            ViewBag.ListaTurnira = new SelectList(turniri, "TurnirID", "NazivTurnira", viewModel.TurnirID);
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
                var dto = new SponzorDTO
                {
                    SponzorID = viewModel.SponzorID,
                    ImeSponzora = viewModel.ImeSponzora,
                    KontaktSponzora = viewModel.KontaktSponzora,
                    VrednostSponzora = viewModel.VrednostSponzora,
                    PrimarniTurnirID = viewModel.TurnirID
                };

                await _sponzorService.UpdateSponzorAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            var turniri = await _sponzorService.GetAllTurniriAsync();
            ViewBag.ListaTurnira = new SelectList(turniri, "TurnirID", "NazivTurnira", viewModel.TurnirID);
            return View(viewModel);
        }

        // GET: Sponzori/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _sponzorService.GetSponzorByIdAsync(id);
            if (s == null) return NotFound();

            var viewModel = new SponzorViewModel
            {
                SponzorID = s.SponzorID,
                ImeSponzora = s.ImeSponzora,
                KontaktSponzora = s.KontaktSponzora,
                VrednostSponzora = s.VrednostSponzora,
                NazivTurnira = s.NaziviTurnira.Any()
                               ? string.Join(", ", s.NaziviTurnira)
                               : "Nema dodeljenih turnira"
            };

            return View(viewModel);
        }

        // POST: Sponzori/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int SponzorID)
        {
            await _sponzorService.DeleteSponzorAsync(SponzorID);
            return RedirectToAction(nameof(Index));
        }
    }
}