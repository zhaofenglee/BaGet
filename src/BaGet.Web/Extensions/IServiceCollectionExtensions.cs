using System;
using BaGet.Core;
using BaGet.Core.Authentication;
using BaGet.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BaGet
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBaGetWebApplication(
            this IServiceCollection services,
            Action<BaGetApplication> configureAction)
        {
            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers()
                .AddApplicationPart(typeof(PackageContentController).Assembly)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddRazorPages();

            services.AddHttpContextAccessor();
            services.AddTransient<IUrlGenerator, BaGetUrlGenerator>();

            var app = services.AddBaGetApplication(configureAction);
            app.AddNugetBasicHttpAuthentication();
            app.AddNugetBasicHttpAuthorization();

            return services;
        }

        private static BaGetApplication AddNugetBasicHttpAuthentication(this BaGetApplication app)
        {
            app.Services.AddAuthentication(options =>
            {
                // Breaks existing tests if the contains check is not here.
                if (!options.SchemeMap.ContainsKey(AuthenticationConstants.NugetBasicAuthenticationScheme))
                {
                    options.AddScheme<NugetBasicAuthenticationHandler>(AuthenticationConstants.NugetBasicAuthenticationScheme, AuthenticationConstants.NugetBasicAuthenticationScheme);
                    options.DefaultAuthenticateScheme = AuthenticationConstants.NugetBasicAuthenticationScheme;
                    options.DefaultChallengeScheme = AuthenticationConstants.NugetBasicAuthenticationScheme;
                }
            });

            return app;
        }

        private static BaGetApplication AddNugetBasicHttpAuthorization(this BaGetApplication app)
        {
            app.Services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthenticationConstants.NugetUserPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            return app;
        }
    }
}
