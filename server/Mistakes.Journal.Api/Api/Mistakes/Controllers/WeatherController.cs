
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mistakes.Journal.Api.Api.Mistakes.Mappers;
using Mistakes.Journal.Api.Api.Mistakes.WebModels;
using Mistakes.Journal.Api.Api.Shared;
using Newtonsoft.Json.Linq;

namespace Mistakes.Journal.Api.Api.Mistakes.Controllers
{
    [ApiController]
    [Route("/api/weather")]
    public class WeatherController : ControllerBase
    {
        private const string pwRequestPattern = "data/2.5/weather?lat={0}&lon={1}&appid={2}&units=metric";
        private const string ssRequestPattern = "json?lat={0}&lng={1}&formatted=0";

        private readonly Uri owmApiUri = new Uri("http://api.openweathermap.org");
        private readonly Uri ssApiUri = new Uri("https://api.sunrise-sunset.org");

        private readonly string owKey = Environment.GetEnvironmentVariable("OPEN_WEATHER_API_KEY");

        [HttpGet]
        public async Task<ActionResult<WeatherWebModel>> GetWeatherInfo(string lat, string lon)
        {
            string weatherJson, sunsetJson;

            try
            {
                weatherJson = await GetWeather(lat, lon);
                sunsetJson = await GetTimeOfDay(lat, lon);
            }
            catch (MJException e)
            {
                return Problem(type: e.ErrorMessageType.ToString(), detail: e.Message);
            }

            return Ok(WeatherMapper.ToWeatherWebModel(weatherJson, sunsetJson));
        }

        public async Task<string> GetWeather(string lat, string lon)
        {
            using var client = new HttpClient { BaseAddress = owmApiUri };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = string.Format(pwRequestPattern, lat, lon, owKey);
            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.BadRequest)
                throw new MJException(GetErrorType(await response.Content.ReadAsStringAsync()));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetTimeOfDay(string lat, string lon)
        {
            using var client = new HttpClient { BaseAddress = ssApiUri };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = string.Format(ssRequestPattern, lat, lon);
            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new MJException(ErrorMessageType.SunriseSunsetError, await response.Content.ReadAsStringAsync());

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
