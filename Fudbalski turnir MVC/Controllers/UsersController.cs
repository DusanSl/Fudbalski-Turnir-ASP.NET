using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.DAL.Models;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsersAsync();
        var model = new List<UserViewModel>();

        foreach (var u in users)
        {
            model.Add(new UserViewModel
            {
                Id = u.Id,
                Email = u.Email ?? "Bez emaila",
                PhoneNumber = u.PhoneNumber,
                EmailConfirmed = u.EmailConfirmed,
                IsActive = u.LockoutEnd == null || u.LockoutEnd < DateTimeOffset.Now,
                Roles = await _userService.GetUserRolesAsync(u)
            });
        }
        return View(model);
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userService.GetUserRolesAsync(user);

        var model = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email ?? "Bez emaila",
            PhoneNumber = user.PhoneNumber ?? "Nema unet broj",
            EmailConfirmed = user.EmailConfirmed,
            IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now,
            Roles = roles,
            SelectedRole = roles.FirstOrDefault() ?? "Nema dodeljenu ulogu"
        };

        return View(model);
    }

    // GET: Users/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        var roles = await _userService.GetUserRolesAsync(user);

        var model = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber,
            IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now,
            SelectedRole = roles.FirstOrDefault() ?? "User"
        };

        return View(model);
    }

    // GET: Users/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        var model = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _userService.DeleteUserAsync(id);
        return RedirectToAction(nameof(Index));
    }
}