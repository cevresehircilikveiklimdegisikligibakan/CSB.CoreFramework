using Autofac;
using CSB.Core.Infrastructure.Extensions;

namespace CSB.Core.Infrastructure.Installers
{
    internal class CoreModuleInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterCoreInterceptors(builder);
        }

        private void RegisterCoreInterceptors(ContainerBuilder builder)
        {
            builder.RegisterInterceptors(CoreBootstrapper.Assembly);
        }
    }
}