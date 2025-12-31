using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.DTOs;
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

        public async Task<IEnumerable<TurnirDTO>> GetAllTurniriAsync()
        {
            return await _context.Turnir
                .Select(t => new TurnirDTO
                {
                    TurnirID = t.TurnirID,
                    NazivTurnira = t.NazivTurnira,
                    Lokacija = t.Lokacija,
                    DatumPocetka = t.DatumPocetka,
                    DatumZavrsetka = t.DatumZavrsetka,
                    TipTurnira = t.TipTurnira,
                    BrojUtakmica = t.Utakmice.Count()
                }).ToListAsync();
        }

        public async Task<TurnirDTO?> GetTurnirByIdAsync(int id)
        {
            var t = await _context.Turnir
                .Include(t => t.Utakmice)
                .FirstOrDefaultAsync(m => m.TurnirID == id);

            if (t == null) return null;

            return new TurnirDTO
            {
                TurnirID = t.TurnirID,
                NazivTurnira = t.NazivTurnira,
                Lokacija = t.Lokacija,
                DatumPocetka = t.DatumPocetka,
                DatumZavrsetka = t.DatumZavrsetka,
                TipTurnira = t.TipTurnira,
                BrojUtakmica = t.Utakmice.Count()
            };
        }

        public async Task CreateTurnirAsync(TurnirDTO dto)
        {
            var turnir = new Turnir
            {
                NazivTurnira = dto.NazivTurnira,
                Lokacija = dto.Lokacija,
                DatumPocetka = dto.DatumPocetka,
                DatumZavrsetka = dto.DatumZavrsetka,
                TipTurnira = dto.TipTurnira
            };
            _context.Turnir.Add(turnir);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTurnirAsync(TurnirDTO dto)
        {
            var turnir = await _context.Turnir.FindAsync(dto.TurnirID);
            if (turnir != null)
            {
                turnir.NazivTurnira = dto.NazivTurnira;
                turnir.Lokacija = dto.Lokacija;
                turnir.DatumPocetka = dto.DatumPocetka;
                turnir.DatumZavrsetka = dto.DatumZavrsetka;
                turnir.TipTurnira = dto.TipTurnira;

                _context.Turnir.Update(turnir);
                await _context.SaveChangesAsync();
            }
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

        public async Task<bool> TurnirExistsAsync(int id)
        {
            return await _context.Turnir.AnyAsync(e => e.TurnirID == id);
        }
    }
}