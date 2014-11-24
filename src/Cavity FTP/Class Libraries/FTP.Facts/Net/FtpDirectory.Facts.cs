namespace Cavity.Net
{
    using System.Collections.ObjectModel;
    using Xunit;

    public sealed class FtpDirectoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FtpDirectory>()
                            .DerivesFrom<Collection<FtpFile>>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Result);
        }
    }
}