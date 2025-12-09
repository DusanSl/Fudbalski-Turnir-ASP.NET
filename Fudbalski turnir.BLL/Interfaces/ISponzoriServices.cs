using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface ISponzoriService
    {
        Task<IEnumerable<Sponzor>> GetAllSponzoriAsync();
        Task<Sponzor?> GetSponzorByIdAsync(int id);
        Task CreateSponzorAsync(Sponzor sponzor);
        Task UpdateSponzorAsync(Sponzor sponzor);
        Task DeleteSponzorAsync(int id);
    }
}