using System;

namespace Mistakes.Journal.Api.Logic.Shared.Extensions
{
    public static class DateExtensions
    {
        public static DateTime ToSeconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        public static bool IsAfter(this DateTime date, DateTime anotherDate)
        {
            return DateTime.Compare(date, anotherDate) > 0;
        }
    }
}