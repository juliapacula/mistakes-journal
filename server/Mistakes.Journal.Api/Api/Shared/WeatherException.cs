using System;

namespace Mistakes.Journal.Api.Api.Shared
{
    public class WeatherException : Exception
    {
        public ErrorMessageType ErrorMessageType { get; }

        public WeatherException(ErrorMessageType errorMessageType, string msg = null)
            : base(msg)
        {
            ErrorMessageType = errorMessageType;
        }
    }
}