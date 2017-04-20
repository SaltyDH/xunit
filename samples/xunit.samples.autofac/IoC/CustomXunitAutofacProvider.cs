using System;
using Autofac;
using Xunit.Abstractions;

namespace xunit.samples.autofac.IoC
{
    public class CustomXunitAutofacProvider : XunitAutofacProvider
    {
        protected override ContainerBuilder ConfigureContainer()
        {
            return new ContainerBuilder();
        }

        public override ILifetimeScope ResolveScope(ITest test, ITestOutputHelper helper)
        {
            return base.ResolveScope(test, helper, ConfigureAction);
        }

        private void ConfigureAction(ContainerBuilder builder)
        {
            builder.RegisterType<Dependency1>()
                .As<IDependency>()
                .InstancePerDependency();
        }
    }
}