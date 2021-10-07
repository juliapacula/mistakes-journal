namespace Mistakes.Journal.Api.Api.Weather.WebModels
{
    public class UserLocationDataWebModel
    {
        /// <summary>
        /// Scale from 0 (great weather) to 9 (very bad weather).
        /// </summary>
        public int WeatherHopelessnessScale { get; set; }

        public string City { get; set; }

        public TimeOfDayType TimeOfDay { get; set; }

        public enum TimeOfDayType
        {
            DayMorning = 0,
            DayNoon = 1,
            DayEvening = 2,

            NightEvening = 3,
            NightMidnight = 4,
            NightMorning = 5,
        }
    }
}
