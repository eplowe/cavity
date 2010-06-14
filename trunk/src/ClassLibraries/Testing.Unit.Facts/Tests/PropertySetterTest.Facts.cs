namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using Cavity.Types;
    using Xunit;

    public class PropertySetterTestFacts
    {
        [Fact]
        public void is_PropertyTest()
        {
            Assert.IsAssignableFrom<PropertyTest>(new PropertySetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), true));
        }

        [Fact]
        public void ctor_PropertyInfo_object()
        {
            Assert.NotNull(new PropertySetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), true));
        }

        [Fact]
        public void prop_Expected()
        {
            var obj = new PropertySetterTest(null as PropertyInfo, false);

            object expected = true;
            obj.Value = expected;
            object actual = obj.Value;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_ExpectedException()
        {
            var obj = new PropertySetterTest(null as PropertyInfo, false);

            Type expected = typeof(ArgumentException);
            obj.ExpectedException = expected;
            Type actual = obj.ExpectedException;

            Assert.Same(expected, actual);
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
    }
}