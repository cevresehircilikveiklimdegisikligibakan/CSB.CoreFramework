using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CSB.Core
{
    public static class Assemblies
    {
        public static Assembly Domain { get; set; }
        public static Assembly Application { get; set; }
        public static Assembly Infrastructure { get; set; }

        public static IServiceCollection AddAssemblyToMapping(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}