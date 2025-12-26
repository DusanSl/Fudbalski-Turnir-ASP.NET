using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using FudbalskiTurnir.ViewModels;   

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var model = new List<UserViewModel>();

        foreach (var u in users)
        {
            model.Add(new UserViewModel
            {
                Id = u.Id,
                Email = u.Email ?? "Bez emaila",
                PhoneNumber = u.PhoneNumber,
                EmailConfirmed = u.EmailConfirmed,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                IsActive = u.LockoutEnd == null || u.LockoutEnd < DateTimeOffset.Now,
                Roles = await _userManager.GetRolesAsync(u)
            });
        }

        return View(model);
    }

    // DETAILS
    public async Task<IActionResult> Details(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var model = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now,
            Roles = await _userManager.GetRolesAsync(user)
        };

        return View(model);
    }
    // GET: Edit
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);

        return View(new UserViewModel
        {
            Id = user.Id,
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now,
            SelectedRole = userRoles.FirstOrDefault() ?? "User"
        });
    }

    // POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id!);
        if (user == null) return NotFound();

        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;

        if (model.IsActive)
        {
            await _userManager.SetLockoutEndDateAsync(user, null); 
        }
        else
        {
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRoleAsync(user, model.SelectedRole);

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded) return RedirectToAction(nameof(Index));

        return View(model);
    }

    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);

        return View(new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Roles = userRoles,
            IsActive = user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.Now
        });
    }

    // DELETE (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        return RedirectToAction(nameof(Index));
    }
}