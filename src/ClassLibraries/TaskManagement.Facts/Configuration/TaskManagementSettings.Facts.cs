namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Cavity;
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
        public void ctor()
        {
            Assert.NotNull(new TaskManagementSettings());
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<TaskManagementSettings>(x => x.RefreshRate)
                            .TypeIs<TimeSpan>()
                            .IsDecoratedWith<TimeSpanValidatorAttribute>()
                            .Result);
        }
    }
}