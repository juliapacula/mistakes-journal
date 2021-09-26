namespace Mistakes.Journal.Api.Api.Shared
{
    public enum ErrorMessageType
    {
        UnknownError = 0,
        Required = 1,
        TooLong = 2,
        TooOldRepetition = 3,
        DateInFuture = 4,
        NotUnique = 5,
        DoesNotMatchPattern = 6,
        LabelDoesNotExist = 7,
        CannotBeSolved = 8,
        MistakeIsAlreadySolved = 9,
        WrongLongitude = 10,
        WrongLatitude = 11,
        IncompleteCoordinates = 12,
        SunriseSunsetError = 13,
        NotLogged = 14
    }
}