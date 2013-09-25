namespace Cavity.IO
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class CurrentTempDirectoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CurrentTempDirectory>().DerivesFrom<TempDirectory>()
                                                                    .IsConcreteClass()
                                                                    .IsUnsealed()
                                                                    .HasDefaultConstructor()
                                                                    .IsNotDecorated()
                                                                    .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var directory = new CurrentTempDirectory())
            {
                Assert.NotNull(directory);
            }
        }

        [Fact]
        public void prop_Location()
        {
            var current = new DirectoryInfo(Environment.CurrentDirectory);

            var expected = new DirectoryInfo(current.Root.FullName).ToDirectory("Temp", true).FullName;
            var actual = CurrentTempDirectory.Location.FullName;

            Assert.Equal(expected, actual);
        }
    }
}