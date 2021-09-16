using System;
using System.Collections.Generic;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Mistake
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Goal { get; set; }
        public MistakePriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Repetition> Repetitions { get; set; }
        public ICollection<Tip> Tips { get; set; }
        public ICollection<MistakeLabel> MistakeLabels { get; set; }
        public bool IsSolved { get; set; }

        private Mistake()
        {
        }

        public Mistake(string name,
            string goal,
            MistakePriority priority)
        {
            Name = name;
            Goal = goal;
            Priority = priority;
            Repetitions = new List<Repetition>();
            Tips = new List<Tip>();
            MistakeLabels = new List<MistakeLabel>();
            CreatedAt = DateTime.Now;
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