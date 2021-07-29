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
        public ICollection<Tip> Tips { get; set; }

        public Mistake(
            string name,
            string goal,
            MistakePriority priority
        )
        {
            Name = name;
            Goal = goal;
            Priority = priority;
            Tips = new List<Tip>();
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