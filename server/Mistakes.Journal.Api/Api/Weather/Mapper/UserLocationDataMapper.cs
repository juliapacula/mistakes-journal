using System;
using System.Linq;
using Mistakes.Journal.Api.Api.Weather.WebModels;
using Newtonsoft.Json.Linq;

namespace Mistakes.Journal.Api.Api.Weather.Mapper
{
    public static class UserLocationDataMapper
    {
        // https://openweathermap.org/weather-conditions

        public static UserLocationDataWebModel ToUserLocationDataWebModel(this string openWeatherJson)
        {
            dynamic owResponse = JObject.Parse(openWeatherJson);
            int clouds = (int)owResponse.clouds.all;
            var rain = (((owResponse.rain as JObject)?.First as JProperty)?.Value as JValue)?.Value;
            var rainValue = rain is null ? 0d : Convert.ToDouble(rain);
            int sunrise = owResponse.sys.sunrise;
            int sunset = owResponse.sys.sunset;
            string place = owResponse.name;

            // average from clouds (0-10) and mm of rain from last hour
            var weatherResult = (clouds / 10f + rainValue) / 2f;
            var sunriseDateTime = DateTimeOffset.FromUnixTimeSeconds(sunrise).UtcDateTime;
            var sunsetDateTime = DateTimeOffset.FromUnixTimeSeconds(sunset).UtcDateTime;

            return new UserLocationDataWebModel
            {
                WeatherHopelessnessScale = weatherResult > 9 ? 9 : (int)weatherResult,
                TimeOfDay = GetTimeOfDay(sunriseDateTime, sunsetDateTime),
                City = place,
            };
        }

        private static UserLocationDataWebModel.TimeOfDayType GetTimeOfDay(DateTime sunrise, DateTime sunset)
        {
            var now = DateTime.UtcNow;

            if (now < sunrise || now > sunset)
            {
                if (now < sunrise)
                    sunset -= TimeSpan.FromDays(1);
                else
                    sunrise += TimeSpan.FromDays(1);

                var nightTimePart = (sunrise - sunset) / 3;

                if (now > sunrise - nightTimePart)
                    return UserLocationDataWebModel.TimeOfDayType.NightMorning;

                if (now > sunrise - 2 * nightTimePart)
                    return UserLocationDataWebModel.TimeOfDayType.NightMidnight;

                return UserLocationDataWebModel.TimeOfDayType.NightEvening;
            }

            var dayTimePart = (sunset - sunrise) / 3;

            if (now > sunset - dayTimePart)
                return UserLocationDataWebModel.TimeOfDayType.DayEvening;

            if (now > sunset - 2 * dayTimePart)
                return UserLocationDataWebModel.TimeOfDayType.DayNoon;

            return UserLocationDataWebModel.TimeOfDayType.DayMorning;
        }
    }
}
