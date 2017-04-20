using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.samples.autofac.XunitExtensibility
{
    /// <summary>
    /// The implementation of <see cref="ITestFrameworkDiscoverer"/> that supports discovery
    /// of unit tests linked against xunit.core.dll, using xunit.execution.dll.
    /// </summary>
    public class CustomXunitTestFrameworkDiscoverer : XunitTestFrameworkDiscoverer
    {
        public CustomXunitTestFrameworkDiscoverer(IAssemblyInfo assemblyInfo, ISourceInformationProvider sourceProvider, IMessageSink diagnosticMessageSink)
            : base(assemblyInfo, sourceProvider, diagnosticMessageSink)
        {
            DiscovererTypeCache.Add(typeof(TheoryAttribute), typeof(CustomTheoryDiscoverer));
            DiscovererTypeCache.Add(typeof(FactAttribute), typeof(CustomFactDiscoverer));
        }
    }
}