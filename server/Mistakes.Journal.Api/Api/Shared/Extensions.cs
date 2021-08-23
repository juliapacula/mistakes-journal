using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mistakes.Journal.Api.Api.Shared
{
    public static class Extensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }

        public static void RemoveEmptyStrings(this ICollection<string> collection)
        {
            foreach (var itemToRemove in collection.Where(string.IsNullOrWhiteSpace)) 
                collection.Remove(itemToRemove);
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static bool IsPresent(this string str)
        {
            return !(string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str));
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
        {
            return collection ?? Enumerable.Empty<T>();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }
    }
}
