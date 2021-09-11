using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class MistakeWebModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Goal { get; set; }
        public Mistake.MistakePriority Priority { get; set; }
        public IReadOnlyCollection<string> Tips { get; set; }
        public IReadOnlyCollection<LabelWebModel> Labels { get; set; }
        public IReadOnlyCollection<RepetitionWebModel> RepetitionDates { get; set; }
        public DateTime CreatedAt { get; set; }
        public SolvingState CurrentSolvingState { get; set; }
        public string Consequences { get; set; }
        public string WhatCanIDoBetter { get; set; }
        public string WhatDidILearn { get; set; }
        public string CanIFixIt { get; set; }
        public string OnlyResponsible { get; set; }
    }

    public enum SolvingState
    {
        InProgress = 0,
        CanBeSolved = 1,
        Solved = 2,
    }
}