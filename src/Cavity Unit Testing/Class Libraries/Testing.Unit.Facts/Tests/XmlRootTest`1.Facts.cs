namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Types;

    using Xunit;

    public sealed class XmlRootTestOfTFacts
    {
        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new XmlRootTest<object>("name"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new XmlRootTest<object>(string.Empty));
        }

        [Fact]
        public void ctor_stringEmpty_string()
        {
            Assert.NotNull(new XmlRootTest<object>(string.Empty, "namespace"));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new XmlRootTest<object>(null));
        }

        [Fact]
        public void ctor_stringNull_string()
        {
            Assert.NotNull(new XmlRootTest<object>(null, "namespace"));
        }

        [Fact]
        public void ctor_string_string()
        {
            Assert.NotNull(new XmlRootTest<object>("name", "namespace"));
        }

        [Fact]
        public void ctor_string_stringEmpty()
        {
            Assert.NotNull(new XmlRootTest<object>("name", string.Empty));
        }

        [Fact]
        public void ctor_string_stringNull()
        {
            Assert.NotNull(new XmlRootTest<object>("name", null));
        }

        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new XmlRootTest<int>("name"));
        }

        [Fact]
        public void op_Check()
        {
            Assert.True(new XmlRootTest<XmlSerializableClass1>("root", "urn:example.net").Check());
        }

        [Fact]
        public void op_Check_whenNameWrong()
        {
            Assert.Throws<UnitTestException>(() => new XmlRootTest<XmlSerializableClass1>("xxx").Check());
        }

        [Fact]
        public void op_Check_whenNamespaceIsWrong()
        {
            Assert.Throws<UnitTestException>(() => new XmlRootTest<XmlSerializableClass1>("root").Check());
        }

        [Fact]
        public void op_Check_whenUndecorated()
        {
            Assert.Throws<UnitTestException>(() => new XmlRootTest<object>("name").Check());
        }

        [Fact]
        public void prop_ElementName()
        {
            const string expected = "bar";

            var obj = new XmlRootTest<object>("foo")
                          {
                              ElementName = expected
                          };

            var actual = obj.ElementName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Namespace()
        {
            const string expected = "namespace";

            var obj = new XmlRootTest<object>("name")
                          {
                              Namespace = expected
                          };

            var actual = obj.Namespace;

            Assert.Equal(expected, actual);
        }
    }
}