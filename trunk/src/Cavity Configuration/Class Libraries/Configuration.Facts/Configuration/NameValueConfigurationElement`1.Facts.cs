namespace Cavity.Configuration
{
    using System.Configuration;

    using Xunit;

    public sealed class NameValueConfigurationElementOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<NameValueConfigurationElement<AbsoluteUri>>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new NameValueConfigurationElement<AbsoluteUri>());
        }

        [Fact]
        public void ctor_string_T()
        {
            Assert.NotNull(new NameValueConfigurationElement<AbsoluteUri>("example", new AbsoluteUri("http://example.com/")));
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<NameValueConfigurationElement<AbsoluteUri>>(p => p.Name)
                            .IsAutoProperty(string.Empty)
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<NameValueConfigurationElement<AbsoluteUri>>(p => p.Value)
                            .IsAutoProperty<AbsoluteUri>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}