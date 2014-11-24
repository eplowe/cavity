namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using Xunit;

    public sealed class AddRemoveClearConfigurationElementCollectionOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AddRemoveClearConfigurationElementCollection<DirectoryInfo>>()
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
            Assert.NotNull(new AddRemoveClearConfigurationElementCollection<DirectoryInfo>());
        }

        [Fact]
        public void op_Add_NameValueConfigurationElement()
        {
            var element = new NameValueConfigurationElement<DirectoryInfo>("C", new DirectoryInfo(@"C:\"));
            var obj = new AddRemoveClearConfigurationElementCollection<DirectoryInfo>
                          {
                              element
                          };

            Assert.True(obj.Contains(element));
        }

        [Fact]
        public void op_Add_NameValueConfigurationElementNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddRemoveClearConfigurationElementCollection<DirectoryInfo>().Add(null));
        }

        [Fact]
        public void op_Add_string_T()
        {
            var obj = new AddRemoveClearConfigurationElementCollection<DirectoryInfo>
                          {
                              {
                                  "C", new DirectoryInfo(@"C:\")
                              }
                          };

            Assert.Equal("C", obj.First().Name);
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new AddRemoveClearConfigurationElementCollection<DirectoryInfo>
                          {
                              new NameValueConfigurationElement<DirectoryInfo>("C", new DirectoryInfo(@"C:\"))
                          };

            Assert.NotEmpty(obj);
            obj.Clear();
            Assert.Empty(obj);
        }

        [Fact]
        public void op_Contains_NameValueConfigurationElement()
        {
            var element = new NameValueConfigurationElement<DirectoryInfo>("C", new DirectoryInfo(@"C:\"));
            var obj = new AddRemoveClearConfigurationElementCollection<DirectoryInfo>
                          {
                              element
                          };

            Assert.True(obj.Contains(element));
        }

        [Fact]
        public void op_CopyTo_NameValueConfigurationElement_int()
        {
            var expected = new NameValueConfigurationElement<DirectoryInfo>("C", new DirectoryInfo(@"C:\"));
            var obj = new AddRemoveClearConfigurationElementCollection<DirectoryInfo>
                          {
                              expected,
                              new NameValueConfigurationElement<DirectoryInfo>("D", new DirectoryInfo(@"D:\"))
                          };

            var array = new NameValueConfigurationElement<DirectoryInfo>[obj.Count];
            obj.CopyTo(array, 0);

            var actual = array.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Remove_NameValueConfigurationElement()
        {
            var element = new NameValueConfigurationElement<DirectoryInfo>("C", new DirectoryInfo(@"C:\"));
            var obj = new AddRemoveClearConfigurationElementCollection<DirectoryInfo>
                          {
                              new NameValueConfigurationElement<DirectoryInfo>("D", new DirectoryInfo(@"D:\")),
                              element
                          };

            Assert.True(obj.Remove(element));
            Assert.False(obj.Contains(element));
        }

        [Fact]
        public void op_Remove_NameValueConfigurationElement_whenEmpty()
        {
            var element = new NameValueConfigurationElement<DirectoryInfo>("C", new DirectoryInfo(@"C:\"));
            var obj = new AddRemoveClearConfigurationElementCollection<DirectoryInfo>();

            Assert.False(obj.Remove(element));
        }

        [Fact]
        public void prop_CollectionType()
        {
            Assert.True(new PropertyExpectations<AddRemoveClearConfigurationElementCollection<DirectoryInfo>>(p => p.CollectionType)
                            .TypeIs<ConfigurationElementCollectionType>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_CollectionType_get()
        {
            const ConfigurationElementCollectionType expected = ConfigurationElementCollectionType.AddRemoveClearMap;
            var actual = new AddRemoveClearConfigurationElementCollection<DirectoryInfo>().CollectionType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IsReadOnly()
        {
            Assert.True(new PropertyExpectations<AddRemoveClearConfigurationElementCollection<DirectoryInfo>>(p => p.IsReadOnly)
                            .TypeIs<bool>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsReadOnly_get()
        {
            Assert.False(new AddRemoveClearConfigurationElementCollection<DirectoryInfo>().IsReadOnly);
        }
    }
}