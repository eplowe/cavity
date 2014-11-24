namespace Cavity.Web.Routing
{
    using System.Web.Routing;
    using Moq;
    using Xunit;

    public sealed class IRegisterRoutesFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRegisterRoutes>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Register_RouteCollection()
        {
            var routes = new RouteCollection();

            var mock = new Mock<IRegisterRoutes>(MockBehavior.Strict);
            mock
                .Setup(x => x.Register(routes))
                .Verifiable();

            mock.Object.Register(routes);

            mock.VerifyAll();
        }
    }
}