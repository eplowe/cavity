namespace Cavity.Configuration
{
    using System.Configuration;

    using Xunit;

    public sealed class TaskManagementExtensionsFacts
    {
        [Fact]
        public void a_definition()
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

        [Fact]
        public void prop_CollectionType()
        {
            Assert.True(new PropertyExpectations<TaskManagementExtensionCollection>(p => p.CollectionType)
                            .TypeIs<ConfigurationElementCollectionType>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_CollectionType_get()
        {
            const ConfigurationElementCollectionType expected = ConfigurationElementCollectionType.AddRemoveClearMap;
            var actual = new TaskManagementExtensionCollection().CollectionType;

            Assert.Equal(expected, actual);
        }
    }
}