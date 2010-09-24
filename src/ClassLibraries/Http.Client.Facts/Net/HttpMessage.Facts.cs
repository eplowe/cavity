namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Cavity.Net.Mime;
    using Cavity.Text;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class HttpMessageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpMessage>()
                            .DerivesFrom<ComparableObject>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<IHttpMessage>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DerivedHttpMessage() as HttpMessage);
        }

        [Fact]
        public void op_Read_TextReader()
        {
            HttpMessage message = new DerivedHttpMessage();
            HttpHeader connection = "Connection: close";

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(connection);
                    writer.WriteLine(string.Empty);
                    writer.WriteLine("body");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        message.Read(reader);
                    }
                }
            }

            Assert.Equal(1, message.Headers.Count);
            Assert.True(message.Headers.Contains(connection));
        }

        [Fact]
        public void op_Read_TextReaderEmpty()
        {
            HttpMessage message = new DerivedHttpMessage();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        message.Read(reader);
                    }
                }
            }

            Assert.Empty(message.Headers);
        }

        [Fact]
        public void op_Read_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedHttpMessage().Read(null));
        }

        [Fact]
        public void op_Read_TextReader_whenUndefinedMediaType()
        {
            var expected = new StringBuilder();
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: keep-alive");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            try
            {
                var locator = new Mock<IServiceLocator>();
                ServiceLocator.SetLocatorProvider(() => locator.Object);

                using (var stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(expected);
                        writer.Flush();
                        stream.Position = 0;
                        using (var reader = new StreamReader(stream))
                        {
                            Assert.Throws<ArgumentNullException>(() => new DerivedHttpMessage().Read(reader));
                        }
                    }
                }

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void op_ToString()
        {
            var expected = new StringBuilder();
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: keep-alive");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            HttpMessage obj = new DerivedHttpMessage();

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
                        writer.Write(expected);
                        writer.Flush();
                        stream.Position = 0;
                        using (var reader = new StreamReader(stream))
                        {
                            obj.Read(reader);
                        }
                    }
                }

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_Write_TextWriter()
        {
            var expected = new StringBuilder();
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: keep-alive");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            HttpMessage obj = new DerivedHttpMessage();

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
                        writer.Write(expected);
                        writer.Flush();
                        stream.Position = 0;
                        using (var reader = new StreamReader(stream))
                        {
                            obj.Read(reader);
                        }
                    }
                }

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            var actual = new StringBuilder();

            using (var writer = new StringWriter(actual, CultureInfo.InvariantCulture))
            {
                obj.Write(writer);
                writer.Flush();
            }

            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void op_Write_TextWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedHttpMessage().Write(null));
        }

        [Fact]
        public void prop_Body()
        {
            Assert.True(new PropertyExpectations<DerivedHttpMessage>("Body")
                            .TypeIs<IContent>()
                            .ArgumentNullException()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Headers()
        {
            Assert.True(new PropertyExpectations<DerivedHttpMessage>("Headers")
                            .TypeIs<HttpHeaderCollection>()
                            .ArgumentNullException()
                            .IsNotDecorated()
                            .Result);
        }
    }
}