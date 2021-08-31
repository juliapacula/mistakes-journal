
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
        private const string requestPattern = "data/2.5/weather?lat={0}&lon={1}&appid={2}";

        private readonly Uri owmApiUri = new Uri("http://api.openweathermap.org");
        private readonly string key = Environment.GetEnvironmentVariable("OPEN_WEATHER_API_KEY");

        [HttpGet]
        public async Task<ActionResult<WeatherWebModel>> GetWeather(string lat, string lon)
        {
            using var client = new HttpClient { BaseAddress = owmApiUri };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = string.Format(requestPattern, lat, lon, key);
            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(GetErrorType(await response.Content.ReadAsStringAsync()));

            var json = await response.Content.ReadAsStringAsync();
            var weatherWebModel = json.ToWeatherWebModel();

            return Ok(weatherWebModel);
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
