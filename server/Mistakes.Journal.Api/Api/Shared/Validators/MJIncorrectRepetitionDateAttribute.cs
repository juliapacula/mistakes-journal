using System;
using System.ComponentModel.DataAnnotations;
using Mistakes.Journal.Api.Logic.Shared.Extensions;

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

            if (dateTime.ToSeconds() < (DateTime.UtcNow - TimeSpan.FromDays(days)).ToSeconds())
                return new ValidationResult(ErrorMessageType.TooOldRepetition.ToString());
            
            if (dateTime.ToSeconds().IsAfter(DateTime.UtcNow.ToSeconds()))
                return new ValidationResult(ErrorMessageType.DateInFuture.ToString());

            return ValidationResult.Success;
        }
    }
}