using FudbalskiTurnir.BLL.DTOs;
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

        public async Task<IEnumerable<KlubDTO>> GetAllKluboviAsync()
        {
            return await _context.Klub
                .Include(k => k.Turniri)
                .Select(k => new KlubDTO
                {
                    KlubID = k.KlubID,
                    ImeKluba = k.ImeKluba,
                    GodinaOsnivanja = k.GodinaOsnivanja,
                    RankingTima = k.RankingTima,
                    BrojIgraca = k.BrojIgraca,
                    Stadion = k.Stadion,
                    BrojOsvojenihTitula = k.BrojOsvojenihTitula,
                    NazivTurnira = k.Turniri!.Select(t => t.NazivTurnira).ToList(),
                })
                .ToListAsync(); 
        }

        public async Task<KlubDTO?> GetKlubByIdAsync(int id)
        {
            var k = await _context.Klub
                .Include(k => k.Turniri)
                .FirstOrDefaultAsync(k => k.KlubID == id);

            if (k == null) return null;

            return new KlubDTO
            {
                KlubID = k.KlubID,
                ImeKluba = k.ImeKluba,
                GodinaOsnivanja = k.GodinaOsnivanja,
                RankingTima = k.RankingTima,
                BrojIgraca = k.BrojIgraca,
                Stadion = k.Stadion,
                BrojOsvojenihTitula = k.BrojOsvojenihTitula,
                NazivTurnira = k.Turniri!.Select(t => t.NazivTurnira).ToList(),
                PrimarniTurnirID = k.Turniri!.FirstOrDefault()?.TurnirID
            };
        }

        public async Task CreateKlubAsync(KlubDTO dto)
        {
            var klub = new Klub
            {
                ImeKluba = dto.ImeKluba,
                GodinaOsnivanja = dto.GodinaOsnivanja,
                RankingTima = dto.RankingTima,
                BrojIgraca = dto.BrojIgraca,
                Stadion = dto.Stadion,
                BrojOsvojenihTitula = dto.BrojOsvojenihTitula
            };

            if (dto.PrimarniTurnirID.HasValue && dto.PrimarniTurnirID.Value > 0)
            {
                var turnir = await _context.Turnir.FindAsync(dto.PrimarniTurnirID.Value);
                if (turnir != null)
                {
                    klub.Turniri = new List<Turnir> { turnir };
                }
            }

            _context.Klub.Add(klub);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateKlubAsync(KlubDTO dto)
        {
            var existingKlub = await _context.Klub
                .Include(k => k.Turniri)
                .FirstOrDefaultAsync(k => k.KlubID == dto.KlubID);

            if (existingKlub == null) return;

            existingKlub.ImeKluba = dto.ImeKluba;
            existingKlub.GodinaOsnivanja = dto.GodinaOsnivanja;
            existingKlub.RankingTima = dto.RankingTima;
            existingKlub.BrojIgraca = dto.BrojIgraca;
            existingKlub.Stadion = dto.Stadion;
            existingKlub.BrojOsvojenihTitula = dto.BrojOsvojenihTitula;

            existingKlub.Turniri!.Clear();
            if (dto.PrimarniTurnirID.HasValue && dto.PrimarniTurnirID.Value > 0)
            {
                var turnir = await _context.Turnir.FindAsync(dto.PrimarniTurnirID.Value);
                if (turnir != null)
                {
                    existingKlub.Turniri.Add(turnir);
                }
            }

            _context.Entry(existingKlub).State = EntityState.Modified;
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

        public async Task<IEnumerable<Turnir>> GetAllTurniriAsync()
        {
            return await _context.Turnir.ToListAsync();
        }
    }
}