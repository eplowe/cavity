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

    public sealed class HttpResponseFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpResponse>()
                .DerivesFrom<HttpMessage>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IHttpResponse>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpResponse());
        }

        [Fact]
        public void prop_StatusLine()
        {
            Assert.NotNull(new PropertyExpectations<HttpResponse>("StatusLine")
                .TypeIs<StatusLine>()
                .DefaultValueIsNull()
                .ArgumentNullException()
                .Set(new StatusLine("HTTP/1.1", 200, "OK"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_HttpResponse_stringNull()
        {
            HttpResponse obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpResponse_stringEmpty()
        {
            HttpResponse expected;

            Assert.Throws<ArgumentOutOfRangeException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpResponse_string()
        {
            HttpResponse expected = "HTTP/1.1 200 OK";
            HttpResponse actual = new HttpResponse
            {
                StatusLine = new StatusLine("HTTP/1.1", 200, "OK")
            };

            Assert.Equal<HttpResponse>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpResponse.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpResponse.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string_when201()
        {
            StatusLine statusLine = "HTTP/1.1 201 Created";

            StringBuilder expected = new StringBuilder();
            expected.AppendLine(statusLine);

            HttpResponse obj = HttpResponse.Parse(expected.ToString());

            Assert.Equal<string>(statusLine, obj.StatusLine);

            Assert.Equal<int>(0, obj.Headers.Count);

            Assert.Null(obj.Body);
        }

        [Fact]
        public void op_Parse_string_when404()
        {
            StatusLine statusLine = "HTTP/1.1 404 Not Found";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";

            StringBuilder expected = new StringBuilder();
            expected.AppendLine(statusLine);
            expected.AppendLine(contentLength);
            expected.AppendLine(contentType);
            expected.AppendLine(string.Empty);
            expected.Append("text");

            HttpResponse obj = null;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                obj = HttpResponse.Parse(expected.ToString());

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal<string>(statusLine, obj.StatusLine);

            Assert.True(obj.Headers.Contains(contentLength));
            Assert.True(obj.Headers.Contains(contentType));

            Assert.Equal<string>("text", (obj.Body as TextPlain).Value);
        }

        [Fact]
        public void op_Read_StreamReader_when201()
        {
            var response = new HttpResponse();
            StatusLine statusLine = "HTTP/1.1 201 Created";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";

            try
            {
                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                using (var stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(statusLine);
                        writer.WriteLine(contentLength);
                        writer.WriteLine(contentType);
                        writer.WriteLine(string.Empty);
                        writer.Write("text");
                        writer.Flush();
                        stream.Position = 0;
                        using (var reader = new StreamReader(stream))
                        {
                            response.Read(reader);
                        }
                    }
                }

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal<StatusLine>(statusLine, response.StatusLine);

            Assert.Equal<int>(2, response.Headers.Count);
            Assert.True(response.Headers.Contains(contentLength));
            Assert.True(response.Headers.Contains(contentType));

            Assert.Equal<string>("text", (response.Body as TextPlain).Value);
        }

        [Fact]
        public void op_Read_StreamReader_when404()
        {
            var response = new HttpResponse();
            StatusLine statusLine = "HTTP/1.1 404 Not Found";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";

            try
            {
                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                using (var stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(statusLine);
                        writer.WriteLine(contentLength);
                        writer.WriteLine(contentType);
                        writer.WriteLine(string.Empty);
                        writer.Write("text");
                        writer.Flush();
                        stream.Position = 0;
                        using (var reader = new StreamReader(stream))
                        {
                            response.Read(reader);
                        }
                    }
                }

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal<StatusLine>(statusLine, response.StatusLine);

            Assert.Equal<int>(2, response.Headers.Count);
            Assert.True(response.Headers.Contains(contentLength));
            Assert.True(response.Headers.Contains(contentType));

            Assert.Equal<string>("text", (response.Body as TextPlain).Value);
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
                        Assert.Throws<ArgumentNullException>(() => new HttpResponse().Read(reader));
                    }
                }
            }
        }

        [Fact]
        public void op_Read_StreamReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpResponse().Read(null as StreamReader));
        }

        [Fact]
        public void op_ToString()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("HTTP/1.1 404 Not Found");
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            string actual = null;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                actual = HttpResponse.Parse(expected.ToString()).ToString();

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