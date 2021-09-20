using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mistakes.Journal.Api
{
    public static class DatabaseSetup
    {
        public static void AddDatabase(this IServiceCollection services, string databaseUri)
        {
            services.AddDbContext<MistakesJournalContext>(options => options
                .UseNpgsql(databaseUri)
                .UseSnakeCaseNamingConvention());
        }

        public static string ToDatabaseUri(this string databaseUrl, bool isDevelopmentMode)
        {
            Uri.TryCreate(databaseUrl, UriKind.Absolute, out var uri);

            if (uri == null)
            {
                return string.Empty;
            }

            var userInfo = uri.UserInfo.Split(':');
            var connectionString =
                $"Host={uri.Host};Database={uri.LocalPath.Substring(1)};Username={userInfo[0]};Password={userInfo[1]};";

            if (isDevelopmentMode)
            {
                return connectionString;
            }

            return connectionString + "SslMode=Require;TrustServerCertificate=true;";
        }
    }
}