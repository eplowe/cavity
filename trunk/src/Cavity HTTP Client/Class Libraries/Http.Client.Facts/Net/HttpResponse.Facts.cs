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

    public sealed class HttpResponseFacts
    {
        [Fact]
        public void a_definition()
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
        public void opImplicit_HttpResponse_string()
        {
            var expected = new HttpResponse
            {
                StatusLine = new StatusLine("HTTP/1.1", 200, "OK")
            };
            HttpResponse actual = "HTTP/1.1 200 OK";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpResponse_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => (HttpResponse)string.Empty);
        }

        [Fact]
        public void opImplicit_HttpResponse_stringNull()
        {
            Assert.Null((HttpResponse)null);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpResponse.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpResponse.FromString(null));
        }

        [Fact]
        public void op_FromString_string_when201()
        {
            StatusLine statusLine = "HTTP/1.1 201 Created";

            var expected = new StringBuilder();
            expected.AppendLine(statusLine);

            var obj = HttpResponse.FromString(expected.ToString());

            Assert.Equal<string>(statusLine, obj.StatusLine);

            Assert.Equal(0, obj.Headers.Count);

            Assert.Null(obj.Body);
        }

        [Fact]
        public void op_FromString_string_when404()
        {
            StatusLine statusLine = "HTTP/1.1 404 Not Found";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";

            var expected = new StringBuilder();
            expected.AppendLine(statusLine);
            expected.AppendLine(contentLength);
            expected.AppendLine(contentType);
            expected.AppendLine(string.Empty);
            expected.Append("text");

            HttpResponse obj;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(new TextPlain())
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

                obj = HttpResponse.FromString(expected.ToString());

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            Assert.Equal<string>(statusLine, obj.StatusLine);

            Assert.True(obj.Headers.Contains(contentLength));
            Assert.True(obj.Headers.Contains(contentType));

            Assert.Equal("text", ((TextPlain)obj.Body).Value);
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
                        Assert.Throws<ArgumentNullException>(() => new HttpResponse().Read(reader));
                    }
                }
            }
        }

        [Fact]
        public void op_Read_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpResponse().Read(null));
        }

        [Fact]
        public void op_Read_TextReader_when201()
        {
            var response = new HttpResponse();
            StatusLine statusLine = "HTTP/1.1 201 Created";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";

            try
            {
                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(new TextPlain())
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

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

            Assert.Equal(statusLine, response.StatusLine);

            Assert.Equal(2, response.Headers.Count);
            Assert.True(response.Headers.Contains(contentLength));
            Assert.True(response.Headers.Contains(contentType));

            Assert.Equal("text", ((TextPlain)response.Body).Value);
        }

        [Fact]
        public void op_Read_TextReader_when404()
        {
            var response = new HttpResponse();
            StatusLine statusLine = "HTTP/1.1 404 Not Found";
            HttpHeader contentLength = "Content-Length: 4";
            HttpHeader contentType = "Content-Type: text/plain; charset=UTF-8";

            try
            {
                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(new TextPlain())
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

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

            Assert.Equal(statusLine, response.StatusLine);

            Assert.Equal(2, response.Headers.Count);
            Assert.True(response.Headers.Contains(contentLength));
            Assert.True(response.Headers.Contains(contentType));

            Assert.Equal("text", ((TextPlain)response.Body).Value);
        }

        [Fact]
        public void op_ToString_when201()
        {
            var expected = new StringBuilder();
            expected.AppendLine("HTTP/1.1 201 Created");
            expected.AppendLine(string.Empty);

            var actual = HttpResponse.FromString(expected.ToString()).ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_when404()
        {
            var expected = new StringBuilder();
            expected.AppendLine("HTTP/1.1 404 Not Found");
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
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

                actual = HttpResponse.FromString(expected.ToString()).ToString();

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
            Assert.Throws<ArgumentNullException>(() => new HttpResponse().Write(null));
        }

        [Fact]
        public void op_Write_TextWriter_when201()
        {
            var expected = new StringBuilder();
            expected.AppendLine("HTTP/1.1 201 Created");
            expected.AppendLine(string.Empty);

            var obj = HttpResponse.FromString(expected.ToString());

            using (var stream = new MemoryStream())
            {
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
        }

        [Fact]
        public void op_Write_TextWriter_when404()
        {
            var expected = new StringBuilder();
            expected.AppendLine("HTTP/1.1 404 Not Found");
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            HttpResponse obj;
            try
            {
                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(new TextPlain())
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

                obj = HttpResponse.FromString(expected.ToString());

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
                        Assert.Equal(expected.ToString(), reader.ReadToEnd());
                    }
                }
            }
        }

        [Fact]
        public void prop_StatusLine()
        {
            Assert.True(new PropertyExpectations<HttpResponse>(p => p.StatusLine)
                            .TypeIs<StatusLine>()
                            .DefaultValueIsNull()
                            .ArgumentNullException()
                            .Set(new StatusLine("HTTP/1.1", 200, "OK"))
                            .IsNotDecorated()
                            .Result);
        }
    }
}