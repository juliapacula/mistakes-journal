using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mistakes.Journal.Api.Api.User.Mappers;
using Mistakes.Journal.Api.Api.User.WebModels;
using Microsoft.EntityFrameworkCore;
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
            [Required(ErrorMessage = "ErrorRequiredEmail")]
            [EmailAddress(ErrorMessage = "ErrorEmailAddress")]
            public string Email { get; set; }

            [Required(ErrorMessage = "ErrorCountryRequired")]
            public string Country { get; set; }

            [Required(ErrorMessage = "ErrorAgeRequired")]
            public int Age { get; set; }

            [Required(ErrorMessage = "ErrorPasswordRequired")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "ErrorPasswordNoMatch")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "ErrorRequiredNewsletter")]
            public bool AgreeToNewsletter { get; set; }
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

            var userAgeRange = UserMapper.AgeToAgeRange(NewUser.Age);
            var availableGroups = Enum.GetValues(typeof(ResearchGroup)).OfType<ResearchGroup>().ToList();
            var allUsers = await _userManager.Users.ToListAsync();
            
            var groupForNewUser = Convert.ToBoolean(Environment.GetEnvironmentVariable("RESEARCH_GROUPS") ?? "false")
                ? SelectGroup(allUsers, availableGroups, NewUser.Country, userAgeRange)
                : ResearchGroup.Default;

            var user = new MistakesJournalUser
            {
                Country = NewUser.Country,
                Age = userAgeRange,
                UserName = NewUser.Email,
                Email = NewUser.Email,
                Language = CultureInfo.CurrentCulture.Name == "pl" ? ApplicationLanguage.PL : ApplicationLanguage.EN,
                Group = groupForNewUser,
                AgreeToNewsletter = NewUser.AgreeToNewsletter,
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

        private static ResearchGroup SelectGroup(List<MistakesJournalUser> allUsers, IEnumerable<ResearchGroup> availableGroups, string country, UserWebModel.AgeRange age)
        {
            // 1. Country and Age
            var finalGroups1 = CalculateGroups(availableGroups, allUsers, u => u.Country == country && u.Age == age);
            if (finalGroups1.Count == 1)
                return finalGroups1[0].Key;

            // 2. Country or Age
            var finalGroups2 = CalculateGroups(finalGroups1.Select(grouping => grouping.Key), allUsers, u => u.Country == country || u.Age == age);
            if (finalGroups2.Count == 1)
                return finalGroups2[0].Key;

            // 3. Groups size
            var finalGroups3 = CalculateGroups(finalGroups2.Select(grouping => grouping.Key), allUsers, u => true);
            return finalGroups3[0].Key;
        }

        private static List<(ResearchGroup Key, int Value)> CalculateGroups(IEnumerable<ResearchGroup> availableGroups, IEnumerable<MistakesJournalUser> allUsers, Func<MistakesJournalUser, bool> predicate)
        {
            var groups = allUsers.Where(predicate).GroupBy(u => u.Group).Where(grouping => availableGroups.Contains(grouping.Key)).OrderBy(grouping => grouping.Count()).ToList();
            var dictionary = availableGroups.Select(g => (Key: g, Value: groups.FirstOrDefault(gr => gr.Key == g)?.Count() ?? 0)).OrderBy(t => t.Value).ToList();
            return dictionary.Where(t => t.Value == dictionary.First().Value).ToList();
        }
    }
}
