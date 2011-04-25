namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Cavity;
    using Xunit;

    public sealed class TaskManagementExtensionFacts
    {
        [Fact]
        public void type_definition()
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
    }
}