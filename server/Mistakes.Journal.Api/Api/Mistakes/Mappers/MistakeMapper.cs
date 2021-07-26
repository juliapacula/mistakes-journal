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
                Tips = mistake.Tips.Select(t => t.Content).ToList(),
            };
        }
    }
}