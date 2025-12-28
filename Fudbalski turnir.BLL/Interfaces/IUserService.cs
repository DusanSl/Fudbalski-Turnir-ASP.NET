using FudbalskiTurnir.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<bool> UpdateUserAsync(User user, string selectedRole, bool isActive);
        Task<bool> DeleteUserAsync(string id);
        Task<IEnumerable<string>> GetUserRolesAsync(User user);
    }
}