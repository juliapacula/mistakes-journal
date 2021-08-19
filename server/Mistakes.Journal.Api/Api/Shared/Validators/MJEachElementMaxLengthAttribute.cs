using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mistakes.Journal.Api.Api.Shared.Validators
{
    public class MJEachElementMaxLengthAttribute : ValidationAttribute
    {
        public MJEachElementMaxLengthAttribute(int length)
        {
            Length = length;
            ErrorMessage = ErrorMessageType.TooLong.ToString();
        }

        public int Length { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var list = (IEnumerable<string>) value;

            if (list.All(item => item.Length <= Length))
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
    }
}