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
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<TwoFactorAuthenticationModel> _logger;

        public TwoFactorAuthenticationModel(
        UserManager<User> userManager, SignInManager<User> signInManager, ILogger<TwoFactorAuthenticationModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        public bool HasAuthenticator { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        public int RecoveryCodesLeft { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        [BindProperty]
        public bool Is2faEnabled { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        public bool IsMachineRemembered { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            await _signInManager.ForgetTwoFactorClientAsync();
            StatusMessage = "Trenutni pregledač je zaboravljen. Kada se sledeći put prijavite sa ovog pregledača, biće vam zatražen 2FA kod.";
            return RedirectToPage();
        }
    }
}