using System;
using Mistakes.Journal.Api.Logic.Shared.Extensions;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Repetition
    {
        public Guid Id { get; }
        public DateTime OccurredAt { get; set; }
        public Guid MistakeId { get; }
        public Mistake Mistake { get; }

        public Repetition(DateTime occurredAt)
        {
            OccurredAt = occurredAt.ToSeconds();
        }
    }
}