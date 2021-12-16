using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mistakes.Journal.Api.Api.Shared;
using Mistakes.Journal.Api.Api.Weather.Mapper;
using Mistakes.Journal.Api.Api.Weather.WebModels;
using Newtonsoft.Json.Linq;

namespace Mistakes.Journal.Api.Api.Weather.Controller
{
    [ApiController]
    [Authorize]
    [Route("/api/user-location-data")]
    public class UserLocationDataController : ControllerBase
    {
        private readonly Uri _owmApiUri = new Uri("http://api.openweathermap.org");
        private readonly ILogger _logger;

        private readonly string _owKey = Environment.GetEnvironmentVariable("OPEN_WEATHER_API_KEY");

        public UserLocationDataController(ILogger<UserLocationDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<UserLocationDataWebModel>> GetUserLocationData(string lat, string lon)
        {
            using var client = new HttpClient { BaseAddress = _owmApiUri };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = $"data/2.5/weather?lat={lat}&lon={lon}&appid={_owKey}&units=metric";
            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(GetErrorType(await response.Content.ReadAsStringAsync()).ToString());
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Error, $"GetUserLocationData: request = {request}");
                _logger.Log(LogLevel.Error, $"GetUserLocationData: responseStatusCode = {response.StatusCode}, owKey={_owKey}");

                return BadRequest();
            }

            var output = await response.Content.ReadAsStringAsync();
            return Ok(output.ToUserLocationDataWebModel());
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