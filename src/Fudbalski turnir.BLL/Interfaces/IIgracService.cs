using Fudbalski_turnir.BLL.DTO;
using FudbalskiTurnir.DAL.Models;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IIgracService
    {
        Task<IEnumerable<IgracDTO>> GetAllIgraceAsync();
        Task<IgracDTO?> GetIgracByIdAsync(int id);
        Task CreateIgracAsync(IgracDTO igracDto); 
        Task UpdateIgracAsync(IgracDTO igracDto); 
        Task DeleteIgracAsync(int id);
        Task<IEnumerable<Klub>> GetAllKluboviAsync(); 
        Task<bool> IsBrojDresaDostupanAsync(int klubId, int brojDresa, int? trenutniIgracId = null);
    }
}