using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared.RequestsParameters;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class MistakeSearchWebModel : PagingParameters
    {
        public string Name { get; set; }

        public string Goal { get; set; }

        public IEnumerable<Mistake.MistakePriority> Priorities { get; set; }

        public IEnumerable<Guid> Labels { get; set; }

        // TODO MJ-127; Can be improved: by dates, counter, progress, milestones...
    }
}