namespace Cavity.Net
{
    using System;
    using System.Net;
    using Xunit;

    public sealed class HttpStatusLineFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpStatusLine>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_HttpStatusCode()
        {
            Assert.NotNull(new HttpStatusLine(HttpStatusCode.OK));
        }

        [Fact]
        public void ctor_HttpVersionNull_HttpStatusCode()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpStatusLine(null, HttpStatusCode.OK));
        }

        [Fact]
        public void ctor_HttpVersionNull_int_string()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpStatusLine(null, 200, "OK"));
        }

        [Fact]
        public void ctor_HttpVersion_HttpStatusCode()
        {
            Assert.NotNull(new HttpStatusLine("HTTP/1.1", HttpStatusCode.OK));
        }

        [Fact]
        public void ctor_HttpVersion_int_string()
        {
            Assert.NotNull(new HttpStatusLine("HTTP/1.1", 200, "OK"));
        }

        [Fact]
        public void ctor_HttpVersion_int_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpStatusLine("HTTP/1.1", 200, string.Empty));
        }

        [Fact]
        public void ctor_HttpVersion_int_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpStatusLine("HTTP/1.1", 200, null));
        }

        [Fact]
        public void ctor_int_string()
        {
            Assert.NotNull(new HttpStatusLine(200, "OK"));
        }

        [Fact]
        public void opImplicit_string_RequestLine()
        {
            const string expected = "HTTP/1.1 200 OK";
            string actual = new HttpStatusLine(HttpStatusCode.OK);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new HttpStatusLine(HttpStatusCode.SeeOther);
            var actual = HttpStatusLine.FromString("HTTP/1.1 303 See Other");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpStatusLine.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpStatusLine.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenCodeMissing()
        {
            Assert.Throws<FormatException>(() => HttpStatusLine.FromString("HTTP/1.1 OK"));
        }

        [Fact]
        public void op_FromString_string_whenReasonMissing()
        {
            var expected = new HttpStatusLine(HttpStatusCode.OK);
            var actual = HttpStatusLine.FromString("HTTP/1.1 200");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode100()
        {
            const string expected = "Continue";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)100);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode101()
        {
            const string expected = "Switching Protocols";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)101);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode200()
        {
            const string expected = "OK";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)200);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode201()
        {
            const string expected = "Created";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)201);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode202()
        {
            const string expected = "Accepted";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)202);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode203()
        {
            const string expected = "Non-Authoritative Information";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)203);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode204()
        {
            const string expected = "No Content";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)204);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode205()
        {
            const string expected = "Reset Content";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)205);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode206()
        {
            const string expected = "Partial Content";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)206);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode300()
        {
            const string expected = "Multiple Choices";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)300);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode301()
        {
            const string expected = "Moved Permanently";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)301);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode302()
        {
            const string expected = "Found";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)302);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode303()
        {
            const string expected = "See Other";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)303);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode304()
        {
            const string expected = "Not Modified";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)304);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode305()
        {
            const string expected = "Use Proxy";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)305);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode307()
        {
            const string expected = "Temporary Redirect";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)307);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode400()
        {
            const string expected = "Bad Request";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)400);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode401()
        {
            const string expected = "Unauthorized";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)401);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode402()
        {
            const string expected = "Payment Required";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)402);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode403()
        {
            const string expected = "Forbidden";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)403);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode404()
        {
            const string expected = "Not Found";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)404);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode405()
        {
            const string expected = "Method Not Allowed";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)405);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode406()
        {
            const string expected = "Not Acceptable";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)406);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode407()
        {
            const string expected = "Proxy Authentication Required";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)407);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode408()
        {
            const string expected = "Request Time-out";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)408);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode409()
        {
            const string expected = "Conflict";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)409);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode410()
        {
            const string expected = "Gone";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)410);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode411()
        {
            const string expected = "Length Required";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)411);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode412()
        {
            const string expected = "Precondition Failed";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)412);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode413()
        {
            const string expected = "Request Entity Too Large";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)413);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode414()
        {
            const string expected = "Request-URI Too Large";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)414);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode415()
        {
            const string expected = "Unsupported Media Type";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)415);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode416()
        {
            const string expected = "Requested range not satisfiable";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)416);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode417()
        {
            const string expected = "Expectation Failed";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)417);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode500()
        {
            const string expected = "Internal Server Error";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)500);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode501()
        {
            const string expected = "Not Implemented";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)501);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode502()
        {
            const string expected = "Bad Gateway";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)502);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode503()
        {
            const string expected = "Service Unavailable";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)503);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode504()
        {
            const string expected = "Gateway Time-out";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)504);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode505()
        {
            const string expected = "HTTP Version not supported";
            var actual = HttpStatusLine.ReasonPhase((HttpStatusCode)505);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReasonPhrase_HttpStatusCode999()
        {
            Assert.Null(HttpStatusLine.ReasonPhase((HttpStatusCode)999));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "HTTP/1.0 200 OK";
            var actual = new HttpStatusLine("HTTP/1.0", 200, "OK").ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Code()
        {
            Assert.True(new PropertyExpectations<HttpStatusLine>(x => x.Code)
                            .IsAutoProperty<int>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Reason()
        {
            Assert.True(new PropertyExpectations<HttpStatusLine>(x => x.Reason)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Version()
        {
            Assert.True(new PropertyExpectations<HttpStatusLine>(x => x.Version)
                            .TypeIs<HttpVersion>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}