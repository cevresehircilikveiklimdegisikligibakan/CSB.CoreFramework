using CSB.Core.WebApp.Middlewares.ExceptionLogging;
using CSB.Core.WebApp.Middlewares.ServiceLogging;
using Microsoft.AspNetCore.Builder;

namespace CSB.Core.WebApp.Middlewares
{
    public static class ServiceRegister
    {
        public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app,
                                                               bool addServiceLogging = true)
        {
            if (addServiceLogging)
                app.UseMiddleware<ServiceLoggingMiddleware>();

            app.UseMiddleware<ExceptionLoggingMiddleware>();

            return app;
        }
    }
}
