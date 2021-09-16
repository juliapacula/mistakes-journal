using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mistakes.Journal.Api.Api.Shared;
using Newtonsoft.Json.Linq;

namespace Mistakes.Journal.Api.Api.Weather
{
    [ApiController]
    [Route("/api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly Uri _owmApiUri = new Uri("http://api.openweathermap.org");
        private readonly Uri _ssApiUri = new Uri("https://api.sunrise-sunset.org");

        private readonly string _owKey = Environment.GetEnvironmentVariable("OPEN_WEATHER_API_KEY");

        [HttpGet]
        public async Task<ActionResult<WeatherWebModel>> GetWeatherInfo(string lat, string lon)
        {
            string weatherJson, sunsetJson;

            try
            {
                weatherJson = await GetWeather(lat, lon);
                sunsetJson = await GetTimeOfDay(lat, lon);
            }
            catch (WeatherException e)
            {
                return Problem(type: e.ErrorMessageType.ToString(), detail: e.Message);
            }

            return Ok(WeatherHelper.ToWeatherWebModel(weatherJson, sunsetJson));
        }

        private async Task<string> GetWeather(string lat, string lon)
        {
            using var client = new HttpClient { BaseAddress = _owmApiUri };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = $"data/2.5/weather?lat={lat}&lon={lon}&appid={_owKey}&units=metric";
            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.BadRequest)
                throw new WeatherException(GetErrorType(await response.Content.ReadAsStringAsync()));

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetTimeOfDay(string lat, string lon)
        {
            using var client = new HttpClient { BaseAddress = _ssApiUri };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = $"json?lat={lat}&lng={lon}&formatted=0";
            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new WeatherException(ErrorMessageType.SunriseSunsetError, await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }

        private ErrorMessageType GetErrorType(string errorJson)
        {
            dynamic response = JObject.Parse(errorJson);
            var msg = response.message.Value;

            return msg switch
            {
                "wrong longitude" => ErrorMessageType.WrongLongitude,
                "wrong latitude" => ErrorMessageType.WrongLatitude,
                "Nothing to geocode" => ErrorMessageType.IncompleteCoordinates,
                _ => ErrorMessageType.UnknownError,
            };
        }
    }
}
