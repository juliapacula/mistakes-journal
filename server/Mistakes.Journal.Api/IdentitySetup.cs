using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Mistakes.Journal.Api.Logic.Identity.Models;
using Mistakes.Journal.Api.Logic.Identity.Services;
using Mistakes.Journal.Api.Resources;

namespace Mistakes.Journal.Api
{
    public static class IdentitySetup
    {
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<MistakesJournalUser, IdentityRole<Guid>>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<MistakesJournalContext>();

            services.Configure<CookiePolicyOptions>(options => { options.Secure = CookieSecurePolicy.Always; });

            services.Configure<DataProtectionTokenProviderOptions>(tokenOptions =>
            {
                tokenOptions.TokenLifespan = TimeSpan.FromHours(48);
            });
            
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/api/self/logout";
                options.AccessDeniedPath = "/account/denied";
                options.ReturnUrlParameter = "returnUrl";
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                options.SlidingExpiration = true;
                options.Cookie.Name = "MistakesJournal";
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                    else
                    {
                        context.Response.Redirect(context.RedirectUri);
                    }

                    return Task.CompletedTask;
                };
            });
            services.AddTransient<IUserProvider, MistakesJournalUserProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddIdentityPages(this IServiceCollection services)
        {
            services.AddSingleton<AccountLocalizationService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, o => o.ResourcesPath = "Resources")
                .AddDataAnnotationsLocalization();
            
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("pl")
                    };

                    options.DefaultRequestCulture = new RequestCulture("pl");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                });
        }
    }
}