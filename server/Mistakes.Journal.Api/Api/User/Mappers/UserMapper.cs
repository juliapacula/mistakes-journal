using System;
using Mistakes.Journal.Api.Api.User.WebModels;
using Mistakes.Journal.Api.Logic.Identity.Models;

namespace Mistakes.Journal.Api.Api.User.Mappers
{
    public static class UserMapper
    {
        public static UserWebModel ToWebModel(this MistakesJournalUser user)
        {
            return new UserWebModel
            {
                Email = user.Email,
                Country = user.Country,
                Age = user.Age,
                Group = user.Group,
                Language = user.Language,
                WatchedTutorial = user.WatchedTutorial,
            };
        }

        public static bool IsToday(this DateTime dateTime)
        {
            // 4 = next day starts at 4 a.m.
            return (DateTime.Now - TimeSpan.FromHours(4)).Date - (dateTime.Date - TimeSpan.FromHours(4)).Date != TimeSpan.Zero;
        }

        public static UserWebModel.AgeRange AgeToAgeRange(int age)
        {
            return age switch
            {
                var n when n <= 18 => UserWebModel.AgeRange.RangeTo18,
                var n when n <= 25 => UserWebModel.AgeRange.Range19To25,
                var n when n <= 35 => UserWebModel.AgeRange.Range26To35,
                var n when n <= 55 => UserWebModel.AgeRange.Range36To55,
                _ => UserWebModel.AgeRange.Range56AndMore,
            };
        }
    }
}