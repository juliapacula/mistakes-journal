namespace Mistakes.Journal.Api.Api.Shared.RequestsParameters
{
    public class MistakesSortingParameters : SolvedParameters
    {
        public MistakeSortingField Field { get; set; } = MistakeSortingField.FirstOccurence;

        public bool Desc { get; set; } = false;
    }
}