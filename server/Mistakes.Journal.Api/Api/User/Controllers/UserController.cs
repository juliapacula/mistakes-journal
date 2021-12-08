using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mistakes.Journal.Api.Api.User.Mappers;
using Mistakes.Journal.Api.Api.User.WebModels;
using Mistakes.Journal.Api.Logic.Identity.Models;

namespace Mistakes.Journal.Api.Api.User.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/self")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<MistakesJournalUser> _userManager;
        private readonly SignInManager<MistakesJournalUser> _signInManager;

        public UserController(
            UserManager<MistakesJournalUser> userManager,
            SignInManager<MistakesJournalUser> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<ActionResult<UserWebModel>> GetSelf()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (!user.LastLoggingIn.HasValue || !user.LastLoggingIn.Value.IsToday())
            {
                user.LastLoggingIn = DateTime.Now;
                user.LoggedDaysCount++;
                await _userManager.UpdateAsync(user);
            }

            return Ok(user.ToWebModel());
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPut("language")]
        public async Task<ActionResult> ChangeLanguage(UserLanguageWebModel language)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            user.Language = language.Language;

            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpPut("tutorial")]
        public async Task<ActionResult> ChangeWhetherWatchedTutorial(UserWatchedTutorialWebModel watchedTutorialWebModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            user.WatchedTutorial = watchedTutorialWebModel.WatchedTutorial;

            await _userManager.UpdateAsync(user);

            return Ok();
        }
    }
}