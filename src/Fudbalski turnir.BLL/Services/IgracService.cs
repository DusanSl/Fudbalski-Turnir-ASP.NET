using Fudbalski_turnir.BLL.DTO;
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

        public async Task<IEnumerable<IgracDTO>> GetAllIgraceAsync()
        {
            return await _context.Igrac
                .Include(i => i.Klub)
                .Select(i => new IgracDTO
                {
                    OsobaID = i.OsobaID,
                    Ime = i.Ime,
                    Prezime = i.Prezime,
                    Pozicija = i.Pozicija,
                    BrojDresa = i.BrojDresa,
                    DatumRodjenja = i.DatumRodjenja,
                    Nacionalnost = i.Nacionalnost,
                    KlubID = i.KlubID,
                    ImeKluba = i.Klub != null ? i.Klub.ImeKluba : "Bez kluba"
                }).ToListAsync();
        }

        public async Task<IgracDTO?> GetIgracByIdAsync(int id)
        {
            var i = await _context.Igrac
                .Include(i => i.Klub)
                .FirstOrDefaultAsync(m => m.OsobaID == id);

            if (i == null) return null;

            return new IgracDTO
            {
                OsobaID = i.OsobaID,
                Ime = i.Ime,
                Prezime = i.Prezime,
                Pozicija = i.Pozicija,
                BrojDresa = i.BrojDresa,
                DatumRodjenja = i.DatumRodjenja,
                Nacionalnost = i.Nacionalnost,
                KlubID = i.KlubID,
                ImeKluba = i.Klub?.ImeKluba
            };
        }

        public async Task CreateIgracAsync(IgracDTO dto)
        {
            var igrac = new Igrac
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Pozicija = dto.Pozicija,
                BrojDresa = dto.BrojDresa,
                DatumRodjenja = dto.DatumRodjenja,
                Nacionalnost = dto.Nacionalnost,
                KlubID = dto.KlubID
            };
            _context.Igrac.Add(igrac);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIgracAsync(IgracDTO dto)
        {
            var igrac = await _context.Igrac.FindAsync(dto.OsobaID);
            if (igrac != null)
            {
                igrac.Ime = dto.Ime;
                igrac.Prezime = dto.Prezime;
                igrac.Pozicija = dto.Pozicija;
                igrac.BrojDresa = dto.BrojDresa;
                igrac.DatumRodjenja = dto.DatumRodjenja;
                igrac.Nacionalnost = dto.Nacionalnost;
                igrac.KlubID = dto.KlubID;

                _context.Igrac.Update(igrac);
                await _context.SaveChangesAsync();
            }
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

        public async Task<IEnumerable<Klub>> GetAllKluboviAsync()
        {
            return await _context.Klub.ToListAsync();
        }
        public async Task<bool> IsBrojDresaDostupanAsync(int klubId, int brojDresa, 
            int? trenutniIgracId = null)
        {
            bool zauzeto = await _context.Igrac.AnyAsync(i => i.KlubID == klubId
                                                         && i.BrojDresa == brojDresa
                                                         && i.OsobaID != trenutniIgracId);

            return !zauzeto; 
        }
    }
}