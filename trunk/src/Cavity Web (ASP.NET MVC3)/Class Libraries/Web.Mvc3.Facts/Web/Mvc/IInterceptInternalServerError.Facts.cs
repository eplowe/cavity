namespace Cavity.Web.Mvc
{
    using System.Web.Mvc;

    using Moq;

    using Xunit;

    public sealed class IInterceptInternalServerErrorFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IInterceptInternalServerError>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_View_ExceptionContext()
        {
            var expected = new EmptyResult();
            var filterContext = new ExceptionContext();

            var mock = new Mock<IInterceptInternalServerError>(MockBehavior.Strict);
            mock
                .Setup(x => x.View(filterContext))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.View(filterContext);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}