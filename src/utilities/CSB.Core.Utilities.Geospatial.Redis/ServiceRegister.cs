using CSB.Core.Utilities.Geospatial.Redis.Services;
using CSB.Core.Utilities.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSB.Core.Utilities.Geospatial.Redis
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddRedisGeospatialService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedisService(configuration);
            services.AddScoped<IGeospatialSevice, GeospatialSevice>();
            return services;
        }
    }
}