namespace Cavity.Web.Mvc
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Mime;
    using System.Web;
    using Moq;
    using Xunit;

    public sealed class AcceptFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Accept>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Accept());
        }

        [Fact]
        public void opImplicit_Accept_string()
        {
            const string expected = "text/html";

            Accept obj = expected;

            Assert.Equal(1, obj.ContentTypes.Count);
            Assert.Equal(expected, obj.ContentTypes[0].MediaType);
        }

        [Fact]
        public void opImplicit_Accept_stringEmpty()
        {
            Accept obj = string.Empty;

            Assert.Equal(1, obj.ContentTypes.Count);
            Assert.Equal("*/*", obj.ContentTypes[0].MediaType);
        }

        [Fact]
        public void opImplicit_Accept_stringNull()
        {
            Accept obj = null as string;

            Assert.Equal(1, obj.ContentTypes.Count);
            Assert.Equal("*/*", obj.ContentTypes[0].MediaType);
        }

        [Fact]
        public void op_FromString_string()
        {
            const string expected = "text/html";
            var actual = Accept.FromString(expected).ContentTypes[0].MediaType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringAccept()
        {
            const string value = "text/*, text/html, text/html;level=1, */*";
            var obj = Accept.FromString(value);

            Assert.Equal(3, obj.ContentTypes.Count);

            Assert.Equal("text/html", obj.ContentTypes[0].MediaType);
            Assert.Equal("text/*", obj.ContentTypes[1].MediaType);
            Assert.Equal("*/*", obj.ContentTypes[2].MediaType);
        }

        [Fact]
        public void op_FromString_stringAcceptDisordered()
        {
            const string value = "*/*, application/xhtml+xml, text/html;q=0.4";
            var obj = Accept.FromString(value);

            Assert.Equal(3, obj.ContentTypes.Count);

            Assert.Equal("application/xhtml+xml", obj.ContentTypes[0].MediaType);
            Assert.Equal("text/html", obj.ContentTypes[1].MediaType);
            Assert.Equal("*/*", obj.ContentTypes[2].MediaType);
        }

        [Fact]
        public void op_FromString_stringAcceptFirefox()
        {
            const string value = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            var obj = Accept.FromString(value);

            Assert.Equal(4, obj.ContentTypes.Count);

            Assert.Equal("text/html", obj.ContentTypes[0].MediaType);
            Assert.Equal("application/xhtml+xml", obj.ContentTypes[1].MediaType);
            Assert.Equal("application/xml", obj.ContentTypes[2].MediaType);
            Assert.Equal("*/*", obj.ContentTypes[3].MediaType);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            const string expected = "*/*";
            var actual = Accept.FromString(string.Empty).ContentTypes[0].MediaType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            const string expected = "*/*";
            var actual = Accept.FromString(null).ContentTypes[0].MediaType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringSemicolon()
        {
            const string expected = "text/html";
            var actual = Accept.FromString(expected + ";q=0.4").ContentTypes[0].MediaType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringWithSingleAsterixContentType()
        {
            Assert.Equal(4, Accept.FromString("text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2").ContentTypes.Count);
        }

        [Fact]
        public void op_Negotiate_HttpRequestBaseNull_Type()
        {
            Assert.Throws<ArgumentNullException>(() => new Accept().Negotiate(null, typeof(DummyController)));
        }

        [Fact]
        public void op_Negotiate_HttpRequestBase_Type()
        {
            var obj = Accept.FromString("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request
                .SetupGet(x => x.Path)
                .Returns("/test")
                .Verifiable();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test")
                .Verifiable();

            const string expected = "/test.xml";
            var actual = (obj.Negotiate(request.Object, typeof(DummyController)) as SeeOtherResult).Location;

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_Negotiate_HttpRequestBase_TypeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Accept().Negotiate(new Mock<HttpRequestBase>(MockBehavior.Strict).Object, typeof(string)));
        }

        [Fact]
        public void op_Negotiate_HttpRequestBase_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Accept().Negotiate(new Mock<HttpRequestBase>(MockBehavior.Strict).Object, null));
        }

        [Fact]
        public void prop_ContentTypes()
        {
            Assert.True(new PropertyExpectations<Accept>(x => x.ContentTypes)
                            .TypeIs<Collection<ContentType>>()
                            .DefaultValueIsNotNull()
                            .Result);
        }
    }
}