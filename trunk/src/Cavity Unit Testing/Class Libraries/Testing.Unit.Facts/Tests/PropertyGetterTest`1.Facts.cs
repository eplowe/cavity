namespace Cavity.Tests
{
    using Cavity.Types;
    using Xunit;

    public sealed class PropertyGetterTestOfTFacts
    {
        [Fact]
        public void ctor_PropertyInfo()
        {
            Assert.NotNull(new PropertyGetterTest<int>(typeof(PropertiedClass1).GetProperty("AutoBoolean")));
        }

        [Fact]
        public void is_PropertyTest()
        {
            Assert.IsAssignableFrom<PropertyTestBase>(new PropertyGetterTest<int>(typeof(PropertiedClass1).GetProperty("AutoBoolean")));
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<UnitTestException>(() => new PropertyGetterTest<string>(typeof(PropertiedClass1).GetProperty("AutoBoolean")).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new PropertyGetterTest<bool>(typeof(PropertiedClass1).GetProperty("AutoBoolean")).Check());
        }

        [Fact]
        public void prop_Expected()
        {
            var expected = typeof(PropertiedClass1).GetProperty("AutoBoolean");

            var obj = new PropertyGetterTest<int>(null)
                          {
                              Expected = expected
                          };

            var actual = obj.Expected;

            Assert.Same(expected, actual);
        }
    }
}