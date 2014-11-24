namespace Cavity.Net
{
    using System.IO;
    using Moq;
    using Xunit;

    public sealed class IHttpMessageBodyFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpMessageBody>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_Write_Stream()
        {
            using (var stream = new MemoryStream())
            {
                var mock = new Mock<IHttpMessageBody>();

                // ReSharper disable AccessToDisposedClosure
                mock
                    .Setup(x => x.Write(stream))
                    .Verifiable();

                // ReSharper restore AccessToDisposedClosure
                mock.Object.Write(stream);

                mock.VerifyAll();
            }
        }
    }
}