namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.IO;
    using Cavity;
    using Cavity.Net.Mime;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class HttpFacts
    {
        [Fact]
        public void type_definition()
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
        public void op_Send_IHttpRequestNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Http().Send(null as IHttpRequest));
        }

        [Fact]
        public void op_Send_IHttpRequest()
        {
            try
            {
                var request = new Mock<IHttpRequest>();
                request
                    .SetupGet<Uri>(x => x.AbsoluteUri)
                    .Returns(new Uri("http://www.example.com/"))
                    .Verifiable();
                request
                    .Setup(x => x.Write(It.IsAny<TextWriter>()))
                    .Callback((TextWriter writer) => this.WriteGet(writer, "GET", "/", "www.example.com"))
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
                
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                IHttpResponse response = new Http().Send(request.Object);

                Assert.Equal<string>("HTTP/1.1 200 OK", response.StatusLine);
                Assert.Equal<int>(9, response.Headers.Count);

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
        public void op_Send_IHttpRequest_whenOptions()
        {
            try
            {
                var request = new Mock<IHttpRequest>();
                request
                    .SetupGet<Uri>(x => x.AbsoluteUri)
                    .Returns(new Uri("http://www.example.com/"))
                    .Verifiable();
                request
                    .Setup(x => x.Write(It.IsAny<TextWriter>()))
                    .Callback((TextWriter writer) => this.WriteGet(writer, "OPTIONS", "*", "www.example.com"))
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

                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                IHttpResponse response = new Http().Send(request.Object);

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

        private void WriteGet(TextWriter writer, string method, string requestUri, string host)
        {
            writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} {1} HTTP/1.1", method, requestUri));
            writer.WriteLine("Host: " + host);
            writer.WriteLine("Connection: Close");
            writer.WriteLine(string.Empty);
        }
    }
}