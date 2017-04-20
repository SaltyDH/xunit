using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.samples.autofac.XunitExtensibility
{
    public class CustomXunitTestClassRunner : XunitTestClassRunner
    {
        private readonly IDictionary<Type, object> _collectionFixtureMappings;

        public CustomXunitTestClassRunner(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, IDictionary<Type, object> collectionFixtureMappings)
            : base(testClass, @class, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource, collectionFixtureMappings)
        {
            _collectionFixtureMappings = collectionFixtureMappings;
        }

        /// <inheritdoc/>
        protected override bool TryGetConstructorArgument(ConstructorInfo constructor, int index,
            ParameterInfo parameter, out object argumentValue)
        {
            return ClassFixtureMappings.TryGetValue(parameter.ParameterType, out argumentValue)
                   || _collectionFixtureMappings.TryGetValue(parameter.ParameterType, out argumentValue)
                   || DeferDiscovery(parameter, out argumentValue);
        }

        private static bool DeferDiscovery(ParameterInfo parameter, out object argumentValue)
        {
            argumentValue = new DeferredParameter(parameter.ParameterType);
            return true;
        }
    }
}