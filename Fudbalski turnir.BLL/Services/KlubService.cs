using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL;
using FudbalskiTurnir.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.BLL.Services
{
    public class KlubService : IKlubService
    {
        private readonly ApplicationDbContext _context;

        public KlubService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Vraća sve klubove sa njihovim turnirima
        public async Task<IEnumerable<Klub>> GetAllKluboviAsync()
        {
            return await _context.Klub
                .Include(k => k.Turniri)
                .ToListAsync();
        }

        // Vraća jedan klub po ID-u
        public async Task<Klub?> GetKlubByIdAsync(int id)
        {
            return await _context.Klub
                .Include(k => k.Turniri)
                .FirstOrDefaultAsync(k => k.KlubID == id);
        }

        // Kreira klub i opciono ga povezuje sa turnirom
        public async Task CreateKlubAsync(Klub klub, int? turnirId)
        {
            if (turnirId.HasValue && turnirId.Value > 0)
            {
                var turnir = await _context.Turnir.FindAsync(turnirId.Value);
                if (turnir != null)
                {
                    klub.Turniri = new List<Turnir> { turnir };
                }
            }
            _context.Klub.Add(klub);
            await _context.SaveChangesAsync();
        }

        // Ažurira klub i osvežava listu turnira
        public async Task UpdateKlubAsync(Klub klub, int? turnirId)
        {
            var existingKlub = await _context.Klub
                .Include(k => k.Turniri)
                .FirstOrDefaultAsync(k => k.KlubID == klub.KlubID);

            if (existingKlub != null)
            {
                // Ažuriraj osnovna polja (Ime, Stadion, itd.)
                _context.Entry(existingKlub).CurrentValues.SetValues(klub);

                // Ažuriraj vezu sa turnirima (Many-to-Many)
                existingKlub.Turniri.Clear();
                if (turnirId.HasValue && turnirId.Value > 0)
                {
                    var turnir = await _context.Turnir.FindAsync(turnirId.Value);
                    if (turnir != null) existingKlub.Turniri.Add(turnir);
                }

                await _context.SaveChangesAsync();
            }
        }

        // Briše klub
        public async Task DeleteKlubAsync(int id)
        {
            var klub = await _context.Klub.FindAsync(id);
            if (klub != null)
            {
                _context.Klub.Remove(klub);
                await _context.SaveChangesAsync();
            }
        }

        // Pomoćna metoda za Dropdown liste u View-u
        public async Task<IEnumerable<Turnir>> GetAllTurniriAsync()
        {
            return await _context.Turnir.ToListAsync();
        }
    }
}