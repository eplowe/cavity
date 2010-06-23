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
        public void ctor_RequestLineNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest(null as RequestLine));
        }

        [Fact]
        public void ctor_RequestLine()
        {
            Assert.NotNull(new HttpRequest(new RequestLine("GET", "/", "HTTP/1.1")));
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

            var requestLine = new RequestLine("GET", expected.AbsoluteUri, "HTTP/1.1");

            Uri actual = new HttpRequest(requestLine).AbsoluteUri;

            Assert.Equal<Uri>(expected, actual);
        }

        [Fact]
        public void prop_RequestLine()
        {
            Assert.NotNull(new PropertyExpectations<HttpRequest>("RequestLine")
                .TypeIs<RequestLine>()
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

            Assert.Throws<ArgumentNullException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpRequest_string()
        {
            HttpRequest expected = "GET / HTTP/1.1";
            HttpRequest actual = new HttpRequest("GET / HTTP/1.1");

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
            Assert.Throws<ArgumentNullException>(() => HttpRequest.Parse(string.Empty));
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
            HttpHeader contentLength = "Content-Length: 7";
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
        public void op_Read_StreamReader_whenGet()
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
        public void op_Read_StreamReader_whenPost()
        {
            var request = new HttpRequest();
            RequestLine requestLine = "POST / HTTP/1.1";
            HttpHeader contentLength = "Content-Length: 7";
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
                        writer.WriteLine("body");
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

            Assert.NotNull(request.Body);
        }

        [Fact]
        public void op_Read_StreamReaderEmpty()
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
        public void op_Read_StreamReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest().Read(null as StreamReader));
        }

        [Fact]
        public void op_Write_StreamWriterNull()
        {
            var obj = new HttpRequest("GET / HTTP/1.1");

            Assert.Throws<ArgumentNullException>(() => obj.Write(null as StreamWriter));
        }

        [Fact]
        public void op_ToString()
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