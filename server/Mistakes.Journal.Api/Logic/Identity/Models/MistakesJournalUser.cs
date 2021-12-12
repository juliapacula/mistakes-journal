using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Mistakes.Journal.Api.Api.User.WebModels;
using Mistakes.Journal.Api.Logic.Mistakes.Models;
using Mistakes.Journal.Api.Logic.Shared.Models;

namespace Mistakes.Journal.Api.Logic.Identity.Models
{
    public class MistakesJournalUser : IdentityUser<Guid>
    {
        public string Country { get; set; }
        public UserWebModel.AgeRange Age { get; set; }
        public ICollection<Mistake> Mistakes { get; set; }
        public ICollection<Label> Labels { get; set; }
        public ICollection<Tip> Tips { get; set; }
        public ApplicationLanguage Language { get; set; }
        public ResearchGroup Group { get; set; }
        public bool WatchedTutorial { get; set; }
        public DateTime? LastLoggingIn { get; set; }
        public int LoggedDaysCount { get; set; }
        public bool AgreeToNewsletter { get; set; }
    }
}
