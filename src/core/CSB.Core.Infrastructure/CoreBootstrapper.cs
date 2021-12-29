using Autofac;
using CSB.Core.Infrastructure.Installers;
using System.Reflection;

namespace CSB.Core.Infrastructure
{
    public class CoreBootstrapper
    {
        public static void RegisterCoreBootstrapper(ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModuleInstaller());
        }

        public static Assembly Assembly { get; } = typeof(CoreBootstrapper).Assembly;
    }
}