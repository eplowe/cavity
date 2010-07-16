namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using Cavity.Types;
    using Xunit;

    public sealed class PropertySetterTestFacts
    {
        [Fact]
        public void ctor_PropertyInfo_object()
        {
            Assert.NotNull(new PropertySetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), true));
        }

        [Fact]
        public void is_PropertyTest()
        {
            Assert.IsAssignableFrom<PropertyTestBase>(new PropertySetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), true));
        }

        [Fact]
        public void op_Check_whenAbstract()
        {
            Assert.Throws<MissingMethodException>(() => new PropertySetterTest(typeof(AbstractBaseClass1).GetProperty("AutoAbstract"), false).Check());
        }

        [Fact]
        public void op_Check_whenException()
        {
            Assert.Throws<TargetInvocationException>(() => new PropertySetterTest(typeof(PropertiedClass1).GetProperty("ArgumentOutOfRangeExceptionValue"), string.Empty).Check());
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<ArgumentException>(() => new PropertySetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), 123).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new PropertySetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), false).Check());
        }

        [Fact]
        public void prop_Expected()
        {
            const bool expected = true;

            var obj = new PropertySetterTest(null, false)
            {
                Value = expected
            };

            var actual = obj.Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ExpectedException()
        {
            var expected = typeof(ArgumentException);

            var obj = new PropertySetterTest(null, false)
            {
                ExpectedException = expected
            };

            var actual = obj.ExpectedException;

            Assert.Same(expected, actual);
        }
    }
}