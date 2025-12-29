using FudbalskiTurnir.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface ISponzorService
    {
        Task<IEnumerable<Sponzor>> GetAllSponzoriAsync();
        Task<Sponzor?> GetSponzorByIdAsync(int id);
        Task CreateSponzorAsync(Sponzor sponzor, int? turnirId);
        Task UpdateSponzorAsync(Sponzor sponzor, int? turnirId);
        Task DeleteSponzorAsync(int id);
        Task<IEnumerable<Turnir>> GetAllTurniriAsync();
    }
}