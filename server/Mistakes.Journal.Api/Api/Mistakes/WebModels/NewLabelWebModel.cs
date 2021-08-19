using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Shared.Validators;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class NewLabelWebModel
    {
        [MJRequired]
        [MJMaxLength(Constants.ShortTextMaxLength)]
        public string Name { get; set; }
        
        [MJRequired]
        public uint Color { get; set; }
    }
}