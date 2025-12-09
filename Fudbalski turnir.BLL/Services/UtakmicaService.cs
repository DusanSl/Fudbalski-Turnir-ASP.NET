using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.BLL.Services
{
    public class UtakmicaService : IUtakmiceService
    {
        private readonly ApplicationDbContext _context;

        public UtakmicaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Utakmica>> GetAllUtakmiceAsync()
        {
            return await _context.Utakmica.ToListAsync();
        }

        public async Task<Utakmica?> GetUtakmicaByIdAsync(int id)
        {
            return await _context.Utakmica.FindAsync(id);
        }

        public async Task CreateUtakmicaAsync(Utakmica utakmica)
        {
            _context.Utakmica.Add(utakmica);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUtakmicaAsync(Utakmica utakmica)
        {
            _context.Utakmica.Update(utakmica);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUtakmicaAsync(int id)
        {
            var utakmica = await _context.Utakmica.FindAsync(id);
            if (utakmica != null)
            {
                _context.Utakmica.Remove(utakmica);
                await _context.SaveChangesAsync();
            }
        }
    }
}