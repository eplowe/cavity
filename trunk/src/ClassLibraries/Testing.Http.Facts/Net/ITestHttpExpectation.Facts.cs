namespace Cavity.Net
{
    using System.Net;
    using Moq;
    using Xunit;

    public sealed class ITestHttpExpectationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITestHttpExpectation>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Check_Response()
        {
            const bool expected = true;

            var response = new Response(HttpStatusCode.OK, "http://example.com/", new WebHeaderCollection(), new CookieCollection());

            var mock = new Mock<ITestHttpExpectation>();
            mock
                .Setup(x => x.Check(response))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Check(response);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}