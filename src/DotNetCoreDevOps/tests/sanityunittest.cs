using Xunit;
using Xunit.Abstractions;

namespace MyFirstDotNetCoreTests
{
    public class SanityUnitTest
    {
        private readonly ITestOutputHelper output;

        public SanityUnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PassingTest()
        {
            output.WriteLine("This is output from Passing");
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            output.WriteLine("This is output from Failing");
            Assert.NotEqual(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}