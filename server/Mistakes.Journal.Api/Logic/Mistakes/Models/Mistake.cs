using System;
using System.Collections.Generic;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Mistake
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Goal { get; }
        public MistakePriority Priority { get; }
        public ICollection<Tip> Tips { get; }

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

        public enum MistakePriority
        {
            Low = 0,
            Medium = 1,
            High = 2,
        }
    }
}