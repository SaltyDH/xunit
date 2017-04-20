using System;
using Autofac;
using Xunit.Abstractions;

namespace xunit.samples.autofac.IoC
{
    public abstract class XunitAutofacProvider : IXunitAutofacProvider
    {
        public virtual IContainer ResolveContainer(ITest test)
        {
            var builder = ConfigureContainer();
            var container = builder.Build();
            return container;
        }

        protected virtual ILifetimeScope ResolveScope(ITest test, ITestOutputHelper helper, Action<ContainerBuilder> configureAction = null)
        {
            var container = ResolveContainer(test);

            return container.BeginLifetimeScope(builder =>
            {
                ConfigureAction(builder, helper);
                configureAction?.Invoke(builder);
            });
        }

        public abstract ILifetimeScope ResolveScope(ITest test, ITestOutputHelper helper);
        protected abstract ContainerBuilder ConfigureContainer();

        private void ConfigureAction(ContainerBuilder builder, ITestOutputHelper helper)
        {
            builder.Register(ctx => helper)
                .As<ITestOutputHelper>()
                .InstancePerLifetimeScope();
        }
    }
}