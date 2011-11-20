namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Cavity;
    using Xunit;

    public sealed class RedirectionConfigurationElementOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RedirectionConfigurationElement<AbsoluteUri>>()
                            .DerivesFrom<ConfigurationElement>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RedirectionConfigurationElement<AbsoluteUri>());
        }

        [Fact]
        public void ctor_AbsoluteUri_AbsoluteUri()
        {
            AbsoluteUri from = "http://example.com/";
            AbsoluteUri to = "http://example.net/";

            Assert.NotNull(new RedirectionConfigurationElement<AbsoluteUri>(from, to));
        }

        [Fact]
        public void prop_From()
        {
            Assert.True(new PropertyExpectations<RedirectionConfigurationElement<AbsoluteUri>>(p => p.From)
                            .IsAutoProperty<AbsoluteUri>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_To()
        {
            Assert.True(new PropertyExpectations<RedirectionConfigurationElement<AbsoluteUri>>(p => p.To)
                            .IsAutoProperty<AbsoluteUri>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}