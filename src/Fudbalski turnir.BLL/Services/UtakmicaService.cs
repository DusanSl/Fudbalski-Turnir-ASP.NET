using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

public class UtakmicaService : IUtakmiceService
{
    private readonly ApplicationDbContext _context;

    public UtakmicaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UtakmicaDTO>> GetAllUtakmiceAsync()
    {
        return await _context.Utakmica
            .Include(u => u.Turnir) 
            .OrderByDescending(u => u.Datum)
            .Select(u => new UtakmicaDTO
            {
                UtakmicaID = u.UtakmicaID,
                TurnirID = u.TurnirID,
                NazivTurnira = u.Turnir != null ? u.Turnir.NazivTurnira : "Nema turnira",
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
            }).ToListAsync();
    }

    public async Task<UtakmicaDTO?> GetUtakmicaByIdAsync(int id)
    {
        var u = await _context.Utakmica
            .Include(u => u.Turnir)
            .FirstOrDefaultAsync(u => u.UtakmicaID == id);

        if (u == null) return null;

        return new UtakmicaDTO
        {
            UtakmicaID = u.UtakmicaID,
            TurnirID = u.TurnirID,
            NazivTurnira = u.Turnir?.NazivTurnira,
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
    }

    public async Task CreateUtakmicaAsync(UtakmicaDTO dto)
    {
        if (dto.PrviKlubNaziv == dto.DrugiKlubNaziv)
        {
            throw new ArgumentException("Klub ne može igrati utakmicu protiv samog sebe.");
        }
        var u = new Utakmica
        {
            TurnirID = dto.TurnirID,
            Datum = dto.Datum,
            Mesto = dto.Mesto,
            PrviKlubNaziv = dto.PrviKlubNaziv,
            DrugiKlubNaziv = dto.DrugiKlubNaziv,
            Kolo = dto.Kolo,
            PrviKlubGolovi = dto.PrviKlubGolovi,
            DrugiKlubGolovi = dto.DrugiKlubGolovi,
            Produzeci = dto.Produzeci,
            Penali = dto.Penali,
            PrviKlubPenali = dto.PrviKlubPenali,
            DrugiKlubPenali = dto.DrugiKlubPenali,
            Grupa = dto.Grupa
        };
        _context.Utakmica.Add(u);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUtakmicaAsync(UtakmicaDTO dto)
    {
        if (dto.PrviKlubNaziv == dto.DrugiKlubNaziv)
        {
            throw new ArgumentException("Klub ne može igrati utakmicu protiv samog sebe.");
        }
        var u = await _context.Utakmica.FindAsync(dto.UtakmicaID);
        if (u != null)
        {
            u.TurnirID = dto.TurnirID;
            u.Datum = dto.Datum;
            u.Mesto = dto.Mesto;
            u.PrviKlubNaziv = dto.PrviKlubNaziv;
            u.DrugiKlubNaziv = dto.DrugiKlubNaziv;
            u.Kolo = dto.Kolo;
            u.PrviKlubGolovi = dto.PrviKlubGolovi;
            u.DrugiKlubGolovi = dto.DrugiKlubGolovi;
            u.Produzeci = dto.Produzeci;
            u.Penali = dto.Penali;
            u.PrviKlubPenali = dto.PrviKlubPenali;
            u.DrugiKlubPenali = dto.DrugiKlubPenali;
            u.Grupa = dto.Grupa;
            _context.Utakmica.Update(u);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUtakmicaAsync(int id)
    {
        var u = await _context.Utakmica.FindAsync(id);
        if (u != null)
        {
            _context.Utakmica.Remove(u);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> UtakmicaExistsAsync(int id) => await _context.Utakmica.AnyAsync(e => e.UtakmicaID == id);
    public async Task<IEnumerable<Turnir>> GetAllTurniriAsync() => await _context.Turnir.ToListAsync();
    public async Task<IEnumerable<object>> GetKluboviByTurnirAsync(int turnirId) =>
        await _context.Klub.Where(k => k.Turniri.Any(t => t.TurnirID == turnirId))
            .Select(k => new { k.KlubID, k.ImeKluba }).ToListAsync();

    public async Task<TurnirPregledDTO> GetStandingsModelAsync(int turnirId, string faza)
    {
        var model = new TurnirPregledDTO();

        if (faza == "Grupna")
        {
            var utakmice = await _context.Utakmica
                .Where(u => u.TurnirID == turnirId && u.Kolo == "Grupna faza")
                .ToListAsync();

            var grupisano = utakmice.GroupBy(u => u.Grupa).OrderBy(g => g.Key);

            foreach (var grupa in grupisano)
            {
                if (!string.IsNullOrEmpty(grupa.Key))
                {
                    model.Grupe[grupa.Key] = CalculateStandings(grupa.ToList());
                }
            }
        }
        else
        {
            var knockout = await _context.Utakmica
                .Where(u => u.TurnirID == turnirId && u.Kolo != "Grupna faza")
                .ToListAsync();

            model.Bracket = new BracketDTO
            {
                OsminaFinala = knockout.Where(u => u.Kolo == "Osmina finala").Select(MapToKnockout).ToList(),
                Cetvrtfinale = knockout.Where(u => u.Kolo == "Četvrtfinale").Select(MapToKnockout).ToList(),
                Polufinale = knockout.Where(u => u.Kolo == "Polufinale").Select(MapToKnockout).ToList(),
                Finale = knockout.Where(u => u.Kolo == "Finale").Select(MapToKnockout).FirstOrDefault()
            };
        }

        return model;
    }

    private List<StandingsDTO> CalculateStandings(List<Utakmica> utakmice)
    {
        var standings = new Dictionary<string, StandingsDTO>();

        var nazivi = utakmice.Select(u => u.PrviKlubNaziv).Union(utakmice.Select(u => u.DrugiKlubNaziv)).Distinct().ToList();
        var kluboviMapa = _context.Klub.Where(k => nazivi.Contains(k.ImeKluba)).ToDictionary(k => k.ImeKluba, k => k.KlubID);

        foreach (var u in utakmice)
        {
            foreach (var naziv in new[] { u.PrviKlubNaziv, u.DrugiKlubNaziv })
            {
                if (!standings.ContainsKey(naziv))
                    standings[naziv] = new StandingsDTO { KlubNaziv = naziv, KlubID = kluboviMapa.GetValueOrDefault(naziv) };
            }

            var d = standings[u.PrviKlubNaziv];
            var g = standings[u.DrugiKlubNaziv];

            d.Odigrano++;
            g.Odigrano++;

            d.DatiGolovi += u.PrviKlubGolovi;
            d.PrimljeniGolovi += u.DrugiKlubGolovi;
            g.DatiGolovi += u.DrugiKlubGolovi;
            g.PrimljeniGolovi += u.PrviKlubGolovi;

            if (u.PrviKlubGolovi > u.DrugiKlubGolovi)
            {
                d.Pobede++; d.Bodovi += 3; g.Porazi++;
            }
            else if (u.DrugiKlubGolovi > u.PrviKlubGolovi)
            {
                g.Pobede++; g.Bodovi += 3; d.Porazi++;
            }
            else
            {
                d.Nereseno++; g.Nereseno++; d.Bodovi += 1; g.Bodovi += 1;
            }
        }

        return standings.Values
            .OrderByDescending(s => s.Bodovi)
            .ThenByDescending(s => s.GolRazlika)
            .ToList();
    }

    private KnockoutUtakmicaDTO MapToKnockout(Utakmica u) => new KnockoutUtakmicaDTO
    {
        UtakmicaID = u.UtakmicaID,
        PrviKlubNaziv = u.PrviKlubNaziv,
        DrugiKlubNaziv = u.DrugiKlubNaziv,
        PrviKlubGolovi = u.PrviKlubGolovi,
        DrugiKlubGolovi = u.DrugiKlubGolovi,
        Pobednik = u.PrviKlubGolovi > u.DrugiKlubGolovi ? u.PrviKlubNaziv : (u.DrugiKlubGolovi > u.PrviKlubGolovi ? u.DrugiKlubNaziv : "")
    };
}