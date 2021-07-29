namespace Mistakes.Journal.Api.Api.Shared
{
    public class PagingParameters
    {
        public int StartAt { get; set; } = 0;

        public int MaxResults { get; set; } = 25;
    }
}