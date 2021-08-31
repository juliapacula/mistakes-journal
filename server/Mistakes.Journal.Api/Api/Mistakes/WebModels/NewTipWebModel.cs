using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Shared.Validators;

namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class NewTipWebModel
    {
        [MJRequired]
        [MJMaxLength(Constants.ShortTextMaxLength)]
        public string Content { get; set; }
    }
}