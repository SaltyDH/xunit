using System.Reflection;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: TestFramework("xunit.samples.autofac.XunitExtensibility.CustomTestFramework", "xunit.samples.autofac")]

namespace xunit.samples.autofac.XunitExtensibility
{
    public class CustomTestFramework : XunitTestFramework
    {
        public CustomTestFramework(IMessageSink messageSink)
            : base(messageSink)
        {
        }

        protected override ITestFrameworkDiscoverer CreateDiscoverer(IAssemblyInfo assemblyInfo)
        {
            Thread.Sleep(10000);
            return new CustomXunitTestFrameworkDiscoverer(assemblyInfo, SourceInformationProvider, DiagnosticMessageSink);
        }

        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
        {
            return new CustomXunitTestFrameworkExecutor(assemblyName, SourceInformationProvider, DiagnosticMessageSink);
        }
    }
}