using System;

namespace Mistakes.Journal.Api.Api.Shared
{
    public class MJException : Exception
    {
        public ErrorMessageType ErrorMessageType { get; }

        public MJException(ErrorMessageType errorMessageType, string msg = null)
            : base(msg)
        {
            ErrorMessageType = errorMessageType;
        }
    }
}