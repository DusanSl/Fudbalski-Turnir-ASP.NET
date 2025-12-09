using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.BLL.Services
{
    public class MenadzerService : IMenadzerService
    {
        private readonly ApplicationDbContext _context;

        public MenadzerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menadzer>> GetAllMenadzerAsync()
        {
            return await _context.Menadzer.ToListAsync();
        }

        public async Task<Menadzer?> GetMenadzerByIdAsync(int id)
        {
            return await _context.Menadzer.FindAsync(id);
        }

        public async Task CreateMenadzerAsync(Menadzer menadzer)
        {
            _context.Menadzer.Add(menadzer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenadzerAsync(Menadzer menadzer)
        {
            _context.Menadzer.Update(menadzer);
            await _context.SaveChangesAsync();
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
    }
}