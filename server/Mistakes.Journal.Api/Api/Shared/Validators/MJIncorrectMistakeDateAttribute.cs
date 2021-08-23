using System;
using System.ComponentModel.DataAnnotations;

namespace Mistakes.Journal.Api.Api.Shared.Validators
{
    public class MJIncorrectMistakeDateAttribute : ValidationAttribute
    {
        private readonly int days;

        public MJIncorrectMistakeDateAttribute(int days)
        {
            this.days = days;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is DateTime dateTime))
                return ValidationResult.Success;

            if (dateTime < DateTime.Now - TimeSpan.FromDays(days))
                return new ValidationResult(ErrorMessageType.TooOldMistake.ToString());
            
            if (dateTime > DateTime.Now + TimeSpan.FromMinutes(5))
                return new ValidationResult(ErrorMessageType.DateInFuture.ToString());

            return ValidationResult.Success;
        }
    }
}