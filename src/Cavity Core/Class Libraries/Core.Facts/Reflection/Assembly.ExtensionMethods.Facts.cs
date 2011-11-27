namespace Cavity.Reflection
{
    using System;
    using System.IO;
    using System.Reflection;
    using Cavity;
    using Xunit;

    public sealed class AssemblyExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(AssemblyExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_Directory_AssemblyNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Assembly).Directory());
        }

        [Fact]
        public void op_Directory_Assembly()
        {
            var assembly = typeof(AbsoluteUri).Assembly;
            var location = new FileInfo(assembly.Location);

            var expected = location.Directory.FullName;
            var actual = assembly.Directory().FullName;

            Assert.Equal(expected, actual);
        }
    }
}