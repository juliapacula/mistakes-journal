using Mistakes.Journal.Api.Api.User.WebModels;
using Mistakes.Journal.Api.Logic.Identity.Models;

namespace Mistakes.Journal.Api.Api.User.Mappers
{
    public static class UserMapper
    {
        public static UserWebModel ToWebModel(this MistakesJournalUser user)
        {
            return new UserWebModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Group = user.Group,
                Language = user.Language,
            };
        }
    }
}