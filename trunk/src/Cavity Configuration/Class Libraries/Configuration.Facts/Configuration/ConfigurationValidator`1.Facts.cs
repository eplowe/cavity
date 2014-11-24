namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Xunit;

    public sealed class ConfigurationValidatorOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ConfigurationValidator<AbsoluteUri>>()
                            .DerivesFrom<ConfigurationValidatorBase>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ConfigurationValidator<AbsoluteUri>());
        }

        [Fact]
        public void op_CanValidate_Type()
        {
            Assert.True(new ConfigurationValidator<AbsoluteUri>().CanValidate(typeof(AbsoluteUri)));
        }

        [Fact]
        public void op_Validate_ObjectDirectoryInfo()
        {
            new ConfigurationValidator<AbsoluteUri>().Validate(new AbsoluteUri("http://example.com/"));
        }

        [Fact]
        public void op_Validate_ObjectNull()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new ConfigurationValidator<AbsoluteUri>().Validate(null));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_Validate_ObjectString()
        {
            Assert.Throws<InvalidCastException>(() => new ConfigurationValidator<AbsoluteUri>().Validate("example"));
        }
    }
}