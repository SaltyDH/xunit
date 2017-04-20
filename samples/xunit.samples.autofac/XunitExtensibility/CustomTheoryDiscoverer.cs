using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.samples.autofac.XunitExtensibility
{
    public class CustomTheoryDiscoverer : TheoryDiscoverer
    {
        public CustomTheoryDiscoverer(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForDataRow(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod,
            IAttributeInfo theoryAttribute, object[] dataRow)
        {
            return new [] { new CustomXunitTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod, dataRow) };
        }
    }
}