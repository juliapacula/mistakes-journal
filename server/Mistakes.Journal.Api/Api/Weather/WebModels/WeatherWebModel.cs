namespace Mistakes.Journal.Api.Api.Weather.WebModels
{
    public class WeatherWebModel
    {
        public WeatherType Weather { get; set; }

        public TemperatureRange Temperature { get; set; }

        public string City { get; set; }

        public enum TemperatureRange
        {
            Cold = 0,
            Chilly = 1,
            Medium = 2,
            Hot = 3,
            Tropical = 4,
        }

        public enum WeatherType
        {
            Clear = 0,
            Rain = 1,
            Other = 2,
        }
    }
}