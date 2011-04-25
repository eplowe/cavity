namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Cavity;
    using Xunit;

    public sealed class TaskManagementExtensionsFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<TaskManagementExtensionCollection>()
                .DerivesFrom<ConfigurationElementCollection>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TaskManagementExtensionCollection());
        }
    }
}