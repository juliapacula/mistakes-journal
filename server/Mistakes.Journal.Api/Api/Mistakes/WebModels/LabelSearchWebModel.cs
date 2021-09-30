using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared.RequestsParameters;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class LabelSearchWebModel
    {
        public string Name { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}