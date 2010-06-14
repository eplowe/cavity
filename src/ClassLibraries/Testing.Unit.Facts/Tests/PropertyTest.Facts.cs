namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using Cavity.Types;
    using Xunit;

    public class PropertyTestFacts
    {
        [Fact]
        public void ctor_PropertyInfo_object()
        {
            Assert.NotNull(new DerivedPropertyTest(typeof(PropertiedClass1).GetProperty("AutoBoolean")));
        }

        [Fact]
        public void prop_Property()
        {
            var obj = new DerivedPropertyTest(null as PropertyInfo);

            PropertyInfo expected = typeof(PropertiedClass1).GetProperty("AutoBoolean");
            obj.Property = expected;
            PropertyInfo actual = obj.Property;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check()
        {
            Assert.Throws<NotImplementedException>(() => new DerivedPropertyTest(typeof(PropertiedClass1).GetProperty("AutoBoolean")).Check());
        }
    }
}