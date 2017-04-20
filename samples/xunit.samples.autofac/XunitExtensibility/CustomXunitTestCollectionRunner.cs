using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.samples.autofac.XunitExtensibility
{
    public class CustomXunitTestCollectionRunner : XunitTestCollectionRunner
    {
        private readonly IMessageSink _diagnosticMessageSink;

        public CustomXunitTestCollectionRunner(ITestCollection testCollection, IEnumerable<IXunitTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        /// <inheritdoc/>
        protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases)
            => new CustomXunitTestClassRunner(testClass, @class, testCases, _diagnosticMessageSink, MessageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), CancellationTokenSource, CollectionFixtureMappings).RunAsync();
    }
}