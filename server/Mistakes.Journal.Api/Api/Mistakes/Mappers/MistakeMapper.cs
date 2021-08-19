using System;
using System.Collections.Generic;
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
                FirstOccurence = mistake.FirstOccurenceDateTime,
                LastOccurence = mistake.LastRepetitionDateTime,
                Counter = mistake.Counter,
                Tips = mistake.Tips.Select(t => t.Content).ToList(),
                Labels = mistake.MistakeLabels.Select(ml => ml.Label).ToList(),
            };
        }

        public static Mistake ToEntity(this NewMistakeWebModel newMistake)
        {
            return new Mistake(
                newMistake.Name,
                newMistake.Goal,
                newMistake.Priority.GetValueOrDefault(),
                newMistake.AddDateTime.GetValueOrDefault(DateTime.Now));
        }

        public static NewMistakeWebModel ToNewMistakeWebModel(this Mistake mistake)
        {
            return new NewMistakeWebModel
            {
                Name = mistake.Name,
                Goal = mistake.Goal,
                Priority = mistake.Priority,
                Tips = mistake.Tips.Select(t => t.Content).ToList(),
                AddDateTime = mistake.FirstOccurenceDateTime
            };
        }
    }
}