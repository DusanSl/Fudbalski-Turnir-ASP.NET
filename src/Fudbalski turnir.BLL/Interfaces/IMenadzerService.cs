using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IMenadzerService
    {
        Task<IEnumerable<MenadzerDTO>> GetAllMenadzerAsync();
        Task<MenadzerDTO?> GetMenadzerByIdAsync(int id);
        Task CreateMenadzerAsync(MenadzerDTO dto);
        Task UpdateMenadzerAsync(MenadzerDTO dto);
        Task DeleteMenadzerAsync(int id);
        Task<IEnumerable<Klub>> GetAllKluboviAsync();
    }
}