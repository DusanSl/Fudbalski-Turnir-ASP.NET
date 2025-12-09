using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface ITurnirService
    {
        Task<IEnumerable<Turnir>> GetAllTurniriAsync();
        Task<Turnir?> GetTurnirByIdAsync(int id);
        Task CreateTurnirAsync(Turnir turnir);
        Task UpdateTurnirAsync(Turnir turnir);
        Task DeleteTurnirAsync(int id);
    }
}