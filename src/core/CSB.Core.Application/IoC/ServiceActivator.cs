using CSB.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CSB.Core.Application.DependencyResolvers
{
    public static class ServiceActivator
    {
        internal static IServiceProvider _serviceProvider = null;

        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>(IServiceProvider serviceProvider)
        {
            try
            {
                return GetProvider(serviceProvider).GetService<T>();
            }
            catch (Exception exc)
            {
                throw new CoreException($"\"{typeof(T).Name} cannot be resolved\" Exception: {exc.Message}", exc);
            }
        }

        private static IServiceProvider GetProvider(IServiceProvider serviceProvider)
        {
            return serviceProvider ?? CreateScope().ServiceProvider;
        }
        public static IServiceScope CreateScope()
        {
            return _serviceProvider?.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }
    }
}