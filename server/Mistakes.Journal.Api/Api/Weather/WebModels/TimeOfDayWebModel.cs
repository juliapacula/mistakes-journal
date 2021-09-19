namespace Mistakes.Journal.Api.Api.Weather.WebModels
{
    public class TimeOfDayWebModel
    {
        public TimeOfDayType TimeOfDay { get; set; }

        public enum TimeOfDayType
        {
            Morning = 0,
            Day = 1,
            Night = 2,
        }
    }
}
