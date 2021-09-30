using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Mappers
{
    public static class RepetitionMapper
    {
        public static RepetitionWebModel ToWebModel(this Repetition repetition)
        {
            return new RepetitionWebModel
            {
                Id = repetition.Id,
                OccurredAt = repetition.OccurredAt,
            };
        }
    }
}
