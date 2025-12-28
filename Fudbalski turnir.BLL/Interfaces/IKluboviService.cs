using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IKluboviService
    {
        Task<IEnumerable<Klub>> GetAllKluboviAsync();
        Task<Klub?> GetKlubByIdAsync(int id);
        Task CreateKlubAsync(Klub klub, int? turnirId);
        Task UpdateKlubAsync(Klub klub, int? turnirId);
        Task DeleteKlubAsync(int id);
        Task<IEnumerable<Turnir>> GetAllTurniriAsync();
    }
}