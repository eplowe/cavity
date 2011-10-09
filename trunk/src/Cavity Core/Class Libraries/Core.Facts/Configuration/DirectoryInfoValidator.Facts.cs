namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
    using Xunit;

    public sealed class DirectoryInfoValidatorFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DirectoryInfoValidator>()
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
            Assert.NotNull(new DirectoryInfoValidator());
        }

        [Fact]
        public void op_CanValidate_Type()
        {
            Assert.True(new DirectoryInfoValidator().CanValidate(typeof(DirectoryInfo)));
        }

        [Fact]
        public void op_Validate_ObjectDirectoryInfo()
        {
            new DirectoryInfoValidator().Validate(new DirectoryInfo("C:\\"));
        }

        [Fact]
        public void op_Validate_ObjectNull()
        {
// ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new DirectoryInfoValidator().Validate(null));
// ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_Validate_ObjectString()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DirectoryInfoValidator().Validate("example"));
        }
    }
}