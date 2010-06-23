namespace Cavity.Net
{
    using System;
    using System.IO;
    using System.Text;
    using Cavity;
    using Cavity.Net.Mime;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class HttpRequestFacts
    {
        [Fact]
        public void type_definition()
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
        public void prop_AbsoluteUri()
        {
            Assert.NotNull(new PropertyExpectations<HttpRequest>("AbsoluteUri")
                .TypeIs<Uri>()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_AbsoluteUri_get()
        {
            Uri expected = new Uri("http://www.example.com/");

            var obj = new HttpRequest
            {
                RequestLine = new RequestLine("GET", expected.AbsoluteUri, "HTTP/1.1")
            };

            Uri actual = obj.AbsoluteUri;

            Assert.Equal<Uri>(expected, actual);
        }

        [Fact]
        public void prop_RequestLine()
        {
            Assert.NotNull(new PropertyExpectations<HttpRequest>("RequestLine")
                .TypeIs<RequestLine>()
                .DefaultValueIsNull()
                .ArgumentNullException()
                .Set(new RequestLine("GET", "/", "HTTP/1.1"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_HttpRequest_stringNull()
        {
            HttpRequest obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpRequest_stringEmpty()
        {
            HttpRequest expected;

            Assert.Throws<ArgumentOutOfRangeException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpRequest_string()
        {
            HttpRequest expected = "GET / HTTP/1.1";
            HttpRequest actual = new HttpRequest { RequestLine = "GET / HTTP/1.1" };

            Assert.Equal<HttpRequest>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpRequest.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpRequest.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string_whenGet()
        {
            RequestLine requestLine = "GET / HTTP/1.1";
            HttpHeader host = "Host: www.example.com";
            HttpHeader connection = "Connection: close";

            StringBuilder value = new StringBuilder();
            value.AppendLine(requestLine);
            value.AppendLine(host);
            value.AppendLine(connection);

            HttpRequest obj = HttpRequest.Parse(value.ToString());

            Assert.Equal<string>(requestLine, obj.RequestLine);

            Assert.True(obj.Headers.Contains(host));
            Assert.True(obj.Headers.Contains(connection));
        }

        [Fact]
        public void op_Parse_string_whenPost()
        {
            RequestLine requestLine = "POST / HTTP/1.1";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";
            HttpHeader host = "Host: www.example.com";
            HttpHeader connection = "Connection: keep-alive";

            StringBuilder value = new StringBuilder();
            value.AppendLine(requestLine);
            value.AppendLine(contentLength);
            value.AppendLine(contentType);
            value.AppendLine(host);
            value.AppendLine(connection);
            value.AppendLine(string.Empty);
            value.Append("text");

            HttpRequest obj = null;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                obj = HttpRequest.Parse(value.ToString());

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
        public void op_Read_TextReader_whenGet()
        {
            var request = new HttpRequest();
            RequestLine requestLine = "GET / HTTP/1.1";
            HttpHeader host = "Host: www.example.com";
            HttpHeader connection = "Connection: close";

            using (var stream = new MemoryStream())
            {
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
            }

            Assert.Equal<RequestLine>(requestLine, request.RequestLine);

            Assert.Equal<int>(2, request.Headers.Count);
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
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                using (var stream = new MemoryStream())
                {
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
                }

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal<RequestLine>(requestLine, request.RequestLine);

            Assert.Equal<int>(4, request.Headers.Count);
            Assert.True(request.Headers.Contains(contentLength));
            Assert.True(request.Headers.Contains(contentType));
            Assert.True(request.Headers.Contains(host));
            Assert.True(request.Headers.Contains(connection));

            Assert.Equal<string>("text", (request.Body as TextPlain).Value);
        }

        [Fact]
        public void op_Read_TextReaderEmpty()
        {
            using (var stream = new MemoryStream())
            {
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
        }

        [Fact]
        public void op_Read_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest().Read(null as TextReader));
        }

        [Fact]
        public void op_Write_TextWriter_whenGet()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("GET / HTTP/1.1");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: close");

            HttpRequest obj = HttpRequest.Parse(expected.ToString());

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    obj.Write(writer);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        Assert.Equal<string>(expected.ToString(), reader.ReadToEnd());
                    }
                }
            }
        }

        [Fact]
        public void op_Write_TextWriter_whenPost()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("POST / HTTP/1.1");
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: keep-alive");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            HttpRequest obj = null;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                obj = HttpRequest.Parse(expected.ToString());

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    obj.Write(writer);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        Assert.Equal<string>(expected.ToString(), reader.ReadToEnd());
                    }
                }
            }
        }

        [Fact]
        public void op_Write_TextWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest().Write(null as TextWriter));
        }

        [Fact]
        public void op_ToString_whenGet()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("GET / HTTP/1.1");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: close");

            string actual = HttpRequest.Parse(expected.ToString()).ToString();

            Assert.Equal<string>(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenPost()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("POST / HTTP/1.1");
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: keep-alive");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            string actual = null;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                actual = HttpRequest.Parse(expected.ToString()).ToString();

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal<string>(expected.ToString(), actual);
        }
    }
}