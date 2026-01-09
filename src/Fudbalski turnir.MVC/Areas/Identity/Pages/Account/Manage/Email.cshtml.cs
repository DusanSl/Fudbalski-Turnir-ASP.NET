// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using FudbalskiTurnir.DAL.Models;
namespace Fudbalski_turnir.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu i nije namenjen
                ///     za direktno korišćenje iz vašeg koda. Ovaj API se može promeniti ili ukloniti u budućim izdanjima.
                /// </summary>
        public string Email { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu.
                /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu.
                /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu.
                /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
                ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu.
                /// </summary>
        public class InputModel
        {
            /// <summary>
                    ///     Ovaj API podržava ASP.NET Core Identity podrazumevanu UI infrastrukturu.
                    /// </summary>
            [Required(ErrorMessage = "Polje '{0}' je obavezno.")]
            [EmailAddress(ErrorMessage = "Polje '{0}' nije ispravna e-mail adresa.")]
            [Display(Name = "Novi e-mail")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                "/Account/ConfirmEmailChange",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, email = Input.NewEmail, code = code },
                protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                Input.NewEmail,
                "Potvrdite vaš e-mail",
                $"Molimo potvrdite vaš nalog <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klikom ovde</a>.");

                StatusMessage = "Link za potvrdu promene e-maila je poslat. Molimo proverite vaš e-mail.";
                return RedirectToPage();
            }

            StatusMessage = "Vaš e-mail je nepromenjen.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nije moguće učitati korisnika sa ID-jem '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = userId, code = code },
            protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
            email,
            "Potvrdite vaš e-mail",
            $"Molimo potvrdite vaš nalog <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klikom ovde</a>.");

            StatusMessage = "E-mail za verifikaciju je poslat. Molimo proverite vaš e-mail.";
            return RedirectToPage();
        }
    }
}