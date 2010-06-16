namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Types;
    using Xunit;

    public sealed class ConcreteClassTestOfTFacts
    {
        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new ConcreteClassTest<int>());
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ConcreteClassTest<object>());
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new ConcreteClassTest<AbstractBaseClass1>().Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new ConcreteClassTest<string>().Check());
        }
    }
}