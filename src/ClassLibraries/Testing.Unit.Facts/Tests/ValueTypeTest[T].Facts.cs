namespace Cavity.Tests
{
    using System;
    using Cavity.Fluent;
    using Xunit;

    public class ValueTypeTestOfTFacts
    {
        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new ValueTypeTest<DateTime>());
        }

        [Fact]
        public void ctor_Type()
        {
            Assert.NotNull(new ValueTypeTest<DateTime>());
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new ValueTypeTest<object>().Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new ValueTypeTest<DateTime>().Check());
        }
    }
}