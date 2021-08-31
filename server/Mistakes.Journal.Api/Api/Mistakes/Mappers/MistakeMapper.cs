using System.Linq;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
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
                CreatedAt = mistake.CreatedAt
                CurrentSolvingState = mistake.IsSolved ? SolvingState.Solved : mistake.CanBeSolved() ? SolvingState.CanBeSolved : SolvingState.InProgress,
            };
        }

        public static Mistake ToEntity(this NewMistakeWebModel newMistake)
        {
            return new Mistake(
                newMistake.Name,
                newMistake.Goal,
                newMistake.Priority.GetValueOrDefault());
        }

        public static NewMistakeWebModel ToNewMistakeWebModel(this Mistake mistake)
        {
            return new NewMistakeWebModel
            {
                Name = mistake.Name,
                Goal = mistake.Goal,
                Priority = mistake.Priority,
                Tips = mistake.Tips.Select(t => t.Content).ToList(),
                Labels = mistake.MistakeLabels.Select(ml => ml.LabelId).ToList()
            };
        }
    }
}