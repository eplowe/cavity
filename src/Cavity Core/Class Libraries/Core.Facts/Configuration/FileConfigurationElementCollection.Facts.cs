namespace Cavity.Configuration
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using Xunit;

    public sealed class FileConfigurationElementCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileConfigurationElementCollection>()
                            .DerivesFrom<ConfigurationElementCollection>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FileConfigurationElementCollection());
        }

        [Fact]
        public void op_Add_FileConfigurationElement()
        {
            var element = new FileConfigurationElement
            {
                Name = "example",
                File = new FileInfo(@"C:\example.txt")
            };

            new FileConfigurationElementCollection().Add(element);
        }

        [Fact]
        public void op_Add_string_FileInfo()
        {
            var obj = new FileConfigurationElementCollection
            {
                {
                    "C", new FileInfo(@"C:\")
                    }
            };

            Assert.True(obj.Contains("C", new FileInfo(@"C:\")));
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new FileConfigurationElementCollection
            {
                new FileConfigurationElement
                {
                    Name = "example",
                    File = new FileInfo(@"C:\example.txt")
                }
            };

            Assert.NotEmpty(obj);
            obj.Clear();
            Assert.Empty(obj);
        }

        [Fact]
        public void op_Contains_FileConfigurationElement()
        {
            var element = new FileConfigurationElement
            {
                Name = "example",
                File = new FileInfo(@"C:\example.txt")
            };
            var obj = new FileConfigurationElementCollection
            {
                element
            };

            Assert.True(obj.Contains(element));
        }

        [Fact]
        public void op_Contains_stringMissing_FileInfo()
        {
            var obj = new FileConfigurationElementCollection
            {
                new FileConfigurationElement
                {
                    Name = "foo",
                    File = new FileInfo(@"C:\example.txt")
                }
            };

            Assert.False(obj.Contains("bar", new FileInfo(@"C:\example.txt")));
        }

        [Fact]
        public void op_Contains_string_FileInfo()
        {
            var obj = new FileConfigurationElementCollection
            {
                new FileConfigurationElement
                {
                    Name = "example",
                    File = new FileInfo(@"C:\example.txt")
                }
            };

            Assert.True(obj.Contains("example", new FileInfo(@"C:\example.txt")));
        }

        [Fact]
        public void op_CopyTo_FileConfigurationElement_int()
        {
            var expected = new FileConfigurationElement
            {
                Name = "C",
                File = new FileInfo(@"C:\example.txt")
            };
            var obj = new FileConfigurationElementCollection
            {
                expected,
                new FileConfigurationElement
                {
                    Name = "D",
                    File = new FileInfo(@"D:\example.txt")
                }
            };

            var array = new FileConfigurationElement[obj.Count];
            obj.CopyTo(array, 0);

            var actual = array.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Remove_FileConfigurationElement()
        {
            var element = new FileConfigurationElement
            {
                Name = "C",
                File = new FileInfo(@"C:\example.txt")
            };
            var obj = new FileConfigurationElementCollection
            {
                new FileConfigurationElement
                {
                    Name = "D",
                    File = new FileInfo(@"D:\example.txt")
                },
                element
            };

            Assert.True(obj.Remove(element));
            Assert.False(obj.Contains(element));
        }

        [Fact]
        public void op_Remove_FileConfigurationElement_whenEmpty()
        {
            var element = new FileConfigurationElement
            {
                Name = "example",
                File = new FileInfo(@"C:\example.txt")
            };
            var obj = new FileConfigurationElementCollection();

            Assert.False(obj.Remove(element));
        }

        [Fact]
        public void prop_CollectionType()
        {
            Assert.True(new PropertyExpectations<FileConfigurationElementCollection>(p => p.CollectionType)
                            .TypeIs<ConfigurationElementCollectionType>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_CollectionType_get()
        {
            const ConfigurationElementCollectionType expected = ConfigurationElementCollectionType.AddRemoveClearMap;
            var actual = new FileConfigurationElementCollection().CollectionType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IsReadOnly()
        {
            Assert.True(new PropertyExpectations<FileConfigurationElementCollection>(p => p.IsReadOnly)
                            .TypeIs<bool>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsReadOnly_get()
        {
            Assert.False(new FileConfigurationElementCollection().IsReadOnly);
        }
    }
}