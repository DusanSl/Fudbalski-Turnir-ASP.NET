using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fudbalski_turnir.Controllers
{
    public class UtakmiceController : Controller
    {
        private readonly IUtakmiceService _utakmiceService;

        public UtakmiceController(IUtakmiceService utakmiceService)
        {
            _utakmiceService = utakmiceService;
        }

        // GET: Utakmice
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var utakmice = await _utakmiceService.GetAllUtakmiceAsync();
            
            var viewModel = utakmice.Select(u => new UtakmicaViewModel
            {
                UtakmicaID = u.UtakmicaID,
                TurnirID = u.TurnirID,
                NazivTurnira = u.NazivTurnira ?? "Nema turnira",
                Datum = u.Datum,
                Mesto = u.Mesto,
                PrviKlubNaziv = u.PrviKlubNaziv,
                DrugiKlubNaziv = u.DrugiKlubNaziv,
                Kolo = u.Kolo,
                PrviKlubGolovi = u.PrviKlubGolovi,
                DrugiKlubGolovi = u.DrugiKlubGolovi,
                Produzeci = u.Produzeci,
                Penali = u.Penali,
                PrviKlubPenali = u.PrviKlubPenali,
                DrugiKlubPenali = u.DrugiKlubPenali,
                Grupa = u.Grupa
            }).ToList();

            return View(viewModel);
        }

        // GET: Utakmice/Details/5
        [Authorize(Roles = "Admin, Users")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var u = await _utakmiceService.GetUtakmicaByIdAsync(id.Value);
            if (u == null) return NotFound();

            var viewModel = new UtakmicaViewModel
            {
                UtakmicaID = u.UtakmicaID,
                TurnirID = u.TurnirID,
                Datum = u.Datum,
                Mesto = u.Mesto,
                PrviKlubNaziv = u.PrviKlubNaziv,
                DrugiKlubNaziv = u.DrugiKlubNaziv,
                Kolo = u.Kolo,
                PrviKlubGolovi = u.PrviKlubGolovi,
                DrugiKlubGolovi = u.DrugiKlubGolovi,
                Produzeci = u.Produzeci,
                Penali = u.Penali,
                PrviKlubPenali = u.PrviKlubPenali,
                DrugiKlubPenali = u.DrugiKlubPenali,
                Grupa = u.Grupa,
                NazivTurnira = u.NazivTurnira
            };

            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(viewModel);
        }

        // GET: Utakmice/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var turniri = await _utakmiceService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira");
            return View(new UtakmicaViewModel());
        }

        // POST: Utakmice/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UtakmicaViewModel viewModel)
        {
            if (viewModel.PrviKlubNaziv == viewModel.DrugiKlubNaziv && !string.IsNullOrEmpty(viewModel.PrviKlubNaziv))
            {
                ModelState.AddModelError("DrugiKlubNaziv", "Domaćin i gost ne mogu biti isti klub.");
            }
            if (ModelState.IsValid)
            {
                if (viewModel.TurnirID <= 0)
                {
                    ModelState.AddModelError("TurnirID", "Morate izabrati turnir.");
                }
                else
                {
                    var utakmicaDto = new UtakmicaDTO
                    {
                        TurnirID = viewModel.TurnirID,
                        Datum = viewModel.Datum,
                        Mesto = viewModel.Mesto,
                        PrviKlubNaziv = viewModel.PrviKlubNaziv,
                        DrugiKlubNaziv = viewModel.DrugiKlubNaziv,
                        Kolo = viewModel.Kolo,
                        PrviKlubGolovi = viewModel.PrviKlubGolovi,
                        DrugiKlubGolovi = viewModel.DrugiKlubGolovi,
                        Produzeci = viewModel.Produzeci,
                        Penali = viewModel.Penali,
                        PrviKlubPenali = viewModel.PrviKlubPenali,
                        DrugiKlubPenali = viewModel.DrugiKlubPenali,
                        Grupa = viewModel.Grupa
                    };

                    await _utakmiceService.CreateUtakmicaAsync(utakmicaDto);
                    return RedirectToAction(nameof(Index));
                }
            }

            var turniri = await _utakmiceService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira");
            return View(viewModel);
        }

        // GET: Utakmice/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null) return NotFound();

            var u = await _utakmiceService.GetUtakmicaByIdAsync(id.Value);
            if (u == null) return NotFound();

            var viewModel = new UtakmicaViewModel
            {
                UtakmicaID = u.UtakmicaID,
                TurnirID = u.TurnirID,
                Datum = u.Datum,
                Mesto = u.Mesto,
                PrviKlubNaziv = u.PrviKlubNaziv,
                DrugiKlubNaziv = u.DrugiKlubNaziv,
                Kolo = u.Kolo,
                PrviKlubGolovi = u.PrviKlubGolovi,
                DrugiKlubGolovi = u.DrugiKlubGolovi,
                Produzeci = u.Produzeci,
                Penali = u.Penali,
                PrviKlubPenali = u.PrviKlubPenali,
                DrugiKlubPenali = u.DrugiKlubPenali,
                Grupa = u.Grupa
            };

            var turniri = await _utakmiceService.GetAllTurniriAsync();
            ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira", u.TurnirID);

            return View(viewModel);
        }

        // POST: Utakmice/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UtakmicaViewModel viewModel)
        {
            if (id != viewModel.UtakmicaID) return NotFound(); 
            
            if (viewModel.PrviKlubNaziv == viewModel.DrugiKlubNaziv && !string.IsNullOrEmpty(viewModel.PrviKlubNaziv))
            {
                ModelState.AddModelError("DrugiKlubNaziv", "Domaćin i gost ne mogu biti isti klub.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var utakmicaDto = new UtakmicaDTO
                    {
                        UtakmicaID = viewModel.UtakmicaID,
                        TurnirID = viewModel.TurnirID,
                        Datum = viewModel.Datum,
                        Mesto = viewModel.Mesto,
                        PrviKlubNaziv = viewModel.PrviKlubNaziv,
                        DrugiKlubNaziv = viewModel.DrugiKlubNaziv,
                        Kolo = viewModel.Kolo,
                        PrviKlubGolovi = viewModel.PrviKlubGolovi,
                        DrugiKlubGolovi = viewModel.DrugiKlubGolovi,
                        Produzeci = viewModel.Produzeci,
                        Penali = viewModel.Penali,
                        PrviKlubPenali = viewModel.PrviKlubPenali,
                        DrugiKlubPenali = viewModel.DrugiKlubPenali,
                        Grupa = viewModel.Grupa
                    };

                    await _utakmiceService.UpdateUtakmicaAsync(utakmicaDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _utakmiceService.UtakmicaExistsAsync(viewModel.UtakmicaID)) return NotFound();
                    else throw;
                }
            }
            var turniri = await _utakmiceService.GetAllTurniriAsync();
            ViewBag.Turniri = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(turniri, "TurnirID", "NazivTurnira");
            return View(viewModel);
        }

        // GET: Utakmice/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var u = await _utakmiceService.GetUtakmicaByIdAsync(id.Value);
            if (u == null) return NotFound();

            var viewModel = new UtakmicaViewModel
            {
                UtakmicaID = u.UtakmicaID,
                Datum = u.Datum,
                Mesto = u.Mesto,
                PrviKlubNaziv = u.PrviKlubNaziv,
                DrugiKlubNaziv = u.DrugiKlubNaziv,
                Kolo = u.Kolo,
                NazivTurnira = u.NazivTurnira,
                PrviKlubGolovi = u.PrviKlubGolovi,
                DrugiKlubGolovi = u.DrugiKlubGolovi,
                Produzeci = u.Produzeci,
                Penali = u.Penali,
                PrviKlubPenali = u.PrviKlubPenali,
                DrugiKlubPenali = u.DrugiKlubPenali,
                Grupa = u.Grupa
            };

            return View(viewModel);
        }

        // POST: Utakmice/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _utakmiceService.DeleteUtakmicaAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetKluboviByTurnir(int turnirId)
        {
            var klubovi = await _utakmiceService.GetKluboviByTurnirAsync(turnirId);
            return Json(klubovi);
        }
    }
}