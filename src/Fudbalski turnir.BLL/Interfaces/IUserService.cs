using FudbalskiTurnir.BLL.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FudbalskiTurnir.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(string id); 
        Task<IdentityResult> CreateUserAsync(UserDTO userDto, string password);
        Task<bool> UpdateUserAsync(UserDTO userDto);
        Task<bool> DeleteUserAsync(string id);
    }
}