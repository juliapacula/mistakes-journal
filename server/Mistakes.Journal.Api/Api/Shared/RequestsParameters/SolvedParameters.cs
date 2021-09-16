namespace Mistakes.Journal.Api.Api.Shared.RequestsParameters
{
    public class SolvedParameters
    {
        public bool IncludeSolved { get; set; } = false;

        public bool IncludeUnsolved { get; set; } = true;
    }
}