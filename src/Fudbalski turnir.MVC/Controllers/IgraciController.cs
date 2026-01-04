using Fudbalski_turnir.BLL.DTO;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var igraciDto = await _igraciService.GetAllIgraceAsync();
            var viewModel = igraciDto.Select(i => new IgracViewModel
            {
                OsobaID = i.OsobaID,
                Ime = i.Ime,
                Prezime = i.Prezime,
                Pozicija = i.Pozicija,
                BrojDresa = i.BrojDresa,
                DatumRodjenja = i.DatumRodjenja,
                Nacionalnost = i.Nacionalnost,
                KlubID = i.KlubID,
                ImeKluba = i.ImeKluba
            }).ToList();

            return View(viewModel);
        }

        // GET: Igraci/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var i = await _igraciService.GetIgracByIdAsync(id);
            if (i == null) return NotFound();

            var viewModel = new IgracViewModel
            {
                OsobaID = i.OsobaID,
                Ime = i.Ime,
                Prezime = i.Prezime,
                Pozicija = i.Pozicija,
                BrojDresa = i.BrojDresa,
                DatumRodjenja = i.DatumRodjenja,
                Nacionalnost = i.Nacionalnost,
                KlubID = i.KlubID,
                ImeKluba = i.ImeKluba
            };
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(viewModel);
        }

        // GET: Igraci/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var klubovi = await _igraciService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba");
            return View(new IgracViewModel());
        }

        // POST: Igraci/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(IgracViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new IgracDTO
                {
                    Ime = vm.Ime,
                    Prezime = vm.Prezime,
                    Pozicija = vm.Pozicija,
                    BrojDresa = vm.BrojDresa,
                    DatumRodjenja = vm.DatumRodjenja,
                    Nacionalnost = vm.Nacionalnost,
                    KlubID = vm.KlubID
                };
                await _igraciService.CreateIgracAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Igraci/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var i = await _igraciService.GetIgracByIdAsync(id);
            if (i == null) return NotFound();

            var vm = new IgracViewModel
            {
                OsobaID = i.OsobaID,
                KlubID = i.KlubID,
                Ime = i.Ime,
                Prezime = i.Prezime,
                Pozicija = i.Pozicija,
                BrojDresa = i.BrojDresa,
                DatumRodjenja = i.DatumRodjenja,
                Nacionalnost = i.Nacionalnost
            };

            var klubovi = await _igraciService.GetAllKluboviAsync();
            ViewBag.Klub = new SelectList(klubovi, "KlubID", "ImeKluba", i.KlubID);
            return View(vm);
        }

        // POST: Igraci/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, IgracViewModel vm)
        {
            if (id != vm.OsobaID) return NotFound();

            if (ModelState.IsValid)
            {
                var dto = new IgracDTO
                {
                    OsobaID = vm.OsobaID,
                    Ime = vm.Ime,
                    Prezime = vm.Prezime,
                    Pozicija = vm.Pozicija,
                    BrojDresa = vm.BrojDresa,
                    DatumRodjenja = vm.DatumRodjenja,
                    Nacionalnost = vm.Nacionalnost,
                    KlubID = vm.KlubID
                };
                await _igraciService.UpdateIgracAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Igraci/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var i = await _igraciService.GetIgracByIdAsync(id);
            if (i == null) return NotFound();

            return View(new IgracViewModel
            {
                OsobaID = i.OsobaID,
                Ime = i.Ime,
                Prezime = i.Prezime,
                ImeKluba = i.ImeKluba,
                Pozicija = i.Pozicija,
                BrojDresa = i.BrojDresa,
                Nacionalnost = i.Nacionalnost,
            });
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