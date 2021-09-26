using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Mistakes.Journal.Api.Logic.Identity.Services
{
    public interface IUserProvider
    {
        Guid GetId();
    }

    public class MistakesJournalUserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _accessor;

        public MistakesJournalUserProvider(
            IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid GetId()
        {
            var identity = _accessor.HttpContext.User.Identity;

            if (identity.IsAuthenticated)
            {
                var name = _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                return Guid.Parse(name);
            }

            return Guid.Empty;
        }
    }
}