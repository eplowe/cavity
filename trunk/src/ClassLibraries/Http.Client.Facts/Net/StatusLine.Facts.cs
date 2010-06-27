namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class StatusLineFacts
    {
        [Fact]
        public void type_definition()
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
        public void prop_Code()
        {
            Assert.True(new PropertyExpectations<StatusLine>("Code")
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
            Assert.True(new PropertyExpectations<StatusLine>("Reason")
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
            Assert.True(new PropertyExpectations<StatusLine>("Version")
                .TypeIs<HttpVersion>()
                .ArgumentNullException()
                .Set(new HttpVersion(1, 0))
                .Set(new HttpVersion(1, 1))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_StatusLine_stringNull()
        {
            StatusLine obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_StatusLine_string()
        {
            StatusLine expected = "HTTP/1.1 200 OK";
            StatusLine actual = new StatusLine("HTTP/1.1", 200, "OK");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when100Continue()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 100, "Continue");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 100 Continue");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when101SwitchingProtocols()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 101, "Switching Protocols");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 101 Switching Protocols");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when200OK()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 200, "OK");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 200 OK");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when201Created()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 201, "Created");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 201 Created");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when202Accepted()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 202, "Accepted");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 202 Accepted");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when203NonAuthoritativeInformation()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 203, "Non-Authoritative Information");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 203 Non-Authoritative Information");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when204NoContent()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 204, "No Content");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 204 No Content");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when205ResetContent()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 205, "Reset Content");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 205 Reset Content");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when206PartialContent()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 206, "Partial Content");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 206 Partial Content");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when300MultipleChoices()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 300, "Multiple Choices");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 300 Multiple Choices");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when301MovedPermanently()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 301, "Moved Permanently");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 301 Moved Permanently");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when302Found()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 302, "Found");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 302 Found");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when303SeeOther()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 303, "See Other");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 303 See Other");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when304NotModified()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 304, "Not Modified");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 304 Not Modified");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when305UseProxy()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 305, "Use Proxy");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 305 Use Proxy");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when307TemporaryRedirect()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 307, "Temporary Redirect");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 307 Temporary Redirect");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when400BadRequest()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 400, "Bad Request");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 400 Bad Request");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when401Unauthorized()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 401, "Unauthorized");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 401 Unauthorized");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when402PaymentRequired()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 402, "Payment Required");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 402 Payment Required");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when403Forbidden()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 403, "Forbidden");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 403 Forbidden");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when404NotFound()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 404, "Not Found");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 404 Not Found");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when405MethodNotAllowed()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 405, "Method Not Allowed");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 405 Method Not Allowed");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when406NotAcceptable()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 406, "Not Acceptable");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 406 Not Acceptable");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when407ProxyAuthenticationRequired()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 407, "Proxy Authentication Required");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 407 Proxy Authentication Required");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when408RequestTimeout()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 408, "Request Time-out");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 408 Request Time-out");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when409Conflict()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 409, "Conflict");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 409 Conflict");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when410Gone()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 410, "Gone");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 410 Gone");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when411LengthRequired()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 411, "Length Required");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 411 Length Required");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when412PreconditionFailed()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 412, "Precondition Failed");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 412 Precondition Failed");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when413RequestEntityTooLarge()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 413, "Request Entity Too Large");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 413 Request Entity Too Large");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when414RequestUriTooLarge()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 414, "Request-URI Too Large");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 414 Request-URI Too Large");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when415UnsupportedMediaType()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 415, "Unsupported Media Type");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 415 Unsupported Media Type");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when416RequestedRangeNotSatisfiable()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 416, "Requested range not satisfiable");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 416 Requested range not satisfiable");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when417ExpectationFailed()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 417, "Expectation Failed");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 417 Expectation Failed");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when500InternalServerError()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 500, "Internal Server Error");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 500 Internal Server Error");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when501NotImplemented()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 501, "Not Implemented");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 501 Not Implemented");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when502BadGateway()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 502, "Bad Gateway");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 502 Bad Gateway");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when503ServiceUnavailable()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 503, "Service Unavailable");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 503 Service Unavailable");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when504GatewayTimeout()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 504, "Gateway Time-out");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 504 Gateway Time-out");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_string_when505HttpVersionNotSupported()
        {
            StatusLine expected = new StatusLine("HTTP/1.1", 505, "HTTP Version not supported");
            StatusLine actual = StatusLine.FromString("HTTP/1.1 505 HTTP Version not supported");

            Assert.Equal<StatusLine>(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => StatusLine.FromString(null as string));
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<FormatException>(() => StatusLine.FromString(string.Empty));
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
        public void op_ToString()
        {
            string expected = "HTTP/1.1 404 Not Found";
            string actual = new StatusLine("HTTP/1.1", 404, "Not Found").ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}