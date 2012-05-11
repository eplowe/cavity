namespace Cavity.Reflection
{
    using System;
    using System.IO;
    using System.Reflection;

    using Xunit;
    using Xunit.Extensions;

    public sealed class AssemblyExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(AssemblyExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_Directory_Assembly()
        {
            var assembly = typeof(AbsoluteUri).Assembly;
            var location = new FileInfo(assembly.Location);

            // ReSharper disable PossibleNullReferenceException
            var expected = location.Directory.FullName;

            // ReSharper restore PossibleNullReferenceException
            var actual = assembly.Directory().FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Directory_AssemblyNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Assembly).Directory());
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void op_ToDirectory_stringEmpty(string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => AssemblyExtensionMethods.ToDirectory(value));
        }

        [Fact]
        public void op_ToDirectory_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => AssemblyExtensionMethods.ToDirectory(null));
        }

        [Fact]
        public void op_ToDirectory_stringDirectoryRoot()
        {
            Assert.Throws<DirectoryNotFoundException>(() => AssemblyExtensionMethods.ToDirectory(@"C:\"));
        }
    }
}