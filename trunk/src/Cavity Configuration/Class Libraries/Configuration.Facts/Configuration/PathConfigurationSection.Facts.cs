namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;

    using Xunit;

    public sealed class PathConfigurationSectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<PathConfigurationSection>()
                            .DerivesFrom<ConfigurationSection>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void config()
        {
            Assert.NotNull(Config.ExeSection<DerivedPathConfigurationSection>(GetType().Assembly));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new PathConfigurationSection());
        }

        [Fact]
        public void op_Directory_string()
        {
            const string expected = @"C:\Temp";
            var actual = Config.ExeSection<PathConfigurationSection>(GetType().Assembly).Directory("temp").FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Directory_stringEmpty()
        {
            var obj = Config.ExeSection<PathConfigurationSection>(GetType().Assembly);

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Directory(string.Empty));
        }

        [Fact]
        public void op_Directory_stringNull()
        {
            var obj = Config.ExeSection<PathConfigurationSection>(GetType().Assembly);

            Assert.Throws<ArgumentNullException>(() => obj.Directory(null));
        }

        [Fact]
        public void op_Directory_string_whenNotConfigured()
        {
            var obj = Config.ExeSection<PathConfigurationSection>(GetType().Assembly);

            Assert.Null(obj.Directory("missing"));
        }

        [Fact]
        public void op_File_string()
        {
            const string expected = @"C:\example.txt";
            var actual = Config.ExeSection<PathConfigurationSection>(GetType().Assembly).File("example").FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_File_stringEmpty()
        {
            var obj = Config.ExeSection<PathConfigurationSection>(GetType().Assembly);

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.File(string.Empty));
        }

        [Fact]
        public void op_File_stringNull()
        {
            var obj = Config.ExeSection<PathConfigurationSection>(GetType().Assembly);

            Assert.Throws<ArgumentNullException>(() => obj.File(null));
        }

        [Fact]
        public void op_File_string_whenNotConfigured()
        {
            var obj = Config.ExeSection<PathConfigurationSection>(GetType().Assembly);

            Assert.Null(obj.File("missing"));
        }

        [Fact]
        public void prop_Directories()
        {
            Assert.True(new PropertyExpectations<PathConfigurationSection>(p => p.Directories)
                            .TypeIs<AddRemoveClearConfigurationElementCollection<DirectoryInfo>>()
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_Files()
        {
            Assert.True(new PropertyExpectations<PathConfigurationSection>(p => p.Files)
                            .TypeIs<AddRemoveClearConfigurationElementCollection<FileInfo>>()
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .Result);
        }
    }
}