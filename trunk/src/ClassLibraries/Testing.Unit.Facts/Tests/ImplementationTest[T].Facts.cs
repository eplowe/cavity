namespace Cavity.Tests
{
    using System;
    using Cavity.Fluent;
    using Cavity.Types;
    using Xunit;

    public class ImplementationTestOfTFacts
    {
        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new ImplementationTest<int>(typeof(Interface1)));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ImplementationTest<object>(typeof(Interface1)));
        }

        [Fact]
        public void prop_Interface()
        {
            Type expected = typeof(Interface1);

            var obj = new ImplementationTest<object>(expected);

            obj.Interface = expected;
            Type actual = obj.Interface;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new ImplementationTest<object>(typeof(Interface1)).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new ImplementationTest<InterfaceClass1>(typeof(Interface1)).Check());
        }
    }
}