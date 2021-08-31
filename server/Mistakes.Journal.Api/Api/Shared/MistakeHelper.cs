using System.Linq;
using Mistakes.Journal.Api.Api.Shared.RequestsParameters;
using Mistakes.Journal.Api.Logic.Mistakes.Models;

namespace Mistakes.Journal.Api.Api.Shared
{
    public static class MistakeHelper
    {
        public static IOrderedQueryable<Mistake> OrderByField(this IQueryable<Mistake> collection, MistakesSortingParameters sortingParameters)
        {
            return sortingParameters.Field switch
            {
                _ => OrderByLastOccurence(collection, sortingParameters.Desc),

                // more options can be added
            };
        }

        private static IOrderedQueryable<Mistake> OrderByLastOccurence(IQueryable<Mistake> collection, bool desc)
        {
            return desc
                ? collection.OrderByDescending(m => m.Repetitions.First().DateTime)
                : collection.OrderBy(m => m.Repetitions.First().DateTime);
        }
    }

    public enum MistakeSortingField
    {
        FirstOccurence = 0
    }
}