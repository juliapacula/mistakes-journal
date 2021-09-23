using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Mappers
{
    public static class AdditionalQuestionsMapper
    {
        public static MistakeWebModel.MistakeAdditionalQuestionsWebModel ToWebModel(this Mistake.MistakeAdditionalQuestions mistakeAdditionalQuestions)
        {
            return mistakeAdditionalQuestions is null ? null : new MistakeWebModel.MistakeAdditionalQuestionsWebModel
            {
                Consequences = mistakeAdditionalQuestions.Consequences,
                CanIFixIt = mistakeAdditionalQuestions.CanIFixIt,
                WhatDidILearn = mistakeAdditionalQuestions.WhatDidILearn,
                WhatCanIDoBetter = mistakeAdditionalQuestions.WhatCanIDoBetter,
                OnlyResponsible = mistakeAdditionalQuestions.OnlyResponsible
            };
        }
    }
}