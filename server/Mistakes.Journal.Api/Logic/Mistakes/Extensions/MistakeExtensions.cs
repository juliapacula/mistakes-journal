using System;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Logic.Mistakes.Extensions
{
    public static class MistakeExtensions
    {
        public static bool CanBeSolved(this Mistake mistake)
        {
            return (DateTime.Now - mistake.CreatedAt).TotalDays >= Constants.DaysToSolveMistake;
        }
    }
}
