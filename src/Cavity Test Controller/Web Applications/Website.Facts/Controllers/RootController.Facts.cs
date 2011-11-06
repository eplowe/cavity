namespace Cavity.Controllers
{
    using System;
    using System.Web.Mvc;
    using Cavity;
    using Cavity.Web.Mvc;
    using Xunit;

    public sealed class RootControllerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RootController>()
                .DerivesFrom<Controller>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RootController());
        }

        [Fact]
        public void op_Redirect()
        {
            var result = (FoundResult)new RootController().Redirect();

            const string expected = "/today";
            var actual = result.Location;

            Assert.Equal(expected, actual);
        }
    }
}