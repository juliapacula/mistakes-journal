namespace Mistakes.Journal.Api.Api.Shared
{
    public static class Constants
    {
        /* Requirements */
        public const int VeryShortTextMaxLength = 20;
        public const int ShortTextMaxLength = 100;
        public const int LongTextMaxLength = 500;

        /* Patterns */
        public const string ColorPattern = @"#[0-9a-fA-F]{3,8}";

        /* Mistakes */
        public const int DaysToSolveMistake = 66;
    }
}
