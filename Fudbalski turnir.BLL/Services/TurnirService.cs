using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.BLL.Services
{
    public class TurnirService : ITurnirService
    {
        private readonly ApplicationDbContext _context;

        public TurnirService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Turnir>> GetAllTurniriAsync()
        {
            return await _context.Turnir.ToListAsync();
        }

        public async Task<Turnir?> GetTurnirByIdAsync(int id)
        {
            return await _context.Turnir.FirstOrDefaultAsync(t => t.TurnirID == id);
        }

        public async Task CreateTurnirAsync(Turnir turnir)
        {
            _context.Turnir.Add(turnir);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTurnirAsync(Turnir turnir)
        {
            _context.Turnir.Update(turnir);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTurnirAsync(int id)
        {
            var turnir = await _context.Turnir.FindAsync(id);
            if (turnir != null)
            {
                _context.Turnir.Remove(turnir);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Utakmica>> GetAllUtakmiceAsync()
        {
            return await _context.Utakmica
                .Include(u => u.Turnir) 
                .OrderByDescending(u => u.Datum)
                .ToListAsync();
        }

        public async Task<bool> TurnirExistsAsync(int id)
        {
            return await _context.Turnir.AnyAsync(e => e.TurnirID == id);
        }
    }
}