using FudbalskiTurnir.BLL.Interfaces;
using FudbalskiTurnir.BLL.DTOs;
using FudbalskiTurnir.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskiTurnir.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var usersDto = await _userService.GetAllUsersAsync();

            var model = usersDto.Select(u => new UserViewModel
            {
                Id = u.Id,
                Email = u.Email ?? "Bez emaila",
                PhoneNumber = u.PhoneNumber,
                EmailConfirmed = u.EmailConfirmed,
                IsActive = u.IsActive,
                Roles = u.Roles
            }).ToList();

            return View(model);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var u = await _userService.GetUserByIdAsync(id);
            if (u == null) return NotFound();

            var model = new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber ?? "Nema unet broj",
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                EmailConfirmed = u.EmailConfirmed,
                IsActive = u.IsActive,
                Roles = u.Roles,
                SelectedRole = u.SelectedRole ?? "Nema dodeljenu ulogu"
            };

            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var u = await _userService.GetUserByIdAsync(id);
            if (u == null) return NotFound();

            var model = new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                PhoneNumber = u.PhoneNumber,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                IsActive = u.IsActive,
                SelectedRole = u.SelectedRole ?? "User"
            };

            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var userDto = new UserDTO
                {
                    Id = model.Id,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    IsActive = model.IsActive,
                    SelectedRole = model.SelectedRole
                };

                var success = await _userService.UpdateUserAsync(userDto);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Došlo je do greške prilikom ažuriranja korisnika.");
            }

            return View(model);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var u = await _userService.GetUserByIdAsync(id);
            if (u == null) return NotFound();

            var model = new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                IsActive = u.IsActive,
                SelectedRole = u.SelectedRole,
                Roles = u.Roles 
            };

            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Nije moguće obrisati korisnika.");
        }
    }
}