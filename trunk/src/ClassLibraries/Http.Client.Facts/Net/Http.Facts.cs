namespace Cavity.Net
{
    using System;
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
                request.SetupGet<Uri>(x => x.AbsoluteUri).Returns(new Uri("http://www.example.com/")).Verifiable();
                request
                    .Setup(x => x.Write(It.IsAny<StreamWriter>()))
                    .Callback((StreamWriter writer) => this.WriteGet(writer, "www.example.com"))
                    .Verifiable();

                var media = new Mock<IMediaType>();
                media
                    .Setup(x => x.ToBody(It.IsAny<StreamReader>()))
                    .Returns(new Mock<IContent>().Object)
                    .Verifiable();

                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/html")).Returns(media.Object).Verifiable();
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

        private void WriteGet(StreamWriter writer, string host)
        {
            writer.WriteLine("GET / HTTP/1.1");
            writer.WriteLine("Host: " + host);
            writer.WriteLine("Connection: Close");
            writer.WriteLine(string.Empty);
        }
    }
}