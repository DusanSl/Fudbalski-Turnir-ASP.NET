using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IUtakmiceService
    {
        Task<IEnumerable<UtakmicaDTO>> GetAllUtakmiceAsync();
        Task<UtakmicaDTO?> GetUtakmicaByIdAsync(int id);
        Task CreateUtakmicaAsync(UtakmicaDTO utakmicaDto);
        Task UpdateUtakmicaAsync(UtakmicaDTO utakmicaDto);
        Task DeleteUtakmicaAsync(int id);
        Task<bool> UtakmicaExistsAsync(int id);
        Task<IEnumerable<Turnir>> GetAllTurniriAsync();
        Task<IEnumerable<object>> GetKluboviByTurnirAsync(int turnirId); 
        Task<TurnirPregledDTO> GetStandingsModelAsync(int turnirId, string faza);
    }
}