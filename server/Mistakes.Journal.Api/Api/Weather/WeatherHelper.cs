using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Mistakes.Journal.Api.Api.Weather
{
    public static class WeatherHelper
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
                City = place,
            };
        }

        private static WeatherWebModel.TimeOfDayType GetTimeOfDay(DateTime sunrise, DateTime sunset, DateTime noon)
        {
            var now = DateTime.Now;

            if (now < sunrise || now > sunset)
                return WeatherWebModel.TimeOfDayType.Night;

            return now < noon ? WeatherWebModel.TimeOfDayType.Morning : WeatherWebModel.TimeOfDayType.Day;
        }

        private static WeatherWebModel.TemperatureRange GetTemperature(float temperature)
        {
            return temperature switch
            {
                float n when n < 0 => WeatherWebModel.TemperatureRange.Cold,
                float n when n < 10 => WeatherWebModel.TemperatureRange.Chilly,
                float n when n < 22 => WeatherWebModel.TemperatureRange.Medium,
                float n when n < 34 => WeatherWebModel.TemperatureRange.Hot,
                _ => WeatherWebModel.TemperatureRange.Tropical,
            };
        }

        private static WeatherWebModel.WeatherType GetWeatherType(long weatherCode)
        {
            if (GoodWeatherCodes.Contains(weatherCode))
                return WeatherWebModel.WeatherType.Clear;

            if (weatherCode >= 500 && weatherCode < 600)
                return WeatherWebModel.WeatherType.Rain;

            return WeatherWebModel.WeatherType.Other;
        }
    }
}
