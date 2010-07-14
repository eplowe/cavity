namespace Cavity.Tests
{
    using System;
    using Cavity.Fluent;
    using Cavity.Types;
    using Xunit;

    public sealed class BaseClassTestOfTFacts
    {
        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new BaseClassTest<int>(typeof(int)));
        }

        [Fact]
        public void ctor_Type()
        {
            Assert.NotNull(new BaseClassTest<object>(typeof(int)));
        }

        [Fact]
        public void prop_Is()
        {
            var expected = typeof(string);

            var obj = new BaseClassTest<object>(typeof(int))
            {
                Is = expected
            };

            var actual = obj.Is;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Is_get()
        {
            var expected = typeof(int);

            var obj = new BaseClassTest<object>(expected);

            var actual = obj.Is;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new BaseClassTest<object>(typeof(string)).Check());
        }

        [Fact]
        public void op_Check_whenIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BaseClassTest<object>(null).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new BaseClassTest<DerivedClass1>(typeof(Class1)).Check());
        }
    }
}