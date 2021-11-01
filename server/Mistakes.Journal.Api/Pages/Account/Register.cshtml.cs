using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mistakes.Journal.Api.Logic.Identity;
using Mistakes.Journal.Api.Logic.Identity.Models;
using Mistakes.Journal.Api.Logic.Shared.Models;

namespace Mistakes.Journal.Api.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MistakesJournalUser> _signInManager;
        private readonly UserManager<MistakesJournalUser> _userManager;

        public RegisterModel(
            UserManager<MistakesJournalUser> userManager,
            SignInManager<MistakesJournalUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty] public NewUserModel NewUser { get; set; }

        public string ReturnUrl { get; set; }

        public class NewUserModel
        {
            [Required]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var availableGroups = Enum.GetValues(typeof(ResearchGroup)).OfType<ResearchGroup>().ToList();
            var allUsers = await _userManager.Users.ToListAsync();

            var groupForNewUser = ResearchGroup.Default;
            // Will be uncommented when experiment starts.
            // var groupForNewUser = availableGroups.GroupJoin(
            //         allUsers,
            //         group => group,
            //         u => u.Group,
            //         (group, groupUsers) => new
            //         {
            //             Group = group,
            //             NumberOfUsers = groupUsers.Count()
            //         })
            //     .OrderBy(g => g.NumberOfUsers)
            //     .Select(g => g.Group)
            //     .First();

            var user = new MistakesJournalUser
            {
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                UserName = NewUser.Email,
                Email = NewUser.Email,
                Language = ApplicationLanguage.EN,
                Group = groupForNewUser
            };
            var result = await _userManager.CreateAsync(user, NewUser.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}