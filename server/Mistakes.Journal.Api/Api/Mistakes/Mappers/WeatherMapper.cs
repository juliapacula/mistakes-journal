using System.Linq;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Newtonsoft.Json.Linq;

namespace Mistakes.Journal.Api.Api.Mistakes.Mappers
{
    public static class WeatherMapper
    {
        // https://openweathermap.org/weather-conditions
        private static readonly long[] GoodWeatherCodes = {600, 800, 801, 802};

        public static WeatherWebModel ToWeatherWebModel(this string openWeatherJson) // TODO MJ-133 MJ-49
        {
            dynamic response = JObject.Parse(openWeatherJson);
            string iconName = response.weather[0].icon.Value;
            long weatherCode = response.weather[0].id.Value;
            string place = response.name;

            return new WeatherWebModel
            {
                IsDay = iconName.EndsWith('d'),
                IsGoodWeather = GoodWeatherCodes.Contains(weatherCode),
                Place = place,
            };
        }
    }
}
