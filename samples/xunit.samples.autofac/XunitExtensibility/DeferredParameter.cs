using System;

namespace xunit.samples.autofac.XunitExtensibility
{
    internal class DeferredParameter
    {
        public Type ParameterType { get; }

        public DeferredParameter(Type parameterType)
        {
            ParameterType = parameterType;
        }
    }
}