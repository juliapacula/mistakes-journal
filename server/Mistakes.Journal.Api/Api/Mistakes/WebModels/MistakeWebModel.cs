using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared;
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
        public IReadOnlyCollection<Label> Labels { get; set; }
        public DateTime FirstOccurence { get; set; }
        public DateTime LastOccurence { get; set; }
        public int Counter { get; set; }

        /* Calculated measures */
        public int DaysGone => (DateTime.Now - LastOccurence).Days;
        public float Progress => DaysGone / (float) Constants.MistakeProgressMax;

        /* Constants */
        // TODO Czy podawaæ te¿ te sta³e??
        public List<int> Milestones => new List<int>
        {
            Constants.FirstMilestone,
            Constants.SecondMilestone,
            Constants.MistakeProgressMax
        };
    }
}