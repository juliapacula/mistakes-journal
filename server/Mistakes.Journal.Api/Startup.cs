using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mistakes.Journal.Api.Api.Shared;

namespace Mistakes.Journal.Api
{
    public class Startup
    {
        private const string ClientApp = "client";
        private readonly IWebHostEnvironment _currentEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _currentEnvironment = env;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddMvcOptions(o => o.AllowEmptyInputInBodyModelBinding = true);

            services.AddSpaStaticFiles(options => options.RootPath = ClientApp);

            var databaseUri = (_configuration.GetConnectionString("DATABASE_URL").IsNullOrEmpty()
                    ? Environment.GetEnvironmentVariable("DATABASE_URL")
                    : _configuration.GetConnectionString("DATABASE_URL"))
                    .ToDatabaseUri(_currentEnvironment.IsDevelopment());
            services.AddDatabase(databaseUri);

            services.AddIdentityServices();
            services.AddIdentityPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment()) return;

            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
            app.UseSpaStaticFiles();
            app.UseSpa(spa => { spa.Options.SourcePath = ClientApp; });
        }
    }
}
