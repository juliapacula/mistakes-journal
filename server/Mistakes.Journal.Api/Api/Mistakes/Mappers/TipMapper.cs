using System;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Mistakes.Mappers
{
    public static class TipMapper
    {
        public static TipWebModel ToWebModel(this Tip tip)
        {
            return new TipWebModel
            {
                Id = tip.Id,
                Content = tip.Content,
            };
        }

        public static Tip ToEntity(this NewTipWebModel newTip, Guid userId)
        {
            return new Tip(userId, newTip.Content);
        }
    }
}
