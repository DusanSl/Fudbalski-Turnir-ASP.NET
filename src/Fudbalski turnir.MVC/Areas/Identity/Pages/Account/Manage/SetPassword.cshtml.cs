// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FudbalskiTurnir.DAL.Models;
namespace Fudbalski_turnir.Areas.Identity.Pages.Account.Manage
{
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public SetPasswordModel(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        public class InputModel
        {
            /// <summary>
                        ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                        ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                    /// </summary>
            [Required(ErrorMessage = "Polje {0} je obavezno.")]
            [StringLength(100, ErrorMessage = "{0} mora imati najmanje {2} a najviše {1} karaktera.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nova lozinka")]
            public string NewPassword { get; set; }

            /// <summary>
                        ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                    ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                    /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Potvrdite novu lozinku")]
            [Compare("NewPassword", ErrorMessage = "Nova lozinka i lozinka za potvrdu se ne podudaraju.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToPage("./ChangePassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Vaša lozinka je uspešno postavljena.";

            return RedirectToPage();
        }
    }
}