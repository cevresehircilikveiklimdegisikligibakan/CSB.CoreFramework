using CSB.Core.WebAPI.Middlewares;
using CSB.Core.WebAPI.Middlewares.ExceptionLogging;
using CSB.Core.WebAPI.Middlewares.ServiceLogging;
using Microsoft.AspNetCore.Builder;

namespace CSB.Core.WebAPI
{
    public static class ServiceRegister
    {
        public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app,
                                                                bool addExternalAuthentication = true,
                                                                bool addServiceLogging = true)
        {
            if (addServiceLogging)
                app.UseMiddleware<ServiceLoggingMiddleware>();

            app.UseMiddleware<ExceptionLoggingMiddleware>();

            if (addExternalAuthentication)
                app.UseMiddleware<ExternalAuthenticationMiddleware>();

            return app;
        }
    }
}