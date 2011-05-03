namespace Cavity.Configuration
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using Cavity;
    using Xunit;

    public sealed class DirectoryConfigurationElementCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DirectoryConfigurationElementCollection>()
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
            Assert.NotNull(new DirectoryConfigurationElementCollection());
        }

        [Fact]
        public void op_Add_PackageManagementPickup()
        {
            var pickup = new DirectoryConfigurationElement
            {
                Directory = new DirectoryInfo(@"C:\")
            };

            new DirectoryConfigurationElementCollection().Add(pickup);
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new DirectoryConfigurationElementCollection
            {
                new DirectoryConfigurationElement
                {
                    Directory = new DirectoryInfo(@"C:\")
                }
            };

            Assert.NotEmpty(obj);
            obj.Clear();
            Assert.Empty(obj);
        }

        [Fact]
        public void op_Contains_PackageManagementPickup()
        {
            var pickup = new DirectoryConfigurationElement
            {
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection
            {
                pickup
            };

            Assert.True(obj.Contains(pickup));
        }

        [Fact]
        public void op_Contains_string()
        {
            var dir = new DirectoryInfo(@"C:\");
            var obj = new DirectoryConfigurationElementCollection
            {
                new DirectoryConfigurationElement
                {
                    Directory = dir
                }
            };

            Assert.True(obj.Contains(dir.FullName));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            Assert.False(new DirectoryConfigurationElementCollection().Contains(null as string));
        }

        [Fact]
        public void op_CopyTo_PackageManagementPickup_int()
        {
            var expected = new DirectoryConfigurationElement
            {
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection
            {
                expected,
                new DirectoryConfigurationElement
                {
                    Directory = new DirectoryInfo(@"D:\")
                }
            };

            var array = new DirectoryConfigurationElement[obj.Count];
            obj.CopyTo(array, 0);

            var actual = array.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Remove_PackageManagementPickup()
        {
            var pickup = new DirectoryConfigurationElement
            {
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection
            {
                new DirectoryConfigurationElement
                {
                    Directory = new DirectoryInfo(@"D:\")
                },
                pickup
            };

            Assert.True(obj.Remove(pickup));
            Assert.False(obj.Contains(pickup));
        }

        [Fact]
        public void op_Remove_PackageManagementPickup_whenEmpty()
        {
            var pickup = new DirectoryConfigurationElement
            {
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection();

            Assert.False(obj.Remove(pickup));
        }

        [Fact]
        public void prop_CollectionType()
        {
            Assert.True(new PropertyExpectations<DirectoryConfigurationElementCollection>(p => p.CollectionType)
                            .TypeIs<ConfigurationElementCollectionType>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_CollectionType_get()
        {
            const ConfigurationElementCollectionType expected = ConfigurationElementCollectionType.AddRemoveClearMap;
            var actual = new DirectoryConfigurationElementCollection().CollectionType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IsReadOnly()
        {
            Assert.True(new PropertyExpectations<DirectoryConfigurationElementCollection>(p => p.IsReadOnly)
                            .TypeIs<bool>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsReadOnly_get()
        {
            Assert.False(new DirectoryConfigurationElementCollection().IsReadOnly);
        }
    }
}