using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.BLL.Services
{
    public class SponzoriService : ISponzoriService
    {
        private readonly ApplicationDbContext _context;

        public SponzoriService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sponzor>> GetAllSponzoriAsync()
        {
            return await _context.Sponzor.ToListAsync();
        }

        public async Task<Sponzor?> GetSponzorByIdAsync(int id)
        {
            return await _context.Sponzor.FindAsync(id);
        }

        public async Task CreateSponzorAsync(Sponzor sponzor)
        {
            _context.Sponzor.Add(sponzor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSponzorAsync(Sponzor sponzor)
        {
            _context.Sponzor.Update(sponzor);
            await _context.SaveChangesAsync();
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
    }
}