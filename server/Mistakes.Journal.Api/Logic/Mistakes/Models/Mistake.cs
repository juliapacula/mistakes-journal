using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Mistake
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Goal { get; set; }
        public MistakePriority Priority { get; set; }
        public DateTime FirstOccurenceDateTime { get; set; }
        public DateTime LastRepetitionDateTime { get; set; }
        public DateTime? LastRepetitionRegistrationDateTime { get; set; }
        public int Counter { get; set; }
        public int? LastProgressInDays { get; set; }
        public ICollection<Tip> Tips { get; set; }
        public ICollection<MistakeLabel> MistakeLabels { get; set; }

        public Mistake(
            string name,
            string goal,
            MistakePriority priority,
            DateTime firstOccurenceDateTime
        )
        {
            Name = name;
            Goal = goal;
            Priority = priority;
            FirstOccurenceDateTime = firstOccurenceDateTime;
            LastRepetitionDateTime = firstOccurenceDateTime;
            Tips = new List<Tip>();
            MistakeLabels = new List<MistakeLabel>();
        }

        [Flags]
        public enum MistakePriority
        {
            Low = 1,
            Medium = 2,
            High = 4,
        }
    }
}