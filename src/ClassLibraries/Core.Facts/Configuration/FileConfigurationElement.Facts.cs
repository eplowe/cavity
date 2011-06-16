namespace Cavity.Configuration
{
    using System.Configuration;
    using System.IO;
    using Cavity;
    using Cavity.IO;
    using Xunit;

    public sealed class FileConfigurationElementFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileConfigurationElement>()
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
            Assert.NotNull(new FileConfigurationElement());
        }

        [Fact]
        public void ctor_FileInfo()
        {
            using (var temp = new TempFile())
            {
                Assert.NotNull(new FileConfigurationElement(temp.Info));
            }
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<FileConfigurationElement>(p => p.Name)
                            .TypeIs<string>()
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_File()
        {
            Assert.True(new PropertyExpectations<FileConfigurationElement>(p => p.File)
                            .TypeIs<FileInfo>()
                            .DefaultValueIs(null)
                            .ArgumentNullException()
                            .Set(new FileInfo("C:\\example.txt"))
                            .IsNotDecorated()
                            .Result);
        }
    }
}