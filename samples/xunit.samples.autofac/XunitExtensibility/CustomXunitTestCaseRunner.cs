using System.Reflection;
using System.Threading;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.samples.autofac.XunitExtensibility
{
    public class CustomXunitTestCaseRunner : XunitTestCaseRunner
    {
        public CustomXunitTestCaseRunner(IXunitTestCase testCase, string displayName, string skipReason, object[] constructorArguments, object[] testMethodArguments, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource) 
            : base(testCase, displayName, skipReason, constructorArguments, testMethodArguments, messageBus, aggregator, cancellationTokenSource)
        {
        }

        protected override XunitTestRunner GenerateTestRunner(ITest test, MethodInfo testMethod, object[] testMethodArguments, string skipReason)
        {
            return new CustomXunitTestRunner(test, MessageBus, TestClass, ConstructorArguments, testMethod, testMethodArguments, skipReason, BeforeAfterAttributes, Aggregator, CancellationTokenSource);
        }
    }
}