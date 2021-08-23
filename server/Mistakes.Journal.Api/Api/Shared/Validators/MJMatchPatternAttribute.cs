using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Mistakes.Journal.Api.Api.Shared.Validators
{
    public class MJMatchPatternAttribute : ValidationAttribute
    {
        private readonly string regexPattern;

        public MJMatchPatternAttribute(string regexPattern)
        {
            this.regexPattern = regexPattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (!(value is string str) || !Regex.IsMatch(str, regexPattern))
                return new ValidationResult(ErrorMessageType.DoesNotMatchPattern.ToString());

            return ValidationResult.Success;
        }
    }
}