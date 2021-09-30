using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Shared.Validators;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class NewLabelWebModel
    {
        [MJRequired]
        [MJMaxLength(Constants.VeryShortTextMaxLength)]
        public string Name { get; set; }
        
        [MJRequired]
        [MJMatchPattern(Constants.ColorPattern)]
        public string Color { get; set; }
    }
}