using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mistakes.Journal.Api.Api.User.WebModels;

namespace Mistakes.Journal.Api.Api.User.Controllers
{
    [ApiController]
    [Route("/api/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly CookieAuthenticationOptions _cookieAuthenticationOptions;

        public ConfigurationController(IOptionsMonitor<CookieAuthenticationOptions> optionsMonitor)
        {
            _cookieAuthenticationOptions = optionsMonitor.Get(IdentityConstants.ApplicationScheme);
        }

        [HttpGet]
        public ActionResult<UserWebModel> GetSelf()
        {
            return Ok(new ConfigurationWebModel
            {
                DeniedPath = _cookieAuthenticationOptions.AccessDeniedPath,
                RegisterPath = "/account/register",
                LoginPath = _cookieAuthenticationOptions.LoginPath,
                LogoutPath = _cookieAuthenticationOptions.LogoutPath,
            });
        }
    }
}