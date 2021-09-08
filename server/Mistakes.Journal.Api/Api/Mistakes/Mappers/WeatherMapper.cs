using System;
using System.Linq;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Newtonsoft.Json.Linq;

namespace Mistakes.Journal.Api.Api.Mistakes.Mappers
{
    public static class WeatherMapper
    {
        // https://openweathermap.org/weather-conditions
        private static readonly long[] GoodWeatherCodes = { 600, 800, 801 };

        public static WeatherWebModel ToWeatherWebModel(string openWeatherJson, string sunsetJson)
        {
            dynamic owResponse = JObject.Parse(openWeatherJson);
            float temperature = (float)owResponse.main.temp;
            long weatherCode = owResponse.weather[0].id;
            string place = owResponse.name;

            dynamic ssResponse = JObject.Parse(sunsetJson);
            DateTime sunrise = ssResponse.results.sunrise;
            DateTime sunset = ssResponse.results.sunset;
            DateTime noon = ssResponse.results.solar_noon;

            return new WeatherWebModel
            {
                Weather = GetWeatherType(weatherCode),
                Temperature = GetTemperature(temperature),
                TimeOfDay = GetTimeOfDay(sunrise, sunset, noon),
                Place = place,
            };
        }

        private static TimeOfDayType GetTimeOfDay(DateTime sunrise, DateTime sunset, DateTime noon)
        {
            var now = DateTime.Now;

            if (now < sunrise || now > sunset)
                return TimeOfDayType.Night;

            return now < noon ? TimeOfDayType.Morning : TimeOfDayType.Day;
        }

        private static TemperatureRange GetTemperature(float temperature)
        {
            return temperature switch
            {
                float n when n < 0 => TemperatureRange.Cold,
                float n when n < 10 => TemperatureRange.Chilly,
                float n when n < 22 => TemperatureRange.Medium,
                float n when n < 34 => TemperatureRange.Hot,
                _ => TemperatureRange.Tropical,
            };
        }

        private static WeatherType GetWeatherType(long weatherCode)
        {
            if (GoodWeatherCodes.Contains(weatherCode))
                return WeatherType.Clear;

            if (weatherCode >= 500 && weatherCode < 600)
                return WeatherType.Rain;

            return WeatherType.Other;
        }
    }
}
