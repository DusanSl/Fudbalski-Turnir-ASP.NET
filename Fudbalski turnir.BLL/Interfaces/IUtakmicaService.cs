using FudbalskiTurnir.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IUtakmiceService
    {
        Task<IEnumerable<Utakmica>> GetAllUtakmiceAsync();
        Task<Utakmica?> GetUtakmicaByIdAsync(int id);
        Task CreateUtakmicaAsync(Utakmica utakmica);
        Task UpdateUtakmicaAsync(Utakmica utakmica);
        Task DeleteUtakmicaAsync(int id);
        Task<bool> UtakmicaExistsAsync(int id);
        Task<IEnumerable<Turnir>> GetAllTurniriAsync(); 
        Task<IEnumerable<object>> GetKluboviByTurnirAsync(int turnirId); 
    }
}