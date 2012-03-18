namespace Cavity.Tests
{
    using Cavity.Types;

    using Xunit;

    public sealed class XmlElementTestFacts
    {
        [Fact]
        public void ctor()
        {
            Assert.NotNull(new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element")));
        }

        [Fact]
        public void is_AttributePropertyTest()
        {
            Assert.IsAssignableFrom<MemberTestBase>(new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element")));
        }

        [Fact]
        public void op_Check_whenNameWrong()
        {
            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element"))
                          {
                              ElementName = "xxx"
                          };

            Assert.Throws<UnitTestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenNamespaceWrong()
        {
            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element"))
                          {
                              ElementName = "element", 
                              Namespace = "xxx"
                          };

            Assert.Throws<UnitTestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("NamespaceElement"))
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

            Assert.Throws<UnitTestException>(() => obj.Check());
        }

        [Fact]
        public void prop_ElementName()
        {
            const string expected = "example";

            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element"))
                          {
                              ElementName = expected
                          };

            var actual = obj.ElementName;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Namespace()
        {
            const string expected = "example";

            var obj = new XmlElementTest(typeof(XmlSerializableClass1).GetProperty("Element"))
                          {
                              Namespace = expected
                          };

            var actual = obj.Namespace;

            Assert.Same(expected, actual);
        }
    }
}