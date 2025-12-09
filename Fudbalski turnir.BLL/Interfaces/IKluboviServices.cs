using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IKluboviService
    {
        Task<IEnumerable<Klub>> GetAllKluboviAsync();
        Task<Klub?> GetKlubByIdAsync(int id);
        Task CreateKlubAsync(Klub klub);
        Task UpdateKlubAsync(Klub klub);
        Task DeleteKlubAsync(int id);
    }
}