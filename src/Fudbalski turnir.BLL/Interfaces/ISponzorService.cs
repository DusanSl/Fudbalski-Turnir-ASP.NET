using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface ISponzorService
    {
        Task<IEnumerable<SponzorDTO>> GetAllSponzoriAsync();
        Task<SponzorDTO?> GetSponzorByIdAsync(int id);
        Task CreateSponzorAsync(SponzorDTO dto);
        Task UpdateSponzorAsync(SponzorDTO dto);
        Task DeleteSponzorAsync(int id);
        Task<IEnumerable<Turnir>> GetAllTurniriAsync();
    }
}