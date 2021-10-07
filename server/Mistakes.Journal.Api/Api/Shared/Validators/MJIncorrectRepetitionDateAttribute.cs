using System;
using System.ComponentModel.DataAnnotations;

namespace Mistakes.Journal.Api.Api.Shared.Validators
{
    public class MJIncorrectRepetitionDateAttribute : ValidationAttribute
    {
        private readonly int days;

        public MJIncorrectRepetitionDateAttribute(int days)
        {
            this.days = days;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is DateTime dateTime))
                return ValidationResult.Success;

            if (dateTime < DateTime.UtcNow - TimeSpan.FromDays(days))
                return new ValidationResult(ErrorMessageType.TooOldRepetition.ToString());
            
            if (dateTime > DateTime.UtcNow + TimeSpan.FromMinutes(5))
                return new ValidationResult(ErrorMessageType.DateInFuture.ToString());

            return ValidationResult.Success;
        }
    }
}