namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Cavity;
    using Xunit;

    public sealed class TaskManagementConfigurationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TaskManagementConfiguration>()
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
            Assert.NotNull(new TaskManagementConfiguration());
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<TaskManagementConfiguration>(x => x.RefreshRate)
                            .TypeIs<TimeSpan>()
                            .IsDecoratedWith<TimeSpanValidatorAttribute>()
                            .Result);
        }
    }
}