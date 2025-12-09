using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.BLL.Services
{
    public class KluboviService : IKluboviService
    {
        private readonly ApplicationDbContext _context;

        public KluboviService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Klub>> GetAllKluboviAsync()
        {
            return await _context.Klub.ToListAsync();
        }

        public async Task<Klub?> GetKlubByIdAsync(int id)
        {
            return await _context.Klub.FindAsync(id);
        }

        public async Task CreateKlubAsync(Klub klub)
        {
            _context.Klub.Add(klub);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateKlubAsync(Klub klub)
        {
            _context.Klub.Update(klub);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteKlubAsync(int id)
        {
            var klub = await _context.Klub.FindAsync(id);
            if (klub != null)
            {
                _context.Klub.Remove(klub);
                await _context.SaveChangesAsync();
            }
        }
    }
}