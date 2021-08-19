namespace Mistakes.Journal.Api.Api.Shared
{
    public enum ErrorMessageType
    {
        Required = 1,
        TooLong = 2,
        TooOldMistake = 3,
        DateInFuture = 4,
        LastRepetitionIsTooOldToWithdraw = 5,
        NotUnique = 6
    }
}