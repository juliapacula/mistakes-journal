using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Mappers
{
    public static class AdditionalQuestionsMapper
    {
        public static AdditionalQuestionsWebModel ToWebModel(this AdditionalQuestions additionalQuestions)
        {
            return additionalQuestions is null ? null : new AdditionalQuestionsWebModel
            {
                Consequences = additionalQuestions.Consequences,
                CanIFixIt = additionalQuestions.CanIFixIt,
                WhatDidILearn = additionalQuestions.WhatDidILearn,
                WhatCanIDoBetter = additionalQuestions.WhatCanIDoBetter,
                OnlyResponsible = additionalQuestions.OnlyResponsible
            };
        }
    }
}