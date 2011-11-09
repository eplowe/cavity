namespace Cavity.Web.Mvc
{
    using Xunit;

    public sealed class MaintenanceControllerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MaintenanceController>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new MaintenanceController());
        }
    }
}