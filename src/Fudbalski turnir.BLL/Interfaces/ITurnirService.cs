using FudbalskiTurnir.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface ITurnirService
    {
        Task<IEnumerable<TurnirDTO>> GetAllTurniriAsync();
        Task<TurnirDTO?> GetTurnirByIdAsync(int id);
        Task CreateTurnirAsync(TurnirDTO turnirDto);
        Task UpdateTurnirAsync(TurnirDTO turnirDto);
        Task DeleteTurnirAsync(int id);
        Task<bool> TurnirExistsAsync(int id);
    }
}