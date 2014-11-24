namespace Cavity
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class IHttpExpectationsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpExpectations>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Implements<ICollection<HttpExpectation>>()
                            .Result);
        }

        [Fact]
        public void prop_Result()
        {
            var mock = new Mock<IHttpExpectations>();
            mock
                .Setup(x => x.Result)
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Result);

            mock.VerifyAll();
        }
    }
}