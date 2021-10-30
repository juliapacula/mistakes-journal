using System;

namespace Mistakes.Journal.Api.Api.Shared.RequestsParameters
{
    public class MistakesListFilters
    {
        public bool IncludeSolved { get; set; } = false;

        public bool IncludeUnsolved { get; set; } = true;
        public Guid? LabelId { get; set; } = null;
    }
}