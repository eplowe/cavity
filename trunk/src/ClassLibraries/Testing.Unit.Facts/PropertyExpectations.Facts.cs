namespace Cavity
{
    using System;
    using System.Reflection;
    using Cavity.Types;
    using Xunit;

    public class PropertyExpectationsFacts
    {
        [Fact]
        public void ctor_PropertyInfo()
        {
            Assert.NotNull(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoBoolean")));
        }

        [Fact]
        public void ctor_PropertyInfoNull()
        {
            Assert.NotNull(new PropertyExpectations(null as PropertyInfo));
        }

        [Fact]
        public void prop_Property()
        {
            var obj = new PropertyExpectations(null as PropertyInfo);

            PropertyInfo expected = typeof(PropertiedClass1).GetProperty("AutoBoolean");
            obj.Property = expected;
            PropertyInfo actual = obj.Property;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Result_whenAuto()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoString"))
                .IsAutoProperty<string>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenAutoBoolean()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoBoolean"))
                .TypeIs<bool>()
                .DefaultValueIs(false)
                .DefaultValueIsNotNull()
                .Set<bool>()
                .Set(true)
                .Result);
        }

        [Fact]
        public void prop_Result_whenAutoString()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoString"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .Set<string>()
                .Set(string.Empty)
                .Result);
        }

        [Fact]
        public void op_ArgumentNullException()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("ArgumentNullExceptionValue"))
                .ArgumentNullException()
                .Result);
        }

        [Fact]
        public void op_ArgumentOutOfRangeException_object()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("ArgumentOutOfRangeExceptionValue"))
                .ArgumentOutOfRangeException(string.Empty)
                .Result);
        }

        [Fact]
        public void op_FormatException_object()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("FormatExceptionValue"))
                .FormatException(string.Empty)
                .Result);
        }

        [Fact]
        public void op_Exception_object_Type()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("ArgumentExceptionValue"))
                .Exception(string.Empty, typeof(ArgumentException))
                .Result);
        }

        [Fact]
        public void op_Exception_object_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoBoolean"))
                .Exception(string.Empty, null as Type));
        }

        [Fact]
        public void op_Exception_object_TypeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoBoolean"))
                .Exception(string.Empty, typeof(int)));
        }
    }
}