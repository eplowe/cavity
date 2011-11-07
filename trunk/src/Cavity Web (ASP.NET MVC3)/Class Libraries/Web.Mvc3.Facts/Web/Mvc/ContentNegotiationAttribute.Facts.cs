namespace Cavity.Web.Mvc
{
    using System;
    using Xunit;

    public sealed class ContentNegotiationAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ContentNegotiationAttribute>()
                            .DerivesFrom<Attribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsDecoratedWith<AttributeUsageAttribute>()
                            .Result);
        }

        [Fact]
        public void ctor_stringEmpty_string()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ContentNegotiationAttribute(string.Empty, "text/example"));
        }

        [Fact]
        public void ctor_stringNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new ContentNegotiationAttribute(null, "text/example"));
        }

        [Fact]
        public void ctor_string_string()
        {
            Assert.NotNull(new ContentNegotiationAttribute(".example", "text/example"));
        }

        [Fact]
        public void ctor_string_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ContentNegotiationAttribute(".example", string.Empty));
        }

        [Fact]
        public void ctor_string_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ContentNegotiationAttribute(".example", null));
        }

        [Fact]
        public void op_ToContentTypes()
        {
            const string expected = "text/example";

            foreach (var actual in new ContentNegotiationAttribute(".example", expected).ToContentTypes())
            {
                Assert.Equal(expected, actual.MediaType);
            }
        }

        [Fact]
        public void prop_Extension()
        {
            Assert.True(new PropertyExpectations<ContentNegotiationAttribute>(x => x.Extension)
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .Set("example")
                            .Set(".example")
                            .Result);
        }

        [Fact]
        public void prop_MediaTypes()
        {
            Assert.True(new PropertyExpectations<ContentNegotiationAttribute>(x => x.MediaTypes)
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .ArgumentOutOfRangeException(string.Empty)
                            .FormatException("example")
                            .Set("application/xhtml+xml")
                            .Set("*/*, text/*, text/html")
                            .Result);
        }
    }
}