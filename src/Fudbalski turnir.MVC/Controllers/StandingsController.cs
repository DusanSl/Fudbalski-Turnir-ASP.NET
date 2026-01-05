using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class StandingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? turnirId, string faza = "Grupna")
        {
            // Dropdown za turnire
            ViewBag.Turniri = new SelectList(
                await _context.Turnir.ToListAsync(),
                "TurnirID",
                "NazivTurnira"
            );

            ViewBag.SelectedTurnirId = turnirId;
            ViewBag.SelectedFaza = faza;

            if (turnirId == null)
            {
                return View(new GrupnaFazaViewModel
                {
                    Grupe = new Dictionary<string, List<StandingsViewModel>>(),
                    Bracket = new BracketViewModel()
                });
            }

            if (faza == "Grupna")
            {
                var sveUtakmice = await _context.Utakmica
                    .Where(u => u.TurnirID == turnirId && u.Kolo == "Grupna faza")
                    .ToListAsync();

                var grupe = new Dictionary<string, List<StandingsViewModel>>();

                var grupisano = sveUtakmice.GroupBy(u => u.Grupa).OrderBy(g => g.Key);

                foreach (var grupa in grupisano)
                {
                    if (!string.IsNullOrEmpty(grupa.Key))
                    {
                        var standings = CalculateStandings(grupa.ToList());
                        grupe[grupa.Key] = standings;
                    }
                }

                var model = new GrupnaFazaViewModel
                {
                    Grupe = grupe,
                    Bracket = new BracketViewModel()
                };
                return View(model);
            }
            else 
            {
                var bracket = await GetKnockoutBracket(turnirId.Value);

                var model = new GrupnaFazaViewModel
                {
                    Grupe = new Dictionary<string, List<StandingsViewModel>>(),
                    Bracket = bracket
                };
                return View(model);
            }
        }

        private List<StandingsViewModel> CalculateStandings(List<Utakmica> utakmice)
        {
            var standings = new Dictionary<string, StandingsViewModel>();

            var naziviKlubova = utakmice.Select(u => u.PrviKlubNaziv)
                                .Union(utakmice.Select(u => u.DrugiKlubNaziv))
                                .Distinct()
                                .ToList();

            var kluboviMapa = _context.Klub
                                .Where(k => naziviKlubova.Contains(k.ImeKluba))
                                .ToDictionary(k => k.ImeKluba, k => k.KlubID);

            foreach (var utakmica in utakmice)
            {
                if (!standings.ContainsKey(utakmica.PrviKlubNaziv))
                {
                    standings[utakmica.PrviKlubNaziv] = new StandingsViewModel
                    {
                        KlubNaziv = utakmica.PrviKlubNaziv,
                        KlubID = kluboviMapa.ContainsKey(utakmica.PrviKlubNaziv) ? kluboviMapa[utakmica.PrviKlubNaziv] : 0
                    };
                }

                if (!standings.ContainsKey(utakmica.DrugiKlubNaziv))
                {
                    standings[utakmica.DrugiKlubNaziv] = new StandingsViewModel
                    {
                        KlubNaziv = utakmica.DrugiKlubNaziv,
                        KlubID = kluboviMapa.ContainsKey(utakmica.DrugiKlubNaziv) ? kluboviMapa[utakmica.DrugiKlubNaziv] : 0
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
                .ThenByDescending(s => (s.DatiGolovi - s.PrimljeniGolovi)) 
                .ThenByDescending(s => s.DatiGolovi)
                .ToList();
        }

        private async Task<BracketViewModel> GetKnockoutBracket(int turnirId)
        {
            var sveKnockoutUtakmice = await _context.Utakmica
                .Where(u => u.TurnirID == turnirId && u.Kolo != "Grupna faza")
                .ToListAsync();

            return new BracketViewModel
            {
                OsminaFinala = sveKnockoutUtakmice
                    .Where(u => u.Kolo == "Osmina finala")
                    .OrderBy(u => u.UtakmicaID)
                    .Select(u => MapToKnockout(u)).ToList(),

                Cetvrtfinale = sveKnockoutUtakmice
                    .Where(u => u.Kolo == "Četvrtfinale")
                    .OrderBy(u => u.UtakmicaID)
                    .Select(u => MapToKnockout(u)).ToList(),

                Polufinale = sveKnockoutUtakmice
                    .Where(u => u.Kolo == "Polufinale")
                    .OrderBy(u => u.UtakmicaID)
                    .Select(u => MapToKnockout(u)).ToList(),

                Finale = sveKnockoutUtakmice
                    .Where(u => u.Kolo == "Finale")
                    .Select(u => MapToKnockout(u)).FirstOrDefault()
            };
        }

        private KnockoutUtakmica MapToKnockout(Utakmica u)
        {
            return new KnockoutUtakmica
            {
                UtakmicaID = u.UtakmicaID,
                PrviKlubNaziv = u.PrviKlubNaziv,
                DrugiKlubNaziv = u.DrugiKlubNaziv,
                PrviKlubGolovi = u.PrviKlubGolovi,
                DrugiKlubGolovi = u.DrugiKlubGolovi,
                Produzeci = u.Produzeci,
                Penali = u.Penali,
                PrviKlubPenali = u.PrviKlubPenali,
                DrugiKlubPenali = u.DrugiKlubPenali,
                Pobednik = GetWinner(u),
                Datum = u.Datum,
                Mesto = u.Mesto
            };
        }

        private static string GetWinner(Utakmica u)
        {
            if (u.PrviKlubGolovi == null || u.DrugiKlubGolovi == null)
                return null;

            if (u.Penali && u.PrviKlubPenali.HasValue && u.DrugiKlubPenali.HasValue)
            {
                return u.PrviKlubPenali > u.DrugiKlubPenali ? u.PrviKlubNaziv : u.DrugiKlubNaziv;
            }

            if (u.PrviKlubGolovi > u.DrugiKlubGolovi)
                return u.PrviKlubNaziv;
            else if (u.DrugiKlubGolovi > u.PrviKlubGolovi)
                return u.DrugiKlubNaziv;

            return null;
        }
    }
}