namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
    using Xunit;

    public sealed class FileInfoValidatorFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileInfoValidator>()
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
            Assert.NotNull(new FileInfoValidator());
        }

        [Fact]
        public void op_CanValidate_Type()
        {
            Assert.True(new FileInfoValidator().CanValidate(typeof(FileInfo)));
        }

        [Fact]
        public void op_Validate_ObjectFileInfo()
        {
            new FileInfoValidator().Validate(new FileInfo("C:\\example.txt"));
        }

        [Fact]
        public void op_Validate_ObjectNull()
        {
// ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new FileInfoValidator().Validate(null));
// ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_Validate_ObjectString()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new FileInfoValidator().Validate("example"));
        }
    }
}