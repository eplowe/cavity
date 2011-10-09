namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
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
        public void op_Add_DirectoryConfigurationElement()
        {
            var element = new DirectoryConfigurationElement
            {
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection
            {
                element
            };

            Assert.True(obj.Contains(element));
        }

        [Fact]
        public void op_Add_DirectoryConfigurationElementNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DirectoryConfigurationElementCollection().Add(null));
        }

        [Fact]
        public void op_Add_string_DirectoryInfo()
        {
            var obj = new DirectoryConfigurationElementCollection
            {
                {
                    "C", new DirectoryInfo(@"C:\")
                    }
            };

            Assert.True(obj.Contains("C", new DirectoryInfo(@"C:\")));
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
        public void op_Contains_DirectoryConfigurationElement()
        {
            var element = new DirectoryConfigurationElement
            {
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection
            {
                element
            };

            Assert.True(obj.Contains(element));
        }

        [Fact]
        public void op_Contains_stringMissing_DirectoryInfo()
        {
            var obj = new DirectoryConfigurationElementCollection
            {
                new DirectoryConfigurationElement
                {
                    Name = "C",
                    Directory = new DirectoryInfo(@"C:\")
                }
            };

            Assert.False(obj.Contains("D", new DirectoryInfo(@"C:\")));
        }

        [Fact]
        public void op_Contains_string_DirectoryInfo()
        {
            var obj = new DirectoryConfigurationElementCollection
            {
                new DirectoryConfigurationElement
                {
                    Name = "C",
                    Directory = new DirectoryInfo(@"C:\")
                }
            };

            Assert.True(obj.Contains("C", new DirectoryInfo(@"C:\")));
        }

        [Fact]
        public void op_Contains_string_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DirectoryConfigurationElementCollection().Contains("C", null));
        }

        [Fact]
        public void op_CopyTo_DirectoryConfigurationElement_int()
        {
            var expected = new DirectoryConfigurationElement
            {
                Name = "C",
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection
            {
                expected,
                new DirectoryConfigurationElement
                {
                    Name = "D",
                    Directory = new DirectoryInfo(@"D:\")
                }
            };

            var array = new DirectoryConfigurationElement[obj.Count];
            obj.CopyTo(array, 0);

            var actual = array.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Remove_DirectoryConfigurationElement()
        {
            var element = new DirectoryConfigurationElement
            {
                Name = "C",
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection
            {
                new DirectoryConfigurationElement
                {
                    Name = "D",
                    Directory = new DirectoryInfo(@"D:\")
                },
                element
            };

            Assert.True(obj.Remove(element));
            Assert.False(obj.Contains(element));
        }

        [Fact]
        public void op_Remove_DirectoryConfigurationElement_whenEmpty()
        {
            var element = new DirectoryConfigurationElement
            {
                Directory = new DirectoryInfo(@"C:\")
            };
            var obj = new DirectoryConfigurationElementCollection();

            Assert.False(obj.Remove(element));
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