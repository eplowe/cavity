namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Types;
    using Xunit;

    public sealed class ImplementationTestOfTFacts
    {
        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ImplementationTest<object>(typeof(IInterface1)));
        }

        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new ImplementationTest<int>(typeof(IInterface1)));
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<UnitTestException>(() => new ImplementationTest<object>(typeof(IInterface1)).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new ImplementationTest<InterfacedClass1>(typeof(IInterface1)).Check());
        }

        [Fact]
        public void op_Check_whenUnexpectedInterface()
        {
            Assert.Throws<UnitTestException>(() => new ImplementationTest<InterfacedClass1>(null).Check());
        }

        [Fact]
        public void prop_Interface()
        {
            var expected = typeof(IInterface1);

            var obj = new ImplementationTest<object>(expected)
            {
                Interface = expected
            };

            var actual = obj.Interface;

            Assert.Same(expected, actual);
        }
    }
}