using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.samples.autofac.XunitExtensibility
{
    public class CustomXunitTestCase : XunitTestCase
    {
        /// <summary/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete(
            "Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public CustomXunitTestCase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XunitTheoryTestCase"/> class.
        /// </summary>
        /// <param name="diagnosticMessageSink">The message sink used to send diagnostic messages</param>
        /// <param name="defaultMethodDisplay">Default method display to use (when not customized).</param>
        /// <param name="testMethod">The method under test.</param>
        /// <param name="testMethodArguments"></param>
        public CustomXunitTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay,
            ITestMethod testMethod,
            object[] testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments)
        {
        }

        /// <inheritdoc/>
        public override Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            object[] constructorArguments,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
            => new CustomXunitTestCaseRunner(this, DisplayName, SkipReason, constructorArguments, TestMethodArguments, messageBus, aggregator, cancellationTokenSource).RunAsync();
    }
}