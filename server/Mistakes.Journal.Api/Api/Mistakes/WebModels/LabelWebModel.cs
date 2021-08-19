using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class LabelWebModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public uint Color { get; set; }
        public IReadOnlyCollection<Mistake> Mistakes { get; set; }
    }
}