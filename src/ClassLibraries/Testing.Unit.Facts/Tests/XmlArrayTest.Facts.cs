namespace Cavity.Tests
{
    using Cavity.Types;
    using Xunit;

    public sealed class XmlArrayTestFacts
    {
        [Fact]
        public void is_AttributePropertyTest()
        {
            Assert.IsAssignableFrom<MemberTestBase>(new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1")));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1")));
        }

        [Fact]
        public void prop_ArrayElementName()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"));

            string expected = "example";
            obj.ArrayElementName = expected;
            string actual = obj.ArrayElementName;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_ArrayItemElementName()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"));

            string expected = "example";
            obj.ArrayItemElementName = expected;
            string actual = obj.ArrayItemElementName;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"))
            {
                ArrayElementName = "array1",
                ArrayItemElementName = "item1"
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
                ArrayElementName = "array2",
                ArrayItemElementName = "item2"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenNameWrong()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"))
            {
                ArrayElementName = "xxx"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenItemsWrong()
        {
            var obj = new XmlArrayTest(typeof(XmlSerializableClass1).GetProperty("Array1"))
            {
                ArrayElementName = "array1",
                ArrayItemElementName = "xxx"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }
    }
}