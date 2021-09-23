using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Shared.Validators;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class UpdatedMistakeWebModel
    {
        [MJMaxLength(Constants.ShortTextMaxLength)]
        public string Name { get; set; }

        [MJMaxLength(Constants.ShortTextMaxLength)]
        public string Goal { get; set; }
        public Mistake.MistakePriority? Priority { get; set; }

        [MJEachElementMaxLength(Constants.ShortTextMaxLength)]
        public IReadOnlyCollection<string> Tips { get; set; }

        [MJMaxLength(Constants.LongTextMaxLength)]
        public string Consequences { get; set; }

        [MJMaxLength(Constants.LongTextMaxLength)]
        public string WhatCanIDoBetter { get; set; }

        [MJMaxLength(Constants.LongTextMaxLength)]
        public string WhatDidILearn { get; set; }

        [MJMaxLength(Constants.LongTextMaxLength)]
        public string CanIFixIt { get; set; }

        [MJMaxLength(Constants.LongTextMaxLength)]
        public string OnlyResponsible { get; set; }
    }
}