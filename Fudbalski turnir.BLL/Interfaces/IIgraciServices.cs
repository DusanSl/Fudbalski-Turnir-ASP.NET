using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IIgraciService
    {
        Task<IEnumerable<Igrac>> GetAllIgraceAsync();
        Task<Igrac?> GetIgracByIdAsync(int id);
        Task CreateIgracAsync(Igrac igrac);
        Task UpdateIgracAsync(Igrac igrac);
        Task DeleteIgracAsync(int id);
    }
}