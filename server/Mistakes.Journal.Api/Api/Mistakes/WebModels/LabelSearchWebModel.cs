using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class LabelSearchWebModel : PagingParameters
    {
        public string Name { get; set; }
        public IEnumerable<uint> Colors { get; set; }
    }
}