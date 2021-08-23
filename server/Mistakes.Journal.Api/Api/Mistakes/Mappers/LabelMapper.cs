using System.Linq;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Mappers
{
    public static class LabelMapper
    {
        public static LabelWebModel ToWebModel(this Label label)
        {
            return new LabelWebModel
            {
                Id = label.Id,
                Name = label.Name,
                Color = label.Color,
            };
        }

        public static Label ToEntity(this NewLabelWebModel newLabel)
        {
            return new Label(
                newLabel.Name,
                newLabel.Color);
        }
    }
}