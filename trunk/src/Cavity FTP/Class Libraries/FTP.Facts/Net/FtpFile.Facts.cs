namespace Cavity.Net
{
    using Xunit;

    public sealed class FtpFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FtpFile>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .Result);
        }
    }
}
