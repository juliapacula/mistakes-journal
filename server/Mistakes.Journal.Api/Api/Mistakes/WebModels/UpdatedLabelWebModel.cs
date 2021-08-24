using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Shared.Validators;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class UpdatedLabelWebModel
    {
        [MJMaxLength(Constants.ShortTextMaxLength)]
        public string Name { get; set; }
        
        [MJMatchPattern(Constants.ColorPattern)]
        public string Color { get; set; }
    }
}