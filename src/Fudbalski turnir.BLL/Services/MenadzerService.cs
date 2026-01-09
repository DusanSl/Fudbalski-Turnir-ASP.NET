using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

public class MenadzerService : IMenadzerService
{
    private readonly ApplicationDbContext _context;
    public MenadzerService(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<MenadzerDTO>> GetAllMenadzerAsync()
    {
        return await _context.Menadzer
            .Include(m => m.Klub)
            .Select(m => new MenadzerDTO
            {
                OsobaID = m.OsobaID,
                Ime = m.Ime,
                Prezime = m.Prezime,
                GodineIskustva = m.GodineIskustva,
                DatumRodjenja = m.DatumRodjenja,
                Nacionalnost = m.Nacionalnost,
                KlubID = m.KlubID,
                ImeKluba = m.Klub!.ImeKluba
            }).ToListAsync();
    }

    public async Task<MenadzerDTO?> GetMenadzerByIdAsync(int id)
    {
        var m = await _context.Menadzer
            .Include(m => m.Klub)
            .FirstOrDefaultAsync(m => m.OsobaID == id);

        if (m == null) return null;

        return new MenadzerDTO
        {
            OsobaID = m.OsobaID,
            Ime = m.Ime,
            Prezime = m.Prezime,
            GodineIskustva = m.GodineIskustva,
            DatumRodjenja = m.DatumRodjenja,
            Nacionalnost = m.Nacionalnost,
            KlubID = m.KlubID,
            ImeKluba = m.Klub?.ImeKluba
        };
    }

    public async Task CreateMenadzerAsync(MenadzerDTO dto)
    {
        var menadzer = new Menadzer
        {
            Ime = dto.Ime,
            Prezime = dto.Prezime,
            DatumRodjenja = dto.DatumRodjenja,
            Nacionalnost = dto.Nacionalnost,
            GodineIskustva = dto.GodineIskustva,
            KlubID = dto.KlubID
        };
        _context.Menadzer.Add(menadzer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMenadzerAsync(MenadzerDTO dto)
    {
        var existing = await _context.Menadzer.FindAsync(dto.OsobaID);
        if (existing != null)
        {
            existing.Ime = dto.Ime;
            existing.Prezime = dto.Prezime;
            existing.DatumRodjenja = dto.DatumRodjenja;
            existing.Nacionalnost = dto.Nacionalnost;
            existing.GodineIskustva = dto.GodineIskustva;
            existing.KlubID = dto.KlubID;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteMenadzerAsync(int id)
    {
        var menadzer = await _context.Menadzer.FindAsync(id);
        if (menadzer != null)
        {
            _context.Menadzer.Remove(menadzer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Klub>> GetAllKluboviAsync() => await _context.Klub.ToListAsync();
}