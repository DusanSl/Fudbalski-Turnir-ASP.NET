using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.BLL.Services
{
    public class IgracService : IIgracService
    {
        private readonly ApplicationDbContext _context;

        public IgracService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Igrac>> GetAllIgraceAsync()
        {
            return await _context.Igrac.Include(i => i.Klub).ToListAsync();
        }

        public async Task<Igrac?> GetIgracByIdAsync(int id)
        {
            return await _context.Igrac.Include(i => i.Klub)
                .FirstOrDefaultAsync(m => m.OsobaID == id);
        }

        public async Task<IEnumerable<Klub>> GetAllKluboviAsync()
        {
            return await _context.Klub.ToListAsync();
        }

        public async Task CreateIgracAsync(Igrac igrac)
        {
            _context.Igrac.Add(igrac);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIgracAsync(Igrac igrac)
        {
            _context.Igrac.Update(igrac);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIgracAsync(int id)
        {
            var igrac = await _context.Igrac.FindAsync(id);
            if (igrac != null)
            {
                _context.Igrac.Remove(igrac);
                await _context.SaveChangesAsync();
            }
        }
    }
}