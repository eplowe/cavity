namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Types;
    using Xunit;

    public class XmlRootDecorationTestOfTFacts
    {
        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new XmlRootDecorationTest<int>("name"));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new XmlRootDecorationTest<object>("name"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new XmlRootDecorationTest<object>(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new XmlRootDecorationTest<object>(null as string));
        }

        [Fact]
        public void ctor_string_string()
        {
            Assert.NotNull(new XmlRootDecorationTest<object>("name", "namespace"));
        }

        [Fact]
        public void ctor_stringEmpty_string()
        {
            Assert.NotNull(new XmlRootDecorationTest<object>(string.Empty, "namespace"));
        }

        [Fact]
        public void ctor_stringNull_string()
        {
            Assert.NotNull(new XmlRootDecorationTest<object>(null as string, "namespace"));
        }

        [Fact]
        public void ctor_string_stringEmpty()
        {
            Assert.NotNull(new XmlRootDecorationTest<object>("name", string.Empty));
        }

        [Fact]
        public void ctor_string_stringNull()
        {
            Assert.NotNull(new XmlRootDecorationTest<object>("name", null as string));
        }

        [Fact]
        public void prop_Name()
        {
            var obj = new XmlRootDecorationTest<object>("foo");

            string expected = "bar";
            obj.Name = expected;
            string actual = obj.Name;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void prop_Namespace()
        {
            var obj = new XmlRootDecorationTest<object>("name");

            string expected = "namespace";
            obj.Namespace = expected;
            string actual = obj.Namespace;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void op_Check()
        {
            Assert.True(new XmlRootDecorationTest<XmlSerializableClass1>("root", "urn:example.net").Check());
        }

        [Fact]
        public void op_Check_whenUndecorated()
        {
            Assert.Throws<TestException>(() => new XmlRootDecorationTest<object>("name").Check());
        }

        [Fact]
        public void op_Check_whenNamespaceIsWrong()
        {
            Assert.Throws<TestException>(() => new XmlRootDecorationTest<XmlSerializableClass1>("root").Check());
        }
    }
}