using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class MistakeLabel
    {
        public Guid MistakeId { get; set; }
        public Mistake Mistake { get; set; }

        public Guid LabelId { get; set; }
        public Label Label { get; set; }

    }
}
