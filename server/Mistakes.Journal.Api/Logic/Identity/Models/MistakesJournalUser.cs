using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Logic.Identity.Models
{
    public class MistakesJournalUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Mistake> Mistakes { get; set; }
    }
}
