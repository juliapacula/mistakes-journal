using System;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Repetition
    {
        public Guid Id { get; }
        public DateTime OccuredAt { get; set; }
        public Guid MistakeId { get; }
        public Mistake Mistake { get; }

        public Repetition(DateTime occuredAt)
        {
            OccuredAt = occuredAt;
        }
    }
}