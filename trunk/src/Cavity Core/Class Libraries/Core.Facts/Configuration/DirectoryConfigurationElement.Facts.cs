namespace Cavity.Configuration
{
    using System.Configuration;
    using System.IO;
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
        public void ctor_string_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DirectoryConfigurationElement("temp", temp.Info));
            }
        }

        [Fact]
        public void prop_Directory()
        {
            Assert.True(new PropertyExpectations<DirectoryConfigurationElement>(p => p.Directory)
                            .TypeIs<DirectoryInfo>()
                            .DefaultValueIs(null)
                            .ArgumentNullException()
                            .Set(new DirectoryInfo("C:\\"))
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<DirectoryConfigurationElement>(p => p.Name)
                            .IsAutoProperty(string.Empty)
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .Result);
        }
    }
}