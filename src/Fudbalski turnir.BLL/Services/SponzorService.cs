using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.BLL.Services
{
    public class SponzorService : ISponzorService
    {
        private readonly ApplicationDbContext _context;

        public SponzorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sponzor>> GetAllSponzoriAsync()
        {
            return await _context.Sponzor
                .Include(s => s.Turniri)
                .ToListAsync();
        }

        public async Task<Sponzor?> GetSponzorByIdAsync(int id)
        {
            return await _context.Sponzor
                .Include(s => s.Turniri)
                .FirstOrDefaultAsync(s => s.SponzorID == id);
        }

        public async Task CreateSponzorAsync(Sponzor sponzor, int? turnirId)
        {
            if (turnirId.HasValue && turnirId.Value > 0)
            {
                var turnir = await _context.Turnir.FindAsync(turnirId.Value);
                if (turnir != null)
                {
                    sponzor.Turniri = new List<Turnir> { turnir };
                }
            }
            _context.Sponzor.Add(sponzor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSponzorAsync(Sponzor sponzor, int? turnirId)
        {
            var existingSponzor = await _context.Sponzor
                .Include(s => s.Turniri)
                .FirstOrDefaultAsync(s => s.SponzorID == sponzor.SponzorID);

            if (existingSponzor != null)
            {
                _context.Entry(existingSponzor).CurrentValues.SetValues(sponzor);

                existingSponzor.Turniri?.Clear();
                if (turnirId.HasValue && turnirId.Value > 0)
                {
                    var turnir = await _context.Turnir.FindAsync(turnirId.Value);
                    if (turnir != null) existingSponzor.Turniri?.Add(turnir);
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

        public async Task<IEnumerable<Turnir>> GetAllTurniriAsync()
        {
            return await _context.Turnir.ToListAsync();
        }
    }
}