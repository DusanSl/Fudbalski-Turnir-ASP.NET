using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FudbalskiTurnir.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> UpdateUserAsync(User user, string selectedRole, bool isActive)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null) return false;

            existingUser.Email = user.Email;
            existingUser.UserName = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;

            if (isActive)
                await _userManager.SetLockoutEndDateAsync(existingUser, null);
            else
                await _userManager.SetLockoutEndDateAsync(existingUser, DateTimeOffset.MaxValue);

            var currentRoles = await _userManager.GetRolesAsync(existingUser);
            await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
            await _userManager.AddToRoleAsync(existingUser, selectedRole);

            var result = await _userManager.UpdateAsync(existingUser);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}