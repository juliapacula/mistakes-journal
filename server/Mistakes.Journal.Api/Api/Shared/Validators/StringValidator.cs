namespace Mistakes.Journal.Api.Api.Shared.Validators
{
    public static class StringValidator
    {
        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrEmpty(value?.Trim());
        }
    }
}