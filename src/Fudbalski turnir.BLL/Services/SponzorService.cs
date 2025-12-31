using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

public class SponzorService : ISponzorService
{
    private readonly ApplicationDbContext _context;
    public SponzorService(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<SponzorDTO>> GetAllSponzoriAsync()
    {
        return await _context.Sponzor
            .Include(s => s.Turniri)
            .Select(s => new SponzorDTO
            {
                SponzorID = s.SponzorID,
                ImeSponzora = s.ImeSponzora,
                KontaktSponzora = s.KontaktSponzora,
                VrednostSponzora = s.VrednostSponzora,
                NaziviTurnira = s.Turniri.Select(t => t.NazivTurnira).ToList()
            }).ToListAsync();
    }

    public async Task<SponzorDTO?> GetSponzorByIdAsync(int id)
    {
        var s = await _context.Sponzor
            .Include(s => s.Turniri)
            .FirstOrDefaultAsync(s => s.SponzorID == id);

        if (s == null) return null;

        return new SponzorDTO
        {
            SponzorID = s.SponzorID,
            ImeSponzora = s.ImeSponzora,
            KontaktSponzora = s.KontaktSponzora,
            VrednostSponzora = s.VrednostSponzora,
            PrimarniTurnirID = s.Turniri.FirstOrDefault()?.TurnirID,
            NaziviTurnira = s.Turniri.Select(t => t.NazivTurnira).ToList()
        };
    }

    public async Task CreateSponzorAsync(SponzorDTO dto)
    {
        var sponzor = new Sponzor
        {
            ImeSponzora = dto.ImeSponzora,
            KontaktSponzora = dto.KontaktSponzora,
            VrednostSponzora = dto.VrednostSponzora,
            Turniri = new List<Turnir>()
        };

        if (dto.PrimarniTurnirID.HasValue)
        {
            var turnir = await _context.Turnir.FindAsync(dto.PrimarniTurnirID.Value);
            if (turnir != null) sponzor.Turniri.Add(turnir);
        }

        _context.Sponzor.Add(sponzor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSponzorAsync(SponzorDTO dto)
    {
        var existing = await _context.Sponzor
            .Include(s => s.Turniri)
            .FirstOrDefaultAsync(s => s.SponzorID == dto.SponzorID);

        if (existing != null)
        {
            existing.ImeSponzora = dto.ImeSponzora;
            existing.KontaktSponzora = dto.KontaktSponzora;
            existing.VrednostSponzora = dto.VrednostSponzora;

            existing.Turniri.Clear();
            if (dto.PrimarniTurnirID.HasValue)
            {
                var turnir = await _context.Turnir.FindAsync(dto.PrimarniTurnirID.Value);
                if (turnir != null) existing.Turniri.Add(turnir);
            }

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteSponzorAsync(int id)
    {
        var sponzor = await _context.Sponzor.FindAsync(id);
        if (sponzor != null)
        {
            _context.Sponzor.Remove(sponzor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Turnir>> GetAllTurniriAsync() => await _context.Turnir.ToListAsync();
}