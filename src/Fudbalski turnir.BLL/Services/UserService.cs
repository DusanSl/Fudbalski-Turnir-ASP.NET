using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IdentityResult> CreateUserAsync(UserDTO userDto, string password)
    {
        var user = new User
        {
            UserName = userDto.Email,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            EmailConfirmed = userDto.EmailConfirmed,
            PhoneNumberConfirmed = userDto.PhoneNumberConfirmed
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded && !string.IsNullOrEmpty(userDto.SelectedRole))
        {
            await _userManager.AddToRoleAsync(user, userDto.SelectedRole);
        }

        return result;
    }
    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = new List<UserDTO>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(new UserDTO
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                EmailConfirmed = user.EmailConfirmed,
                IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now,
                Roles = roles
            });
        }
        return userDtos;
    }

    public async Task<UserDTO?> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return null;

        var roles = await _userManager.GetRolesAsync(user);
        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now,
            Roles = roles,
            SelectedRole = roles.FirstOrDefault()
        };
    }

    public async Task<bool> UpdateUserAsync(UserDTO userDto)
    {
        var existingUser = await _userManager.FindByIdAsync(userDto.Id);
        if (existingUser == null) return false;

        existingUser.Email = userDto.Email;
        existingUser.UserName = userDto.Email;
        existingUser.PhoneNumber = userDto.PhoneNumber;
        existingUser.EmailConfirmed = userDto.EmailConfirmed; // Čuvanje potvrde
        existingUser.PhoneNumberConfirmed = userDto.PhoneNumberConfirmed; // Čuvanje potvrde

        if (userDto.IsActive)
            await _userManager.SetLockoutEndDateAsync(existingUser, null);
        else
            await _userManager.SetLockoutEndDateAsync(existingUser, DateTimeOffset.MaxValue);

        var currentRoles = await _userManager.GetRolesAsync(existingUser);
        await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);

        if (!string.IsNullOrEmpty(userDto.SelectedRole))
        {
            await _userManager.AddToRoleAsync(existingUser, userDto.SelectedRole);
        }

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
    public async Task<UserDTO?> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) return null;

        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now,
        };
    }
}