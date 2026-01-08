using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize(Roles = "Admin,User")]
public class StandingsController : Controller
{
    private readonly ITurnirService _turnirService;
    private readonly IUtakmiceService _utakmiceService;

    public StandingsController(ITurnirService turnirService, IUtakmiceService utakmiceService)
    {
        _turnirService = turnirService;
        _utakmiceService = utakmiceService;
    }

    public async Task<IActionResult> Index(int? turnirId, string faza = "Grupna")
    {
        var turniri = await _turnirService.GetAllTurniriAsync();
        ViewBag.Turniri = new SelectList(turniri, "TurnirID", "NazivTurnira");

        ViewBag.SelectedTurnirId = turnirId;
        ViewBag.SelectedFaza = faza;

        if (turnirId == null)
        {
            return View(new TurnirPregledDTO());
        }

        var model = await _utakmiceService.GetStandingsModelAsync(turnirId.Value, faza);

        return View(model);
    }
}