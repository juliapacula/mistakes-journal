using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Logic.Identity.Models;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Label
    {
        public Guid Id { get; }
        public Guid UserId { get; set; }
        public MistakesJournalUser User { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<MistakeLabel> MistakeLabels { get; set; }

        public Label(Guid createdBy, string name, string color)
        {
            UserId = createdBy;
            Name = name;
            Color = color;
            MistakeLabels = new List<MistakeLabel>();
        }
    }
}
