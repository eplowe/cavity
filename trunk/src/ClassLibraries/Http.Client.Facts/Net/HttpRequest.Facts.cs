namespace Cavity.Net
{
    using System;
    using System.IO;
    using System.Text;
    using Cavity.Net.Mime;
    using Cavity.Text;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class HttpRequestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpRequest>()
                            .DerivesFrom<HttpMessage>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IHttpRequest>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpRequest());
        }

        [Fact]
        public void opImplicit_HttpRequest_string()
        {
            var expected = new HttpRequest
            {
                RequestLine = "GET / HTTP/1.1"
            };
            HttpRequest actual = "GET / HTTP/1.1";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpRequest_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => (HttpRequest)string.Empty);
        }

        [Fact]
        public void opImplicit_HttpRequest_stringNull()
        {
            HttpRequest obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpRequest.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpRequest.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenGet()
        {
            RequestLine requestLine = "GET / HTTP/1.1";
            HttpHeader host = "Host: www.example.com";
            HttpHeader connection = "Connection: close";

            var value = new StringBuilder();
            value.AppendLine(requestLine);
            value.AppendLine(host);
            value.AppendLine(connection);

            var obj = HttpRequest.FromString(value.ToString());

            Assert.Equal<string>(requestLine, obj.RequestLine);

            Assert.True(obj.Headers.Contains(host));
            Assert.True(obj.Headers.Contains(connection));
        }

        [Fact]
        public void op_FromString_string_whenPost()
        {
            RequestLine requestLine = "POST / HTTP/1.1";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";
            HttpHeader host = "Host: www.example.com";
            HttpHeader connection = "Connection: keep-alive";

            var value = new StringBuilder();
            value.AppendLine(requestLine);
            value.AppendLine(contentLength);
            value.AppendLine(contentType);
            value.AppendLine(host);
            value.AppendLine(connection);
            value.AppendLine(string.Empty);
            value.Append("text");

            HttpRequest obj;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(new TextPlain())
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

                obj = HttpRequest.FromString(value.ToString());

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal<string>(requestLine, obj.RequestLine);

            Assert.True(obj.Headers.Contains(contentLength));
            Assert.True(obj.Headers.Contains(contentType));
            Assert.True(obj.Headers.Contains(host));
            Assert.True(obj.Headers.Contains(connection));
        }

        [Fact]
        public void op_Read_TextReaderEmpty()
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    Assert.Throws<ArgumentNullException>(() => new HttpRequest().Read(reader));
                }
            }
        }

        [Fact]
        public void op_Read_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest().Read(null));
        }

        [Fact]
        public void op_Read_TextReader_whenGet()
        {
            var request = new HttpRequest();
            RequestLine requestLine = "GET / HTTP/1.1";
            HttpHeader host = "Host: www.example.com";
            HttpHeader connection = "Connection: close";

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(requestLine);
                writer.WriteLine(host);
                writer.WriteLine(connection);
                writer.WriteLine(string.Empty);
                writer.WriteLine("body");
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    request.Read(reader);
                }
            }

            Assert.Equal(requestLine, request.RequestLine);

            Assert.Equal(2, request.Headers.Count);
            Assert.True(request.Headers.Contains(host));
            Assert.True(request.Headers.Contains(connection));

            Assert.Null(request.Body);
        }

        [Fact]
        public void op_Read_TextReader_whenPost()
        {
            var request = new HttpRequest();
            RequestLine requestLine = "POST / HTTP/1.1";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";
            HttpHeader host = "Host: www.example.com";
            HttpHeader connection = "Connection: keep-alive";

            try
            {
                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(new TextPlain())
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

                using (var stream = new MemoryStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(requestLine);
                    writer.WriteLine(contentLength);
                    writer.WriteLine(contentType);
                    writer.WriteLine(host);
                    writer.WriteLine(connection);
                    writer.WriteLine(string.Empty);
                    writer.Write("text");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        request.Read(reader);
                    }
                }

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal(requestLine, request.RequestLine);

            Assert.Equal(4, request.Headers.Count);
            Assert.True(request.Headers.Contains(contentLength));
            Assert.True(request.Headers.Contains(contentType));
            Assert.True(request.Headers.Contains(host));
            Assert.True(request.Headers.Contains(connection));

            Assert.Equal("text", ((TextPlain)request.Body).Value);
        }

        [Fact]
        public void op_ToString_whenGet()
        {
            var expected = new StringBuilder();
            expected.AppendLine("GET / HTTP/1.1");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: close");
            expected.AppendLine(string.Empty);

            var actual = HttpRequest.FromString(expected.ToString()).ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenPost()
        {
            var expected = new StringBuilder();
            expected.AppendLine("POST / HTTP/1.1");
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: keep-alive");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            string actual;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(new TextPlain())
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

                actual = HttpRequest.FromString(expected.ToString()).ToString();

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_Write_TextWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest().Write(null));
        }

        [Fact]
        public void op_Write_TextWriter_whenGet()
        {
            var expected = new StringBuilder();
            expected.AppendLine("GET / HTTP/1.1");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: close");
            expected.AppendLine(string.Empty);

            var obj = HttpRequest.FromString(expected.ToString());

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                obj.Write(writer);
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    Assert.Equal(expected.ToString(), reader.ReadToEnd());
                }
            }
        }

        [Fact]
        public void op_Write_TextWriter_whenPost()
        {
            var expected = new StringBuilder();
            expected.AppendLine("POST / HTTP/1.1");
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: keep-alive");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            HttpRequest obj;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(new TextPlain())
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

                obj = HttpRequest.FromString(expected.ToString());

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                obj.Write(writer);
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    Assert.Equal(expected.ToString(), reader.ReadToEnd());
                }
            }
        }

        [Fact]
        public void prop_AbsoluteUri()
        {
            Assert.True(new PropertyExpectations<HttpRequest>(p => p.AbsoluteUri)
                            .TypeIs<Uri>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_AbsoluteUri_get()
        {
            var expected = new Uri("http://www.example.com/path");

            var obj = new HttpRequest
            {
                RequestLine = new RequestLine("GET", expected.AbsoluteUri, "HTTP/1.1")
            };

            var actual = obj.AbsoluteUri;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_AbsoluteUri_getFromRelative()
        {
            var expected = new Uri("http://www.example.com/path");

            var obj = new HttpRequest
            {
                RequestLine = "GET /path HTTP/1.1",
                Headers =
                    {
                        (HttpHeader)"Host: www.example.com"
                    }
            };

            var actual = obj.AbsoluteUri;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_RequestLine()
        {
            Assert.True(new PropertyExpectations<HttpRequest>(p => p.RequestLine)
                            .TypeIs<RequestLine>()
                            .DefaultValueIsNull()
                            .ArgumentNullException()
                            .Set(new RequestLine("GET", "/", "HTTP/1.1"))
                            .IsNotDecorated()
                            .Result);
        }
    }
}