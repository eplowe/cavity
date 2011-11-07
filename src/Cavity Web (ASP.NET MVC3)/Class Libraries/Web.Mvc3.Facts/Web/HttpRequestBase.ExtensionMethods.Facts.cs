namespace Cavity.Web
{
    using System;
    using System.Web;
    using Moq;
    using Xunit;

    public sealed class HttpRequestBaseExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(HttpRequestBaseExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseString()
        {
            const string expected = "?querystring";

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?querystring")
                .Verifiable();

            var actual = request.Object.RawQueryString();

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseEmpty()
        {
            const string expected = "?";

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?")
                .Verifiable();

            var actual = request.Object.RawQueryString();

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseMissing()
        {
            var expected = string.Empty;

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test")
                .Verifiable();

            var actual = request.Object.RawQueryString();

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HttpRequestBase).RawQueryString());
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseString_AlphaDecimalNull()
        {
            const string expected = "?querystring";

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?querystring")
                .Verifiable();

            var actual = request.Object.RawQueryString(null);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseCastException_AlphaDecimalNull()
        {
            const string expected = "?123";

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.RawUrl).Returns("http://example.com/test?123").Verifiable();

            var actual = request.Object.RawQueryString(null);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseEmpty_AlphaDecimalNull()
        {
            var expected = string.Empty;

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?")
                .Verifiable();

            var actual = request.Object.RawQueryString(null);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseMissing_AlphaDecimalNull()
        {
            var expected = string.Empty;

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test")
                .Verifiable();

            var actual = request.Object.RawQueryString(null);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseNull_AlphaDecimalNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HttpRequestBase).RawQueryString(null));
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseString_AlphaDecimal()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&querystring".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?querystring")
                .Verifiable();

            var actual = request.Object.RawQueryString(token);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseStringToken_AlphaDecimal()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&querystring".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns(string.Concat("http://example.com/test?[123]&querystring"))
                .Verifiable();

            var actual = request.Object.RawQueryString(token);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseInvalidToken_AlphaDecimal()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&xxx&querystring".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?xxx&querystring")
                .Verifiable();

            var actual = request.Object.RawQueryString(token);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseParams_AlphaDecimal()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&a=b".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?a=b")
                .Verifiable();

            var actual = request.Object.RawQueryString(token);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseParamsToken_AlphaDecimal()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&a=b".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?[123]&a=b")
                .Verifiable();

            var actual = request.Object.RawQueryString(token);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseEmpty_AlphaDecimal()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?")
                .Verifiable();

            var actual = request.Object.RawQueryString(token);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseEmptyToken_AlphaDecimal()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?[123]")
                .Verifiable();

            var actual = request.Object.RawQueryString(token);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseMissing_AlphaDecimal()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test")
                .Verifiable();

            var actual = request.Object.RawQueryString(token);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseNull_AlphaDecimal()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HttpRequestBase).RawQueryString(AlphaDecimal.Random()));
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseString_AlphaDecimalNull_string()
        {
            var expected = string.Concat("?querystring", "&whence=%2flocation");

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?querystring")
                .Verifiable();

            var actual = request.Object.RawQueryString(null, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseString_AlphaDecimalNull_stringEmpty()
        {
            const string expected = "?querystring";

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?querystring&whence=%2freplace")
                .Verifiable();

            var actual = request.Object.RawQueryString(null, string.Empty);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseString_AlphaDecimalNull_stringNull()
        {
            const string expected = "?querystring";

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?querystring&whence=%2freplace")
                .Verifiable();

            var actual = request.Object.RawQueryString(null, null);

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseStringWhence_AlphaDecimalNull_string()
        {
            var expected = string.Concat("?querystring", "&whence=%2flocation");

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?querystring&whence=%2freplace")
                .Verifiable();

            var actual = request.Object.RawQueryString(null, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseCastException_AlphaDecimalNull_string()
        {
            const string expected = "?<123>&whence=%2flocation";

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.RawUrl).Returns("http://example.com/test?<123>").Verifiable();

            var actual = request.Object.RawQueryString(null, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseEmpty_AlphaDecimalNull_string()
        {
            const string expected = "?whence=%2flocation";

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?")
                .Verifiable();

            var actual = request.Object.RawQueryString(null, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseMissing_AlphaDecimalNull_string()
        {
            const string expected = "?whence=%2flocation";

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test")
                .Verifiable();

            var actual = request.Object.RawQueryString(null, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseNull_AlphaDecimalNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HttpRequestBase).RawQueryString(null, "/location"));
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseString_AlphaDecimal_string()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&querystring&whence=%2flocation".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?querystring")
                .Verifiable();

            var actual = request.Object.RawQueryString(token, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseStringToken_AlphaDecimal_string()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&querystring&whence=%2flocation".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?[123]&querystring")
                .Verifiable();

            var actual = request.Object.RawQueryString(token, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseInvalidToken_AlphaDecimal_string()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&xxx&querystring&whence=%2flocation".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?xxx&querystring")
                .Verifiable();

            var actual = request.Object.RawQueryString(token, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseParams_AlphaDecimal_string()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&a=b&whence=%2flocation".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?a=b")
                .Verifiable();

            var actual = request.Object.RawQueryString(token, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseParamsToken_AlphaDecimal_string()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&a=b&whence=%2flocation".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?[123]&a=b")
                .Verifiable();

            var actual = request.Object.RawQueryString(token, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseEmpty_AlphaDecimal_string()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&whence=%2flocation".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?")
                .Verifiable();

            var actual = request.Object.RawQueryString(token, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseEmptyToken_AlphaDecimal_string()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&whence=%2flocation".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?[]")
                .Verifiable();

            var actual = request.Object.RawQueryString(token, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQuery_HttpRequestBaseMissing_AlphaDecimal_string()
        {
            var token = AlphaDecimal.Random();
            var expected = "?[{0}]&whence=%2flocation".FormatWith(token);

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test")
                .Verifiable();

            var actual = request.Object.RawQueryString(token, "/location");

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_RawQueryString_HttpRequestBaseNull_AlphaDecimal_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HttpRequestBase).RawQueryString(AlphaDecimal.Random(), "/location"));
        }

        [Fact]
        public void op_Token_HttpRequestBase()
        {
            var expected = AlphaDecimal.Random();

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?[{0}]".FormatWith(expected))
                .Verifiable();

            var actual = request.Object.Token();

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_Token_HttpRequestBase_whenRawUrlInvalid()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?123")
                .Verifiable();

            Assert.Null(request.Object.Token());

            request.VerifyAll();
        }

        [Fact]
        public void op_Token_HttpRequestBase_whenRawUrlWhence()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?whence=%2Fsome-place")
                .Verifiable();

            Assert.Null(request.Object.Token());

            request.VerifyAll();
        }

        [Fact]
        public void op_Token_HttpRequestBase_whenRawUrlEmpty()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?")
                .Verifiable();

            Assert.Null(request.Object.Token());

            request.VerifyAll();
        }

        [Fact]
        public void op_Token_HttpRequestBase_whenRawUrlMissing()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test")
                .Verifiable();

            Assert.Null(request.Object.Token());

            request.VerifyAll();
        }

        [Fact]
        public void op_Token_HttpRequestBase_whenRawUrlParams()
        {
            var expected = AlphaDecimal.Random();

            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?[{0}]&a=b".FormatWith(expected))
                .Verifiable();

            var actual = request.Object.Token();

            Assert.Equal(expected, actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_Token_HttpRequestBase_whenRawUrlParamsPrefixed()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.RawUrl)
                .Returns("http://example.com/test?a=b&")
                .Verifiable();

            Assert.Null(request.Object.Token());

            request.VerifyAll();
        }

        [Fact]
        public void op_Token_HttpRequestBaseNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HttpRequestBase).Token());
        }
    }
}