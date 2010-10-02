namespace Cavity.Xml
{
    using System;
    using Xunit;

    public sealed class XmlNamespaceFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlNamespace>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_stringEmpty_AbsoluteUri()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new XmlNamespace(string.Empty, new AbsoluteUri("http://example.com/")));
        }

        [Fact]
        public void ctor_stringNull_AbsoluteUri()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlNamespace(null, new AbsoluteUri("http://example.com/")));
        }

        [Fact]
        public void ctor_string_AbsoluteUri()
        {
            Assert.NotNull(new XmlNamespace("prefix", new AbsoluteUri("http://example.com/")));
        }

        [Fact]
        public void ctor_string_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlNamespace("prefix", null));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "xmlns:x=\"http://example.com/\"";
            var actual = new XmlNamespace("x", new AbsoluteUri("http://example.com/")).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Prefix()
        {
            Assert.NotNull(new PropertyExpectations<XmlNamespace>(p => p.Prefix)
                               .IsNotDecorated()
                               .TypeIs<string>()
                               .ArgumentNullException()
                               .ArgumentOutOfRangeException(string.Empty)
                               .Set("prefix")
                               .Result);
        }

        [Fact]
        public void prop_Uri()
        {
            Assert.NotNull(new PropertyExpectations<XmlNamespace>(p => p.Uri)
                               .IsNotDecorated()
                               .TypeIs<AbsoluteUri>()
                               .ArgumentNullException()
                               .Set(new AbsoluteUri("http://example.com/"))
                               .Result);
        }
    }
}