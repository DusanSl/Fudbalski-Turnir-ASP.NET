using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Menadzer
                .Include(m => m.Klub)
                .ToListAsync();
        }

        public async Task<Menadzer?> GetMenadzerByIdAsync(int id)
        {
            return await _context.Menadzer
                .Include(m => m.Klub)
                .FirstOrDefaultAsync(m => m.OsobaID == id);
        }

        public async Task CreateMenadzerAsync(Menadzer menadzer)
        {
            _context.Menadzer.Add(menadzer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenadzerAsync(Menadzer menadzer)
        {
            var existing = await _context.Menadzer.FindAsync(menadzer.OsobaID);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(menadzer);
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

        public async Task<IEnumerable<Klub>> GetAllKluboviAsync()
        {
            return await _context.Klub.ToListAsync();
        }
    }
}