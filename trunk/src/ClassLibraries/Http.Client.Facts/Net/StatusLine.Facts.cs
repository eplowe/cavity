namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class StatusLineFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<StatusLine>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_HttpVersion_int_string()
        {
            Assert.NotNull(new StatusLine(new HttpVersion(1, 0), 200, "OK"));
        }

        [Fact]
        public void opImplicit_StatusLine_string()
        {
            var expected = new StatusLine("HTTP/1.1", 200, "OK");
            StatusLine actual = "HTTP/1.1 200 OK";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_StatusLine_stringNull()
        {
            StatusLine obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<FormatException>(() => StatusLine.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => StatusLine.FromString(null));
        }

        [Fact]
        public void op_FromString_string_when100Continue()
        {
            var expected = new StatusLine("HTTP/1.1", 100, "Continue");
            var actual = StatusLine.FromString("HTTP/1.1 100 Continue");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when101SwitchingProtocols()
        {
            var expected = new StatusLine("HTTP/1.1", 101, "Switching Protocols");
            var actual = StatusLine.FromString("HTTP/1.1 101 Switching Protocols");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when200OK()
        {
            var expected = new StatusLine("HTTP/1.1", 200, "OK");
            var actual = StatusLine.FromString("HTTP/1.1 200 OK");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when201Created()
        {
            var expected = new StatusLine("HTTP/1.1", 201, "Created");
            var actual = StatusLine.FromString("HTTP/1.1 201 Created");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when202Accepted()
        {
            var expected = new StatusLine("HTTP/1.1", 202, "Accepted");
            var actual = StatusLine.FromString("HTTP/1.1 202 Accepted");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when203NonAuthoritativeInformation()
        {
            var expected = new StatusLine("HTTP/1.1", 203, "Non-Authoritative Information");
            var actual = StatusLine.FromString("HTTP/1.1 203 Non-Authoritative Information");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when204NoContent()
        {
            var expected = new StatusLine("HTTP/1.1", 204, "No Content");
            var actual = StatusLine.FromString("HTTP/1.1 204 No Content");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when205ResetContent()
        {
            var expected = new StatusLine("HTTP/1.1", 205, "Reset Content");
            var actual = StatusLine.FromString("HTTP/1.1 205 Reset Content");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when206PartialContent()
        {
            var expected = new StatusLine("HTTP/1.1", 206, "Partial Content");
            var actual = StatusLine.FromString("HTTP/1.1 206 Partial Content");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when300MultipleChoices()
        {
            var expected = new StatusLine("HTTP/1.1", 300, "Multiple Choices");
            var actual = StatusLine.FromString("HTTP/1.1 300 Multiple Choices");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when301MovedPermanently()
        {
            var expected = new StatusLine("HTTP/1.1", 301, "Moved Permanently");
            var actual = StatusLine.FromString("HTTP/1.1 301 Moved Permanently");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when302Found()
        {
            var expected = new StatusLine("HTTP/1.1", 302, "Found");
            var actual = StatusLine.FromString("HTTP/1.1 302 Found");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when303SeeOther()
        {
            var expected = new StatusLine("HTTP/1.1", 303, "See Other");
            var actual = StatusLine.FromString("HTTP/1.1 303 See Other");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when304NotModified()
        {
            var expected = new StatusLine("HTTP/1.1", 304, "Not Modified");
            var actual = StatusLine.FromString("HTTP/1.1 304 Not Modified");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when305UseProxy()
        {
            var expected = new StatusLine("HTTP/1.1", 305, "Use Proxy");
            var actual = StatusLine.FromString("HTTP/1.1 305 Use Proxy");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when307TemporaryRedirect()
        {
            var expected = new StatusLine("HTTP/1.1", 307, "Temporary Redirect");
            var actual = StatusLine.FromString("HTTP/1.1 307 Temporary Redirect");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when400BadRequest()
        {
            var expected = new StatusLine("HTTP/1.1", 400, "Bad Request");
            var actual = StatusLine.FromString("HTTP/1.1 400 Bad Request");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when401Unauthorized()
        {
            var expected = new StatusLine("HTTP/1.1", 401, "Unauthorized");
            var actual = StatusLine.FromString("HTTP/1.1 401 Unauthorized");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when402PaymentRequired()
        {
            var expected = new StatusLine("HTTP/1.1", 402, "Payment Required");
            var actual = StatusLine.FromString("HTTP/1.1 402 Payment Required");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when403Forbidden()
        {
            var expected = new StatusLine("HTTP/1.1", 403, "Forbidden");
            var actual = StatusLine.FromString("HTTP/1.1 403 Forbidden");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when404NotFound()
        {
            var expected = new StatusLine("HTTP/1.1", 404, "Not Found");
            var actual = StatusLine.FromString("HTTP/1.1 404 Not Found");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when405MethodNotAllowed()
        {
            var expected = new StatusLine("HTTP/1.1", 405, "Method Not Allowed");
            var actual = StatusLine.FromString("HTTP/1.1 405 Method Not Allowed");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when406NotAcceptable()
        {
            var expected = new StatusLine("HTTP/1.1", 406, "Not Acceptable");
            var actual = StatusLine.FromString("HTTP/1.1 406 Not Acceptable");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when407ProxyAuthenticationRequired()
        {
            var expected = new StatusLine("HTTP/1.1", 407, "Proxy Authentication Required");
            var actual = StatusLine.FromString("HTTP/1.1 407 Proxy Authentication Required");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when408RequestTimeout()
        {
            var expected = new StatusLine("HTTP/1.1", 408, "Request Time-out");
            var actual = StatusLine.FromString("HTTP/1.1 408 Request Time-out");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when409Conflict()
        {
            var expected = new StatusLine("HTTP/1.1", 409, "Conflict");
            var actual = StatusLine.FromString("HTTP/1.1 409 Conflict");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when410Gone()
        {
            var expected = new StatusLine("HTTP/1.1", 410, "Gone");
            var actual = StatusLine.FromString("HTTP/1.1 410 Gone");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when411LengthRequired()
        {
            var expected = new StatusLine("HTTP/1.1", 411, "Length Required");
            var actual = StatusLine.FromString("HTTP/1.1 411 Length Required");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when412PreconditionFailed()
        {
            var expected = new StatusLine("HTTP/1.1", 412, "Precondition Failed");
            var actual = StatusLine.FromString("HTTP/1.1 412 Precondition Failed");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when413RequestEntityTooLarge()
        {
            var expected = new StatusLine("HTTP/1.1", 413, "Request Entity Too Large");
            var actual = StatusLine.FromString("HTTP/1.1 413 Request Entity Too Large");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when414RequestUriTooLarge()
        {
            var expected = new StatusLine("HTTP/1.1", 414, "Request-URI Too Large");
            var actual = StatusLine.FromString("HTTP/1.1 414 Request-URI Too Large");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when415UnsupportedMediaType()
        {
            var expected = new StatusLine("HTTP/1.1", 415, "Unsupported Media Type");
            var actual = StatusLine.FromString("HTTP/1.1 415 Unsupported Media Type");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when416RequestedRangeNotSatisfiable()
        {
            var expected = new StatusLine("HTTP/1.1", 416, "Requested range not satisfiable");
            var actual = StatusLine.FromString("HTTP/1.1 416 Requested range not satisfiable");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when417ExpectationFailed()
        {
            var expected = new StatusLine("HTTP/1.1", 417, "Expectation Failed");
            var actual = StatusLine.FromString("HTTP/1.1 417 Expectation Failed");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when500InternalServerError()
        {
            var expected = new StatusLine("HTTP/1.1", 500, "Internal Server Error");
            var actual = StatusLine.FromString("HTTP/1.1 500 Internal Server Error");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when501NotImplemented()
        {
            var expected = new StatusLine("HTTP/1.1", 501, "Not Implemented");
            var actual = StatusLine.FromString("HTTP/1.1 501 Not Implemented");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when502BadGateway()
        {
            var expected = new StatusLine("HTTP/1.1", 502, "Bad Gateway");
            var actual = StatusLine.FromString("HTTP/1.1 502 Bad Gateway");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when503ServiceUnavailable()
        {
            var expected = new StatusLine("HTTP/1.1", 503, "Service Unavailable");
            var actual = StatusLine.FromString("HTTP/1.1 503 Service Unavailable");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when504GatewayTimeout()
        {
            var expected = new StatusLine("HTTP/1.1", 504, "Gateway Time-out");
            var actual = StatusLine.FromString("HTTP/1.1 504 Gateway Time-out");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when505HttpVersionNotSupported()
        {
            var expected = new StatusLine("HTTP/1.1", 505, "HTTP Version not supported");
            var actual = StatusLine.FromString("HTTP/1.1 505 HTTP Version not supported");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_whenCR()
        {
            Assert.Throws<FormatException>(() => StatusLine.FromString("999 Foo \r Bar"));
        }

        [Fact]
        public void op_FromString_string_whenLR()
        {
            Assert.Throws<FormatException>(() => StatusLine.FromString("999 Foo \n Bar"));
        }

        [Fact]
        public void op_FromString_string_whenMissingHttpVersion()
        {
            Assert.Throws<FormatException>(() => StatusLine.FromString("200 OK"));
        }

        [Fact]
        public void op_FromString_string_whenMissingHttpVersion404()
        {
            Assert.Throws<FormatException>(() => StatusLine.FromString("404 Not Found"));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "HTTP/1.1 404 Not Found";
            var actual = new StatusLine("HTTP/1.1", 404, "Not Found").ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Code()
        {
            Assert.True(new PropertyExpectations<StatusLine>(p => p.Code)
                            .TypeIs<int>()
                            .ArgumentOutOfRangeException(99)
                            .Set(100)
                            .Set(101)
                            .Set(200)
                            .Set(201)
                            .Set(202)
                            .Set(203)
                            .Set(204)
                            .Set(205)
                            .Set(206)
                            .Set(300)
                            .Set(301)
                            .Set(302)
                            .Set(303)
                            .Set(304)
                            .Set(305)
                            .Set(307)
                            .Set(400)
                            .Set(401)
                            .Set(402)
                            .Set(403)
                            .Set(404)
                            .Set(405)
                            .Set(406)
                            .Set(407)
                            .Set(408)
                            .Set(409)
                            .Set(410)
                            .Set(411)
                            .Set(412)
                            .Set(413)
                            .Set(414)
                            .Set(415)
                            .Set(416)
                            .Set(417)
                            .Set(500)
                            .Set(501)
                            .Set(502)
                            .Set(503)
                            .Set(504)
                            .Set(505)
                            .ArgumentOutOfRangeException(1000)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Reason()
        {
            Assert.True(new PropertyExpectations<StatusLine>(p => p.Reason)
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .ArgumentOutOfRangeException(string.Empty)
                            .FormatException("Foo \r Bar")
                            .FormatException("Foo \n Bar")
                            .Set("Continue")
                            .Set("Switching Protocols")
                            .Set("OK")
                            .Set("Created")
                            .Set("Accepted")
                            .Set("Non-Authoritative Information")
                            .Set("No Content")
                            .Set("Reset Content")
                            .Set("Partial Content")
                            .Set("Multiple Choices")
                            .Set("Moved Permanently")
                            .Set("Found")
                            .Set("See Other")
                            .Set("Not Modified")
                            .Set("Use Proxy")
                            .Set("Temporary Redirect")
                            .Set("Bad Request")
                            .Set("Unauthorized")
                            .Set("Payment Required")
                            .Set("Forbidden")
                            .Set("Not Found")
                            .Set("Method Not Allowed")
                            .Set("Not Acceptable")
                            .Set("Proxy Authentication Required")
                            .Set("Request Time-out")
                            .Set("Conflict")
                            .Set("Gone")
                            .Set("Length Required")
                            .Set("Precondition Failed")
                            .Set("Request Entity Too Large")
                            .Set("Request-URI Too Large")
                            .Set("Unsupported Media Type")
                            .Set("Requested range not satisfiable")
                            .Set("Expectation Failed")
                            .Set("Internal Server Error")
                            .Set("Not Implemented")
                            .Set("Bad Gateway")
                            .Set("Service Unavailable")
                            .Set("Gateway Time-out")
                            .Set("HTTP Version not supported")
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Version()
        {
            Assert.True(new PropertyExpectations<StatusLine>(p => p.Version)
                            .TypeIs<HttpVersion>()
                            .ArgumentNullException()
                            .Set(new HttpVersion(1, 0))
                            .Set(new HttpVersion(1, 1))
                            .IsNotDecorated()
                            .Result);
        }
    }
}