using CSB.Core.Utilities.Logging;
using CSB.Core.Utilities.MessageBroking.RabbitMQ;
using CSB.Core.WebApp.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace CSB.Core.WebApp
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddCoreWebAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AuthHeaderHandler>();
            services.AddCoreUtilities(configuration);
            return services;
        }
        public static IServiceCollection AddKeycloackAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(30))
            .AddOpenIdConnect(options =>
            {
                options.ClientId = configuration["Oidc:ClientId"];
                options.ClientSecret = configuration["Oidc:ClientSecret"];
                options.Authority = configuration["Oidc:Authority"];
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ResponseType = "code";
                options.Scope.Add("profile");
                options.Scope.Add("roles");
                options.ClaimActions.MapUniqueJsonKey("roles", "roles");
                options.ClaimActions.MapUniqueJsonKey("name", "name");
                options.AccessDeniedPath = "/Home/Unauthorized";
            });
            services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = new PathString("/Home/Unauthorized");
            });
            return services;
        }
        public static IServiceCollection AddRefit(this IServiceCollection services, IConfiguration configuration, params Type[] types)
        {
            if (types.Length > 0)
            {
                IHttpClientBuilder refitClientBuilder = null;
                foreach (var type in types)
                {
                    refitClientBuilder = services.AddRefitClient(type);
                }
                refitClientBuilder.ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetValue<string>("CSBWebApiUrl")))
                                  .AddHttpMessageHandler<AuthHeaderHandler>();
            }
            return services;
        }

        public static IServiceCollection AddCoreUtilities(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices(configuration);
            services.AddRabbitMQMessageBrokingService(configuration);
            services.AddLogingService(configuration);

            return services;
        }
    }
}