using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class StandingsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StandingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? turnirId, string faza = "Grupna")
    {
        ViewBag.Turniri = new SelectList(
            await _context.Turnir.ToListAsync(),
            "TurnirID",
            "NazivTurnira"
        );

        ViewBag.SelectedTurnirId = turnirId;
        ViewBag.SelectedFaza = faza;

        if (turnirId == null)
        {
            return View(new List<StandingsViewModel>());
        }

        var utakmice = await _context.Utakmica
            .Where(u => u.TurnirID == turnirId)
            .ToListAsync();

        if (faza == "Grupna")
        {
            utakmice = utakmice.Where(u =>
                u.Kolo.Contains("Kolo") ||
                u.Kolo.Contains("1") ||
                u.Kolo.Contains("2") ||
                u.Kolo.Contains("3") ||
                u.Kolo.Contains("4") ||
                u.Kolo.Contains("5") ||
                u.Kolo.Contains("6")
            ).ToList();
        }
        else // Knockout
        {
            utakmice = utakmice.Where(u =>
                u.Kolo.Contains("Osmina") ||
                u.Kolo.Contains("Četvrtfinale") ||
                u.Kolo.Contains("Polufinale") ||
                u.Kolo.Contains("Finale")
            ).ToList();
        }

        var standings = CalculateStandings(utakmice);

        return View(standings);
    }

    private List<StandingsViewModel> CalculateStandings(List<Utakmica> utakmice)
    {
        var standings = new Dictionary<string, StandingsViewModel>();

        foreach (var utakmica in utakmice)
        {
            // Prvi tim je uvek domaći
            if (!standings.ContainsKey(utakmica.PrviKlubNaziv))
            {
                standings[utakmica.PrviKlubNaziv] = new StandingsViewModel
                {
                    KlubNaziv = utakmica.PrviKlubNaziv
                };
            }

            // Drugi tim je uvek gostujući
            if (!standings.ContainsKey(utakmica.DrugiKlubNaziv))
            {
                standings[utakmica.DrugiKlubNaziv] = new StandingsViewModel
                {
                    KlubNaziv = utakmica.DrugiKlubNaziv
                };
            }

            var domacin = standings[utakmica.PrviKlubNaziv];
            var gost = standings[utakmica.DrugiKlubNaziv];

            domacin.Odigrano++;
            gost.Odigrano++;

            domacin.DatiGolovi += utakmica.PrviKlubGolovi;
            domacin.PrimljeniGolovi += utakmica.DrugiKlubGolovi;

            gost.DatiGolovi += utakmica.DrugiKlubGolovi;
            gost.PrimljeniGolovi += utakmica.PrviKlubGolovi;

            if (utakmica.PrviKlubGolovi > utakmica.DrugiKlubGolovi)
            {
                domacin.Pobede++;
                domacin.Bodovi += 3;
                gost.Porazi++;
            }
            else if (utakmica.DrugiKlubGolovi > utakmica.PrviKlubGolovi)
            {
                gost.Pobede++;
                gost.Bodovi += 3;
                domacin.Porazi++;
            }
            else
            {
                domacin.Nereseno++;
                gost.Nereseno++;
                domacin.Bodovi += 1;
                gost.Bodovi += 1;
            }
        }

        return standings.Values
            .OrderByDescending(s => s.Bodovi)
            .ThenByDescending(s => s.GolRazlika)
            .ThenByDescending(s => s.DatiGolovi)
            .ToList();
    }
}