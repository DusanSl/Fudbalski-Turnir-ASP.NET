// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace Fudbalski_turnir.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
    ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
    /// </summary>
    public class ShowRecoveryCodesModel : PageModel
    {
        /// <summary>
        ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
        ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
        /// </summary>
        [TempData]
        public string[] RecoveryCodes { get; set; }

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
        public IActionResult OnGet()
        {
            if (RecoveryCodes == null || RecoveryCodes.Length == 0)
            {
                return RedirectToPage("./TwoFactorAuthentication");
            }

            return Page();
        }
    }
}