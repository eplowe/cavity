namespace Cavity.Tests
{
    using System.Reflection;
    using Cavity.Types;
    using Xunit;

    public class PropertyGetterTestOfTFacts
    {
        [Fact]
        public void is_PropertyTest()
        {
            Assert.IsAssignableFrom<PropertyTest>(new PropertyGetterTest<int>(typeof(PropertiedClass1).GetProperty("AutoBoolean")));
        }

        [Fact]
        public void ctor_PropertyInfo()
        {
            Assert.NotNull(new PropertyGetterTest<int>(typeof(PropertiedClass1).GetProperty("AutoBoolean")));
        }

        [Fact]
        public void prop_Expected()
        {
            var obj = new PropertyGetterTest<int>(null as PropertyInfo);

            object expected = typeof(PropertiedClass1).GetProperty("AutoBoolean");
            obj.Expected = expected;
            object actual = obj.Expected;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new PropertyGetterTest<string>(typeof(PropertiedClass1).GetProperty("AutoBoolean")).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new PropertyGetterTest<bool>(typeof(PropertiedClass1).GetProperty("AutoBoolean")).Check());
        }
    }
}