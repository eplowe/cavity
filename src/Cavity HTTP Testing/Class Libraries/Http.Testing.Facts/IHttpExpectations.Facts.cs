namespace Cavity.Net
{
    using Xunit;

    public sealed class IHttpExpectationsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpExpectations>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Result);
        }

        ////[Fact]
        ////public void ctor()
        ////{
        ////    Assert.NotNull(new IHttpExpectations());
        ////}
    }
}