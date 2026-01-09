using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "User,Admin")]
public class RezultatiController : Controller
{
    private readonly IUtakmiceService _utakmiceService;

    public RezultatiController(IUtakmiceService utakmiceService)
    {
        _utakmiceService = utakmiceService;
    }

    public async Task<IActionResult> Index()
    {
        var utakmiceDto = await _utakmiceService.GetAllUtakmiceAsync();

        var viewModel = utakmiceDto.Select(u => new UtakmicaViewModel
        {
            UtakmicaID = u.UtakmicaID,
            TurnirID = u.TurnirID,
            NazivTurnira = u.NazivTurnira,
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
}