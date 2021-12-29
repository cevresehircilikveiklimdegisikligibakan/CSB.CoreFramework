using CSB.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;

namespace CSB.Core
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration, JsonSerializerSettings settings = default)
        {
            services.AddAssemblyToMapping(Assembly.GetExecutingAssembly());
            services.AddSingleton<JsonSerializerSettings>(x => settings ?? new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
            services.AddTransient<ISerializer, Serializer>();
            services.AddScoped<IEncoder, Encoder>();
            services.AddScoped<IEnumService, EnumService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMappingService, MappingService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ISerializer, Serializer>();
            services.AddScoped<ITextService, TextService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IXMLSerializer, XMLSerializer>();
            return services;
        }
    }
}