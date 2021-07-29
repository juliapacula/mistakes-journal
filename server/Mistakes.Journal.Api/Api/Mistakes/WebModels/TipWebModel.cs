using System;
using System.Collections.Generic;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class TipWebModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}