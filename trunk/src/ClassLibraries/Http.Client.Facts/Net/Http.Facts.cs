namespace Cavity.Net
{
    using System;
    using System.IO;
    using Cavity.Net.Mime;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class HttpFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Http>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IHttp>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Http());
        }

        [Fact]
        public void op_Send_IHttpRequest()
        {
            try
            {
                var request = new Mock<IHttpRequest>();
                request
                    .SetupGet(x => x.AbsoluteUri)
                    .Returns(new Uri("http://www.example.com/"))
                    .Verifiable();
                request
                    .Setup(x => x.Write(It.IsAny<TextWriter>()))
                    .Callback((TextWriter writer) => WriteGet(writer, "GET", "/", "www.example.com"))
                    .Verifiable();

                var media = new Mock<IMediaType>();
                media
                    .Setup(x => x.ToContent(It.IsAny<TextReader>()))
                    .Returns(new Mock<IContent>().Object)
                    .Verifiable();

                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/html"))
                    .Returns(media.Object)
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

                var response = new Http().Send(request.Object);

                Assert.Equal<string>("HTTP/1.1 200 OK", response.StatusLine);
                Assert.NotEqual(0, response.Headers.Count);

                request.VerifyAll();
                media.VerifyAll();
                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void op_Send_IHttpRequestNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Http().Send(null));
        }

        [Fact]
        public void op_Send_IHttpRequest_whenOptions()
        {
            try
            {
                var request = new Mock<IHttpRequest>();
                request
                    .SetupGet(x => x.AbsoluteUri)
                    .Returns(new Uri("http://www.example.com/"))
                    .Verifiable();
                request
                    .Setup(x => x.Write(It.IsAny<TextWriter>()))
                    .Callback((TextWriter writer) => WriteGet(writer, "OPTIONS", "*", "www.example.com"))
                    .Verifiable();

                var media = new Mock<IMediaType>();
                media
                    .Setup(x => x.ToContent(It.IsAny<TextReader>()))
                    .Returns(new Mock<IContent>().Object)
                    .Verifiable();

                var locator = new Mock<IServiceLocator>();
                locator
                    .Setup(e => e.GetInstance<IMediaType>("text/plain"))
                    .Returns(media.Object)
                    .Verifiable();

                ServiceLocator.SetLocatorProvider(() => locator.Object);

                var response = new Http().Send(request.Object);

                Assert.Equal<string>("HTTP/1.1 200 OK", response.StatusLine);
                Assert.True(response.Headers.ContainsName("Allow"));

                request.VerifyAll();
                media.VerifyAll();
                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        private static void WriteGet(TextWriter writer,
                                     string method,
                                     string requestUri,
                                     string host)
        {
            writer.WriteLine("{0} {1} HTTP/1.1".FormatWith(method, requestUri));
            writer.WriteLine("Host: " + host);
            writer.WriteLine("Connection: Close");
            writer.WriteLine(string.Empty);
        }
    }
}