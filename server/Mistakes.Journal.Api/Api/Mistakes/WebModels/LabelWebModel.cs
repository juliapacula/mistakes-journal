using System;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class LabelWebModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int MistakesCounter { get; set; }
    }
}