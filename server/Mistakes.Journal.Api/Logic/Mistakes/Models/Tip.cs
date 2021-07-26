using System;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Tip
    {
        public Guid Id { get; }
        public string Content { get; }
        public Guid MistakeId { get; }
        public Mistake Mistake { get; }

        public Tip(
            string content
        )
        {
            Content = content;
        }
    }
}