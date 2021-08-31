using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared.RequestsParameters;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class LabelSearchWebModel : PagingParameters
    {
        public string Name { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}