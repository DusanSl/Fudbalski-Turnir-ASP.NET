using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IKlubService
    {
        Task<IEnumerable<KlubDTO>> GetAllKluboviAsync();
        Task<KlubDTO?> GetKlubByIdAsync(int id);
        Task CreateKlubAsync(KlubDTO klubDto);
        Task UpdateKlubAsync(KlubDTO klubDto);
        Task DeleteKlubAsync(int id);
        Task<IEnumerable<Turnir>> GetAllTurniriAsync();
    }
}