namespace Cavity.Tests
{
    using Cavity.Types;
    using Xunit;

    public class XmlElementTestFacts
    {
        [Fact]
        public void is_AttributePropertyTest()
        {
            Assert.IsAssignableFrom<MemberTest>(new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element")));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element")));
        }

        [Fact]
        public void prop_ElementName()
        {
            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element"));

            string expected = "example";
            obj.ElementName = expected;
            string actual = obj.ElementName;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Namespace()
        {
            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element"));

            string expected = "example";
            obj.Namespace = expected;
            string actual = obj.Namespace;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("NamespacedElement"))
            {
                ElementName = "element",
                Namespace = "urn:example.org"
            };

            Assert.True(obj.Check());
        }

        [Fact]
        public void op_Check_whenXmlAttributeMissing()
        {
            var obj = new XmlElementTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"));

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenNameWrong()
        {
            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element"))
            {
                ElementName = "xxx"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenNamespaceWrong()
        {
            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element"))
            {
                ElementName = "element",
                Namespace = "xxx"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }
    }
}