using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mistakes.Journal.Api.Logic.Mistakes.Models
{
    public class Label
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public uint Color { get; set; } // TODO jako co zapisywać color
        public ICollection<MistakeLabel> MistakeLabels { get; set; }

        public Label(string name, uint color)
        {
            Name = name;
            Color = color;
            MistakeLabels = new List<MistakeLabel>();
        }
    }
}
