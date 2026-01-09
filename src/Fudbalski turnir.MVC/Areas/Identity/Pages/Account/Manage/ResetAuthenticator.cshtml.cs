// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FudbalskiTurnir.DAL.Models;
namespace Fudbalski_turnir.Areas.Identity.Pages.Account.Manage
{
    public class ResetAuthenticatorModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<ResetAuthenticatorModel> _logger;

        public ResetAuthenticatorModel(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ILogger<ResetAuthenticatorModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            _logger.LogInformation("Korisnik sa ID-jem '{UserId}' je resetovao ključ svoje aplikacije za autentifikaciju.", user.Id);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Ključ vaše aplikacije za autentifikaciju je resetovan, moraćete da konfigurišete svoju aplikaciju pomoću novog ključa.";

            return RedirectToPage("./EnableAuthenticator");
        }
    }
}