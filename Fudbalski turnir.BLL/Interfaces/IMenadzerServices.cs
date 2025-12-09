using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IMenadzerService
    {
        Task<IEnumerable<Menadzer>> GetAllMenadzerAsync();
        Task<Menadzer?> GetMenadzerByIdAsync(int id);
        Task CreateMenadzerAsync(Menadzer menadzer);
        Task UpdateMenadzerAsync(Menadzer menadzer);
        Task DeleteMenadzerAsync(int id);
    }
}