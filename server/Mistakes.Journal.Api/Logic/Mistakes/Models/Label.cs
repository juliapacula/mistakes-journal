using System;
using System.Collections.Generic;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Label
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<MistakeLabel> MistakeLabels { get; set; }

        public Label(string name, string color)
        {
            Name = name;
            Color = color;
            MistakeLabels = new List<MistakeLabel>();
        }
    }
}
