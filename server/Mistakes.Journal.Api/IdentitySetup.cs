using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Mistakes.Journal.Api.Logic.Identity.Models;
using Mistakes.Journal.Api.Logic.Identity.Services;

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
            services.AddControllersWithViews();
            services.AddRazorPages();
        }
    }
}