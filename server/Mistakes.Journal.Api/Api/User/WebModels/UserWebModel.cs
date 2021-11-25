using Mistakes.Journal.Api.Logic.Shared.Models;

namespace Mistakes.Journal.Api.Api.User.WebModels
{
    public class UserWebModel
    {
        public string Country { get; set; }
        public AgeRange Age { get; set; }
        public string Email { get; set; }
        public ResearchGroup Group { get; set; }
        public ApplicationLanguage Language { get; set; }
        public bool WatchedTutorial { get; set; }

        public enum AgeRange
        {
            RangeTo18,
            Range19To25,
            Range26To35,
            Range36To55,
            Range56AndMore,
        }
    }
}