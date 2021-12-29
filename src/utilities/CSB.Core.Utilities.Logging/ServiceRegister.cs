using CSB.Core.Utilities.Logging.Entities;
using CSB.Core.Utilities.Logging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSB.Core.Utilities.Logging
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddLogingService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILogService, LogService>();
            services.Configure<LogOptions>(o => configuration.GetSection("LogOptions").Bind(o));
            return services;
        }
    }
}