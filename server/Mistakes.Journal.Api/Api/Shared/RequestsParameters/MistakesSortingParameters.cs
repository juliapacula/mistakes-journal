namespace Mistakes.Journal.Api.Api.Shared.RequestsParameters
{
    public class MistakesSortingParameters : SolvedParameters
    {
        public MistakeSortingField Field { get; set; } = MistakeSortingField.CreatedAt;

        public bool Desc { get; set; } = true;
    }
}