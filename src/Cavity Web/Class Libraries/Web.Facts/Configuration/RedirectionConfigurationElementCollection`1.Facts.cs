namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Linq;
    using Cavity;
    using Xunit;

    public sealed class RedirectionConfigurationElementCollectionOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RedirectionConfigurationElementCollection<AbsoluteUri>>()
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
            Assert.NotNull(new RedirectionConfigurationElementCollection<AbsoluteUri>());
        }

        [Fact]
        public void op_Add_NameValueConfigurationElement()
        {
            var element = new RedirectionConfigurationElement<AbsoluteUri>("http://example.com/", "http://example.net/");
            var obj = new RedirectionConfigurationElementCollection<AbsoluteUri>
            {
                element
            };

            Assert.True(obj.Contains(element));
        }

        [Fact]
        public void op_Add_NameValueConfigurationElementNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RedirectionConfigurationElementCollection<AbsoluteUri>().Add(null));
        }

        [Fact]
        public void op_Add_string_T()
        {
            var obj = new RedirectionConfigurationElementCollection<AbsoluteUri>
            {
                {
                    "http://example.com/", "http://example.net/"
                    }
            };

            Assert.Equal((AbsoluteUri)"http://example.com/", obj.First().From);
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new RedirectionConfigurationElementCollection<AbsoluteUri>
            {
                {
                    "http://example.com/", "http://example.net/"
                    }
            };

            Assert.NotEmpty(obj);
            obj.Clear();
            Assert.Empty(obj);
        }

        [Fact]
        public void op_Contains_NameValueConfigurationElement()
        {
            var element = new RedirectionConfigurationElement<AbsoluteUri>("http://example.com/", "http://example.net/");
            var obj = new RedirectionConfigurationElementCollection<AbsoluteUri>
            {
                element
            };

            Assert.True(obj.Contains(element));
        }

        [Fact]
        public void op_CopyTo_NameValueConfigurationElement_int()
        {
            var expected = new RedirectionConfigurationElement<AbsoluteUri>("http://example.com/", "http://example.net/");
            var obj = new RedirectionConfigurationElementCollection<AbsoluteUri>
            {
                expected,
                new RedirectionConfigurationElement<AbsoluteUri>("http://example.org/", "http://example.co.uk/")
            };

            var array = new RedirectionConfigurationElement<AbsoluteUri>[obj.Count];
            obj.CopyTo(array, 0);

            var actual = array.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Remove_NameValueConfigurationElement()
        {
            var element = new RedirectionConfigurationElement<AbsoluteUri>("http://example.com/", "http://example.net/");
            var obj = new RedirectionConfigurationElementCollection<AbsoluteUri>
            {
                new RedirectionConfigurationElement<AbsoluteUri>("http://example.org/", "http://example.co.uk/"),
                element
            };

            Assert.True(obj.Remove(element));
            Assert.False(obj.Contains(element));
        }

        [Fact]
        public void op_Remove_NameValueConfigurationElement_whenEmpty()
        {
            var element = new RedirectionConfigurationElement<AbsoluteUri>("http://example.com/", "http://example.net/");
            var obj = new RedirectionConfigurationElementCollection<AbsoluteUri>();

            Assert.False(obj.Remove(element));
        }

        [Fact]
        public void prop_CollectionType()
        {
            Assert.True(new PropertyExpectations<RedirectionConfigurationElementCollection<AbsoluteUri>>(p => p.CollectionType)
                            .TypeIs<ConfigurationElementCollectionType>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_CollectionType_get()
        {
            const ConfigurationElementCollectionType expected = ConfigurationElementCollectionType.AddRemoveClearMap;
            var actual = new RedirectionConfigurationElementCollection<AbsoluteUri>().CollectionType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IsReadOnly()
        {
            Assert.True(new PropertyExpectations<RedirectionConfigurationElementCollection<AbsoluteUri>>(p => p.IsReadOnly)
                            .TypeIs<bool>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsReadOnly_get()
        {
            Assert.False(new RedirectionConfigurationElementCollection<AbsoluteUri>().IsReadOnly);
        }
    }
}