using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Mistakes.Journal.Api.Logic.Identity.Models;
using Mistakes.Journal.Api.Resources;

namespace Mistakes.Journal.Api.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<MistakesJournalUser> _signInManager;
        private readonly IStringLocalizer _accountLocalizer;

        public LoginModel(SignInManager<MistakesJournalUser> signInManager, IStringLocalizerFactory factory)
        {
            _signInManager = signInManager;
            var type = typeof(AccountResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _accountLocalizer = factory.Create("AccountResource", assemblyName.Name);
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "ErrorRequiredEmail")]
            [EmailAddress(ErrorMessage = "ErrorEmailAddress")]
            public string Email { get; set; }

            [Required(ErrorMessage = "ErrorPasswordRequired")]
            [DataType(DataType.Password, ErrorMessage = "ErrorPasswordIncorrect")]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid) return Page();

            var result = await _signInManager.PasswordSignInAsync(
                Input.Email,
                Input.Password,
                Input.RememberMe,
                false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, _accountLocalizer["ErrorUserNotFound"]);
            ReturnUrl = returnUrl;

            return Page();
        }
    }
}
