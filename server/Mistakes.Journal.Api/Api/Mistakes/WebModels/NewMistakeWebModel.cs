using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Shared.Validators;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class NewMistakeWebModel
    {
        [MJRequired]
        [MJMaxLength(Constants.ShortTextMaxLength)]
        public string Name { get; set; }

        [MJMaxLength(Constants.ShortTextMaxLength)]
        public string Goal { get; set; }
        
        [MJRequired]
        public Mistake.MistakePriority? Priority { get; set; }

        [MJEachElementMaxLength(Constants.ShortTextMaxLength)]
        public IReadOnlyCollection<string> Tips { get; set; }

        public IReadOnlyCollection<Guid> Labels { get; set; }
    }
}