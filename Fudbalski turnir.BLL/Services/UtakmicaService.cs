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
            return await _context.Utakmica.Include(u => u.Turnir).ToListAsync();
        }

        public async Task<Utakmica?> GetUtakmicaByIdAsync(int id)
        {
            return await _context.Utakmica
                .Include(u => u.Turnir)
                .FirstOrDefaultAsync(u => u.UtakmicaID == id);
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

        public async Task<bool> UtakmicaExistsAsync(int id)
        {
            return await _context.Utakmica.AnyAsync(e => e.UtakmicaID == id);
        }

        public async Task<IEnumerable<Turnir>> GetAllTurniriAsync()
        {
            return await _context.Turnir.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetKluboviByTurnirAsync(int turnirId)
        {
            return await _context.Klub
                .Where(k => k.Turniri.Any(t => t.TurnirID == turnirId))
                .Select(k => new { k.KlubID, k.ImeKluba })
                .ToListAsync();
        }
    }
}