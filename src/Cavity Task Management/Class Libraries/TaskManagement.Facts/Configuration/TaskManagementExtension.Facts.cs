namespace Cavity.Configuration
{
    using System.Configuration;
    using System.IO;

    using Xunit;

    public sealed class TaskManagementExtensionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TaskManagementExtension>()
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
            Assert.NotNull(new TaskManagementExtension());
        }

        [Fact]
        public void prop_Directory()
        {
            Assert.NotNull(new PropertyExpectations<TaskManagementExtension>(x => x.Directory)
                               .TypeIs<DirectoryInfo>()
                               .DefaultValueIsNull()
                               .Set(new DirectoryInfo("C:\\"))
                               .Result);
        }
    }
}