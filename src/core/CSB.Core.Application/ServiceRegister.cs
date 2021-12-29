using CSB.Core.Application.Services.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CSB.Core.Application
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddCoreApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidatorServices();
            services.AddMediatR();
            return services;
        }
        private static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assemblies.Application);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
        private static IServiceCollection AddFluentValidatorServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assemblies.Application);
            return services;
        }
    }
}