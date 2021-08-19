using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class MistakeSearchWebModel : PagingParameters
    {
        public string Name { get; set; }

        public string Goal { get; set; }

        public IEnumerable<Mistake.MistakePriority> Priorities { get; set; }

        public IEnumerable<string> Labels { get; set; }


        // TODO if needed: by dates, counter, progress, milestones...
    }
}