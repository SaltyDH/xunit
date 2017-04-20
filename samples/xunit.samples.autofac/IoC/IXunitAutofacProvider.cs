using System;
using Autofac;
using Xunit.Abstractions;

namespace xunit.samples.autofac.IoC
{
    public interface IXunitAutofacProvider
    {
        ILifetimeScope ResolveScope(ITest test, ITestOutputHelper helper);
        IContainer ResolveContainer(ITest test);
    }
}