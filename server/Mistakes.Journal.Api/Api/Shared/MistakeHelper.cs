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
                _ => OrderByCreationDate(collection, sortingParameters.Desc),
                // more options can be added
            };
        }

        private static IOrderedQueryable<Mistake> OrderByCreationDate(IQueryable<Mistake> collection, bool desc)
        {
            return desc
                ? collection.OrderByDescending(m => m.CreatedAt)
                : collection.OrderBy(m => m.CreatedAt);
        }
    }

    public enum MistakeSortingField
    {
        CreatedAt = 0,
    }
}