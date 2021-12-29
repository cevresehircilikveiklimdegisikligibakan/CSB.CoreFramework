using CSB.Core.Infrastructure.Interceptors.Caching;
using CSB.Core.Infrastructure.Interceptors.Caching.Services;
using CSB.Core.Infrastructure.Interceptors.Loging;
using CSB.Core.Infrastructure.Interceptors.Loging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSB.Core.Infrastructure
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddCoreInfrastructureServices(this IServiceCollection services, IConfiguration configuration,
            bool addCachingInterceptorServices = true, bool addLogInterceptorServices = true)
        {
            if (addCachingInterceptorServices)
                services.AddCachingInterceptorServices();
            if (addLogInterceptorServices)
                services.AddLogInterceptorServices();

            services.AddAutoMapperServices();

            return services;
        }

        private static IServiceCollection AddCachingInterceptorServices(this IServiceCollection services)
        {
            services.AddScoped<ICachedInterceptorService, CachedInterceptorService>();
            services.AddScoped<IClearCacheInterceptorService, ClearCacheInterceptorService>();

            return services;
        }
        private static IServiceCollection AddLogInterceptorServices(this IServiceCollection services)
        {
            services.AddScoped<ILogInterceptorService, LogInterceptorService>();

            return services;
        }
        private static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddAssemblyToMapping(Assemblies.Domain);
            services.AddAssemblyToMapping(Assemblies.Application);
            return services;
        }
    }
}