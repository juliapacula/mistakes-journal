using System.ComponentModel.DataAnnotations;

namespace Mistakes.Journal.Api.Api.Shared.Validators
{
    public class MJRequiredAttribute : RequiredAttribute
    {
        public MJRequiredAttribute()
        {
            AllowEmptyStrings = false;
            ErrorMessage = ErrorMessageType.Required.ToString();
        }
    }
}