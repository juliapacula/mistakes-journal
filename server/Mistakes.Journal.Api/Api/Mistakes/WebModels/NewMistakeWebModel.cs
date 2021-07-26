using System.Collections.Generic;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class NewMistakeWebModel
    {
        public string Name { get; set; }
        public string Goal { get; set; }
        public Mistake.MistakePriority? Priority { get; set; }
        public IReadOnlyCollection<string> Tips { get; set; }
    }
}