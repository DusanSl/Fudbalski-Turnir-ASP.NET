using FudbalskiTurnir.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface ITurnirService
    {
        Task<IEnumerable<Turnir>> GetAllTurniriAsync();
        Task<Turnir?> GetTurnirByIdAsync(int id);
        Task CreateTurnirAsync(Turnir turnir);
        Task UpdateTurnirAsync(Turnir turnir);
        Task DeleteTurnirAsync(int id);
        Task<bool> TurnirExistsAsync(int id);
        Task<IEnumerable<Utakmica>> GetAllUtakmiceAsync();
    }
}