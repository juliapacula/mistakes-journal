namespace Mistakes.Journal.Api.Api.Mistakes.WebModels
{
    public class WeatherWebModel
    {
        public WeatherType Weather { get; set; }

        public TemperatureRange Temperature { get; set; }

        public TimeOfDayType TimeOfDay { get; set; }

        public string Place { get; set; }
    }

    public enum TemperatureRange
    {
        Cold = 0,
        Chilly = 1,
        Medium = 2,
        Hot = 3,
        Tropical = 4,
    }

    public enum TimeOfDayType
    {
        Morning = 0,
        Day = 1,
        Night = 2,
    }

    public enum WeatherType
    {
        Clear = 0,
        Rain = 1,
        Other = 2,
    }
}