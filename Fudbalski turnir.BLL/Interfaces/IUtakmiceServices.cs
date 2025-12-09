using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IUtakmiceService
    {
        Task<IEnumerable<Utakmica>> GetAllUtakmiceAsync();
        Task<Utakmica?> GetUtakmicaByIdAsync(int id);
        Task CreateUtakmicaAsync(Utakmica utakmica);
        Task UpdateUtakmicaAsync(Utakmica utakmica);
        Task DeleteUtakmicaAsync(int id);
    }
}