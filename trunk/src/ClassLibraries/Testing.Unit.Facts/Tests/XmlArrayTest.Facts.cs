namespace Cavity.Tests
{
    using Cavity.Types;
    using Xunit;

    public class XmlArrayTestFacts
    {
        [Fact]
        public void is_AttributePropertyTest()
        {
            Assert.IsAssignableFrom<MemberTest>(new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1")));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1")));
        }

        [Fact]
        public void prop_Name()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"));

            string expected = "example";
            obj.Name = expected;
            string actual = obj.Name;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Items()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"));

            string expected = "example";
            obj.Items = expected;
            string actual = obj.Items;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"))
            {
                Name = "array1",
                Items = "item1"
            };

            Assert.True(obj.Check());
        }

        [Fact]
        public void op_Check_whenXmlArrayMissing()
        {
            var obj = new XmlArrayTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"));

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenXmlArrayItemMissing()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array2"))
            {
                Name = "array2",
                Items = "item2"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenNameWrong()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"))
            {
                Name = "xxx"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenItemsWrong()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"))
            {
                Name = "array1",
                Items = "xxx"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }
    }
}