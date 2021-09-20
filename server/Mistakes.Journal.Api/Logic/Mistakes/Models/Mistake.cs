using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Logic.Identity.Models;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Mistake
    {
        public Guid Id { get; }
        public Guid UserId { get; set; }
        public MistakesJournalUser User { get; set; }
        public string Name { get; set; }
        public string Goal { get; set; }
        public MistakePriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Repetition> Repetitions { get; set; }
        public ICollection<Tip> Tips { get; set; }
        public ICollection<MistakeLabel> MistakeLabels { get; set; }
        public bool IsSolved { get; set; }
        public AdditionalQuestions AdditonalQuestions { get; set; }

        private Mistake()
        {
        }

        public Mistake(
            Guid createdBy,
            string name,
            string goal,
            MistakePriority priority,
            string consequences,
            string whatCanIDoBetter,
            string whatDidILearn,
            string canIFixIt,
            string onlyResponsible)
        {
            UserId = createdBy;
            Name = name;
            Goal = goal;
            Priority = priority;
            Repetitions = new List<Repetition>();
            Tips = new List<Tip>();
            MistakeLabels = new List<MistakeLabel>();
            CreatedAt = DateTime.Now;

            AdditonalQuestions = new AdditionalQuestions
            {
                Consequences = consequences,
                WhatCanIDoBetter = whatCanIDoBetter,
                WhatDidILearn = whatDidILearn,
                CanIFixIt = canIFixIt,
                OnlyResponsible = onlyResponsible
            };
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