using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using xunit.samples.autofac.IoC;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.samples.autofac.XunitExtensibility
{
    public class CustomXunitTestRunner : XunitTestRunner
    {
        public CustomXunitTestRunner(ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments,
            MethodInfo testMethod, object[] testMethodArguments, string skipReason,
            IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes, ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason,
                beforeAfterAttributes, aggregator, cancellationTokenSource)
        {
        }

        protected override async Task<Tuple<decimal, string>> InvokeTestAsync(ExceptionAggregator aggregator)
        {
            TestOutputHelper testOutputHelper = null;
            foreach (var obj in ConstructorArguments)
            {
                testOutputHelper = obj as TestOutputHelper;
                if (testOutputHelper != null)
                    break;
            }

            if (testOutputHelper == null)
                testOutputHelper = new TestOutputHelper();

            testOutputHelper.Initialize(MessageBus, Test);
            testOutputHelper.WriteLine("Here");

            var provider = new CustomXunitAutofacProvider();
            using (var scope = provider.ResolveScope(Test, testOutputHelper))
            {
                var resolvedConstructorArguments = ResolveDeferredConstructorArguments(ConstructorArguments, scope);
                ConstructorArguments = resolvedConstructorArguments;

                var resolvedParameters = ResolveMissingParameters(scope);
                TestMethodArguments = resolvedParameters.ToArray();

                var executionTime = await InvokeTestMethodAsync(aggregator);

                var output = testOutputHelper.Output;
                testOutputHelper.Uninitialize();

                return Tuple.Create(executionTime, output);
            }
        }

        private object[] ResolveDeferredConstructorArguments(object[] constructorArguments, IComponentContext scope)
        {
            var resolvedArguments = new object[constructorArguments.Length];
            for (var index = 0; index < constructorArguments.Length; index++)
            {
                var deferredParameter = constructorArguments[index] as DeferredParameter;
                if (deferredParameter == null)
                {
                    resolvedArguments[index] = constructorArguments[index];
                    continue;
                }

                var parameterType = deferredParameter.ParameterType;
                resolvedArguments[index] = scope.Resolve(parameterType);
            }

            return resolvedArguments;
        }

        private object[] ResolveMissingParameters(IComponentContext scope)
        {
            var testMethodArguments = TestMethodArguments;
            var expectedMethodArguments = Test.TestCase.TestMethod.Method.GetParameters();

            var unspecifiedParameters = expectedMethodArguments.Skip(testMethodArguments.Length)
                .ToArray();

            var resolvedParameters = new object[unspecifiedParameters.Length];

            for (var index = 0; index < unspecifiedParameters.Length; index++)
            {
                var parameter = unspecifiedParameters[index];
                var resolvedParameter = scope.Resolve(parameter.ParameterType.ToRuntimeType());
                resolvedParameters[index] = resolvedParameter;
            }

            return TestMethodArguments.Concat(resolvedParameters).ToArray();
        }

        /// <inheritdoc />
        protected override Task<decimal> InvokeTestMethodAsync(ExceptionAggregator aggregator)
            => new XunitTestInvoker(Test, MessageBus, TestClass, ConstructorArguments, TestMethod,
                TestMethodArguments, BeforeAfterAttributes, aggregator, CancellationTokenSource).RunAsync();
    }
}