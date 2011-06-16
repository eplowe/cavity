namespace Cavity.Configuration
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using Cavity;
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
        public void op_Add_PackageManagementPickup()
        {
            var pickup = new FileConfigurationElement
            {
                File = new FileInfo(@"C:\example.txt")
            };

            new FileConfigurationElementCollection().Add(pickup);
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new FileConfigurationElementCollection
            {
                new FileConfigurationElement
                {
                    File = new FileInfo(@"C:\example.txt")
                }
            };

            Assert.NotEmpty(obj);
            obj.Clear();
            Assert.Empty(obj);
        }

        [Fact]
        public void op_Contains_PackageManagementPickup()
        {
            var pickup = new FileConfigurationElement
            {
                File = new FileInfo(@"C:\example.txt")
            };
            var obj = new FileConfigurationElementCollection
            {
                pickup
            };

            Assert.True(obj.Contains(pickup));
        }

        [Fact]
        public void op_Contains_string()
        {
            var dir = new FileInfo(@"C:\example.txt");
            var obj = new FileConfigurationElementCollection
            {
                new FileConfigurationElement
                {
                    File = dir
                }
            };

            Assert.True(obj.Contains(dir.FullName));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            Assert.False(new FileConfigurationElementCollection().Contains(null as string));
        }

        [Fact]
        public void op_CopyTo_PackageManagementPickup_int()
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
        public void op_Remove_PackageManagementPickup()
        {
            var pickup = new FileConfigurationElement
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
                pickup
            };

            Assert.True(obj.Remove(pickup));
            Assert.False(obj.Contains(pickup));
        }

        [Fact]
        public void op_Remove_PackageManagementPickup_whenEmpty()
        {
            var pickup = new FileConfigurationElement
            {
                File = new FileInfo(@"C:\example.txt")
            };
            var obj = new FileConfigurationElementCollection();

            Assert.False(obj.Remove(pickup));
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