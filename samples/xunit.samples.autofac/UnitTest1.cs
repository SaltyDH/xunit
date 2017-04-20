using Xunit;
using Xunit.Abstractions;

namespace xunit.samples.autofac
{
    public class UnitTest1
    {
        private readonly IDependency _classDependency;
        private readonly ITestOutputHelper _classHelper;

        public UnitTest1(IDependency dep, ITestOutputHelper classHelper)
        {
            _classDependency = dep;
            _classHelper = classHelper;
        }

        [Fact]
        public void FactTest()
        {
            _classHelper.WriteLine(_classDependency.DisplayText);
            _classHelper.WriteLine("ClassHelperInFact");
        }

        [Theory]
        [InlineData("Test1")]
        [InlineData("Test2")]
        public void PartialTheory(string test, IDependency dep, ITestOutputHelper helper)
        {
            helper.WriteLine(test);
            helper.WriteLine(dep.DisplayText);
            helper.WriteLine(_classDependency.DisplayText);
            _classHelper.WriteLine("ClassHelperInTheory");

            Assert.NotEqual(_classDependency, dep);
        }
    }
}
