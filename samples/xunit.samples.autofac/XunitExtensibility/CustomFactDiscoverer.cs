using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.samples.autofac.XunitExtensibility
{
    public class CustomFactDiscoverer : FactDiscoverer
    {
        private readonly IMessageSink _diagnosticMessageSink;

        public CustomFactDiscoverer(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        protected override IXunitTestCase CreateTestCase(ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            return new CustomXunitTestCase(_diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod);
        }
    }
}