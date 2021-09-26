using System;
using Mistakes.Journal.Api.Logic.Identity.Models;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Tip
    {
        public Guid Id { get; }
        public Guid UserId { get; set; }
        public MistakesJournalUser User { get; set; }
        public string Content { get; }
        public Guid? MistakeId { get; }
        public Mistake Mistake { get; }

        public Tip(
            Guid createdBy,
            string content
        )
        {
            UserId = createdBy;
            Content = content;
        }
    }
}