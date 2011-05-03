namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
    using Cavity;
    using Cavity.IO;
    using Xunit;

    public sealed class DirectoryConfigurationElementFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DirectoryConfigurationElement>()
                            .DerivesFrom<ConfigurationElement>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DirectoryConfigurationElement());
        }

        [Fact]
        public void ctor_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DirectoryConfigurationElement(temp.Info));
            }
        }

        [Fact]
        public void prop_Directory()
        {
            Assert.True(new PropertyExpectations<DirectoryConfigurationElement>(p => p.Directory)
                            .TypeIs<DirectoryInfo>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}