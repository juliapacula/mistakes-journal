using System;
using Mistakes.Journal.Api.Logic.Shared.Models;

namespace Mistakes.Journal.Api.Api.User.WebModels
{
    public class UserWebModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ResearchGroup Group { get; set; }
        public ApplicationLanguage Language { get; set; }
    }
}