using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fudbalski_turnir.Controllers
{
    [Authorize]
    public class KluboviController : Controller
    {
        private readonly IKlubService _kluboviService;

        public KluboviController(IKlubService kluboviService)
        {
            _kluboviService = kluboviService;
        }

        // GET: Klubovi
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var kluboviDto = await _kluboviService.GetAllKluboviAsync();

            var viewModel = kluboviDto.Select(k => new KlubViewModel
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
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var k = await _kluboviService.GetKlubByIdAsync(id);
            if (k == null) return NotFound();

            var viewModel = new KlubViewModel
            {
                KlubID = k.KlubID,
                ImeKluba = k.ImeKluba,
                RankingTima = k.RankingTima,
                Stadion = k.Stadion,
                BrojOsvojenihTitula = k.BrojOsvojenihTitula,
                GodinaOsnivanja = k.GodinaOsnivanja,
                BrojIgraca = k.BrojIgraca,
                TurnirID = k.PrimarniTurnirID,
                NazivTurnira = (k.NazivTurnira != null && k.NazivTurnira.Any())
                               ? string.Join(", ", k.NazivTurnira)
                               : "Nema dodeljenih turnira"
            };

            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(viewModel);
        }

        // GET: Klubovi/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var turniri = await _kluboviService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira");
            return View(new KlubViewModel());
        }

        // POST: Klubovi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(KlubViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new KlubDTO
                {
                    ImeKluba = vm.ImeKluba,
                    GodinaOsnivanja = vm.GodinaOsnivanja,
                    RankingTima = vm.RankingTima,
                    BrojIgraca = vm.BrojIgraca,
                    Stadion = vm.Stadion,
                    BrojOsvojenihTitula = vm.BrojOsvojenihTitula,
                    PrimarniTurnirID = vm.TurnirID
                };

                await _kluboviService.CreateKlubAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            var turniri = await _kluboviService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira", vm.TurnirID);
            return View(vm);
        }

        // GET: Klubovi/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var k = await _kluboviService.GetKlubByIdAsync(id);
            if (k == null) return NotFound();

            var vm = new KlubViewModel
            {
                KlubID = k.KlubID,
                ImeKluba = k.ImeKluba,
                GodinaOsnivanja = k.GodinaOsnivanja,
                RankingTima = k.RankingTima,
                BrojIgraca = k.BrojIgraca,
                Stadion = k.Stadion,
                BrojOsvojenihTitula = k.BrojOsvojenihTitula,
                TurnirID = k.PrimarniTurnirID
            };

            var turniri = await _kluboviService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira", k.PrimarniTurnirID);
            return View(vm);
        }

        // POST: Klubovi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, KlubViewModel vm)
        {
            if (id != vm.KlubID) return NotFound();

            if (ModelState.IsValid)
            {
                var dto = new KlubDTO
                {
                    KlubID = vm.KlubID,
                    ImeKluba = vm.ImeKluba,
                    GodinaOsnivanja = vm.GodinaOsnivanja,
                    RankingTima = vm.RankingTima,
                    BrojIgraca = vm.BrojIgraca,
                    Stadion = vm.Stadion,
                    BrojOsvojenihTitula = vm.BrojOsvojenihTitula,
                    PrimarniTurnirID = vm.TurnirID
                };

                await _kluboviService.UpdateKlubAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            var turniri = await _kluboviService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira", vm.TurnirID);
            return View(vm);
        }

        // GET: Klubovi/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var k = await _kluboviService.GetKlubByIdAsync(id);
            if (k == null) return NotFound();

            return View(new KlubViewModel
            {
                KlubID = k.KlubID,
                ImeKluba = k.ImeKluba,
                Stadion = k.Stadion,
                GodinaOsnivanja = k.GodinaOsnivanja,
                RankingTima = k.RankingTima,
                BrojIgraca = k.BrojIgraca,
                BrojOsvojenihTitula = k.BrojOsvojenihTitula,
                NazivTurnira = (k.NazivTurnira != null && k.NazivTurnira.Any())
                               ? string.Join(", ", k.NazivTurnira)
                               : "Nema dodeljenih turnira"
            });
        }

        // POST: Klubovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _kluboviService.DeleteKlubAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}