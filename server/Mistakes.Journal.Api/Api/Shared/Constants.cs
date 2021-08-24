using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mistakes.Journal.Api.Api.Shared
{
    public static class Constants
    {
        /* Requirements */
        public const int ShortTextMaxLength = 100;
        public const int MaxMistakeAgeInDays = 3;

        /* Patterns */
        public const string ColorPattern = @"#[0-9a-fA-F]{3,8}";
    }
}
