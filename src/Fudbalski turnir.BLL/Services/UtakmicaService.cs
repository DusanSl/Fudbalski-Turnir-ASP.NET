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
}