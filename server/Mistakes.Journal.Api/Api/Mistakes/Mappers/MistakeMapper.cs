using System.Linq;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Logic.Mistakes.Extensions;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Mappers
{
    public static class MistakeMapper
    {
        public static MistakeWebModel ToWebModel(this Mistake mistake)
        {
            return new MistakeWebModel
            {
                Id = mistake.Id,
                Name = mistake.Name,
                Goal = mistake.Goal,
                Priority = mistake.Priority,
                Labels = mistake.MistakeLabels.Select(ml => ml.Label.ToWebModel()).ToList(),
                Tips = mistake.Tips.Select(t => t.Content).ToList(),
                RepetitionDates = mistake.Repetitions.Select(r => r.ToWebModel()).ToList(),
                CreatedAt = mistake.CreatedAt,
                CurrentSolvingState = mistake.IsSolved ? SolvingState.Solved : mistake.CanBeSolved() ? SolvingState.CanBeSolved : SolvingState.InProgress,
                Consequences = mistake.AdditonalQuestions.Consequences,
                WhatDidILearn = mistake.AdditonalQuestions.WhatDidILearn,
                WhatCanIDoBetter = mistake.AdditonalQuestions.WhatCanIDoBetter,
                CanIFixIt = mistake.AdditonalQuestions.CanIFixIt,
                OnlyResponsible = mistake.AdditonalQuestions.OnlyResponsible,
            };
        }

        public static Mistake ToEntity(this NewMistakeWebModel newMistake)
        {
            return new Mistake(
                newMistake.Name,
                newMistake.Goal,
                newMistake.Priority.GetValueOrDefault(),
                newMistake.Consequences,
                newMistake.WhatCanIDoBetter,
                newMistake.WhatDidILearn,
                newMistake.CanIFixIt,
                newMistake.OnlyResponsible);
        }

        public static NewMistakeWebModel ToNewMistakeWebModel(this Mistake mistake)
        {
            return new NewMistakeWebModel
            {
                Name = mistake.Name,
                Goal = mistake.Goal,
                Priority = mistake.Priority,
                Tips = mistake.Tips.Select(t => t.Content).ToList(),
                Labels = mistake.MistakeLabels.Select(ml => ml.LabelId).ToList(),
                Consequences = mistake.AdditonalQuestions.Consequences,
                WhatDidILearn = mistake.AdditonalQuestions.WhatDidILearn,
                WhatCanIDoBetter = mistake.AdditonalQuestions.WhatCanIDoBetter,
                CanIFixIt = mistake.AdditonalQuestions.CanIFixIt,
                OnlyResponsible = mistake.AdditonalQuestions.OnlyResponsible,
            };
        }
    }
}