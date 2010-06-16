namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Xunit;

    public sealed class InterfaceTestOfTFacts
    {
        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new InterfaceTest<ITestType>());
        }

        [Fact]
        public void ctor_Type()
        {
            Assert.NotNull(new InterfaceTest<ITestType>());
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new InterfaceTest<object>().Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new InterfaceTest<ITestType>().Check());
        }
    }
}