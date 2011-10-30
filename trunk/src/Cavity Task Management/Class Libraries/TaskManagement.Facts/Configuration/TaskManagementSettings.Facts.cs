namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Xunit;

    public sealed class TaskManagementSettingsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TaskManagementSettings>()
                            .DerivesFrom<ConfigurationSection>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void config()
        {
            Assert.NotNull(Config.ExeSection<TaskManagementSettings>(typeof(TaskManagementSettings).Assembly));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TaskManagementSettings());
        }

        [Fact]
        public void prop_Extensions()
        {
            Assert.True(new PropertyExpectations<TaskManagementSettings>(x => x.Extensions)
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .TypeIs<TaskManagementExtensionCollection>()
                            .DefaultValueIsNotNull()
                            .Result);
        }

        [Fact]
        public void prop_RefreshRate()
        {
            Assert.True(new PropertyExpectations<TaskManagementSettings>(x => x.RefreshRate)
                            .IsDecoratedWith<TimeSpanValidatorAttribute>()
                            .TypeIs<TimeSpan>()
                            .DefaultValueIsNotNull()
                            .Set(new TimeSpan(123))
                            .Result);
        }
    }
}