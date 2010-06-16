namespace Cavity.Tests
{
    using System.Reflection;
    using Cavity.Types;
    using Xunit;

    public sealed class PropertyGetterTestFacts
    {
        [Fact]
        public void is_PropertyTest()
        {
            Assert.IsAssignableFrom<PropertyTestBase>(new PropertyGetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), true));
        }

        [Fact]
        public void ctor_PropertyInfo_object()
        {
            Assert.NotNull(new PropertyGetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), true));
        }

        [Fact]
        public void prop_Expected()
        {
            var obj = new PropertyGetterTest(null as PropertyInfo, false);

            object expected = true;
            obj.Expected = expected;
            object actual = obj.Expected;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new PropertyGetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), true).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new PropertyGetterTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"), false).Check());
        }
    }
}