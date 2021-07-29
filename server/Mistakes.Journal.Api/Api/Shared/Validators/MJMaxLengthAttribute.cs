using System.ComponentModel.DataAnnotations;

namespace Mistakes.Journal.Api.Api.Shared.Validators
{
    public class MJMaxLengthAttribute : MaxLengthAttribute
    {
        public MJMaxLengthAttribute(int maxLength)
            : base(maxLength)
        {
            ErrorMessage = ErrorMessageType.TooLong.ToString();
        }
    }
}