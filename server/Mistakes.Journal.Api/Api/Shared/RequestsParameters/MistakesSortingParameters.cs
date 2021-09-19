using Mistakes.Journal.Api.Logic.Mistakes.Extensions;

namespace Mistakes.Journal.Api.Api.Shared.RequestsParameters
{
    public class MistakesSortingParameters
    {
        public MistakeSortingField Field { get; set; } = MistakeSortingField.CreatedAt;

        public bool Desc { get; set; } = true;
    }
}