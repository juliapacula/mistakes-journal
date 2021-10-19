using System;
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
                MistakesCounter = label.MistakeLabels.Count
            };
        }

        public static Label ToEntity(this NewLabelWebModel newLabel, Guid labelId)
        {
            return new Label(
                labelId,
                newLabel.Name,
                newLabel.Color);
        }
    }
}