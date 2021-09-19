using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mistakes.Journal.Api
{
    public class Startup
    {
        private const string ClientApp = "client";
        private readonly IWebHostEnvironment _currentEnvironment;

        public Startup(IWebHostEnvironment env)
        {
            _currentEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddMvcOptions(o => o.AllowEmptyInputInBodyModelBinding = true);

            services.AddSpaStaticFiles(options => options.RootPath = ClientApp);
            services.AddDbContext<MistakesJournalContext>(options => options
                .UseNpgsql(ParseDatabaseUri("postgres://postgres:mistakeSJournal@localhost/mistakes_journal"))
                .UseSnakeCaseNamingConvention());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            if (env.IsDevelopment()) return;

            app.UseDefaultFiles(new DefaultFilesOptions {DefaultFileNames = new List<string> {"index.html"}});
            app.UseSpaStaticFiles();
            app.UseSpa(spa => { spa.Options.SourcePath = ClientApp; });
        }

        private string ParseDatabaseUri(string databaseUrl)
        {
            Uri.TryCreate(databaseUrl, UriKind.Absolute, out var uri);

            if (uri == null)
            {
                return string.Empty;
            }

            var userInfo = uri.UserInfo.Split(':');
            var connectionString = $"Host={uri.Host};Database={uri.LocalPath.Substring(1)};Username={userInfo[0]};Password={userInfo[1]};";

            if (_currentEnvironment.IsDevelopment())
            {
                return connectionString;
            }
            
            return connectionString + "SslMode=Require;TrustServerCertificate=true;";
        }
    }
}