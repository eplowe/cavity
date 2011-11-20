namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using Cavity;
    using Cavity.Net;
    using Xunit;

    public sealed class RedirectionConfigurationSectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RedirectionConfigurationSection>()
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
            Assert.NotNull(Config.ExeSection<DerivedRedirectionConfigurationSection>(GetType().Assembly));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RedirectionConfigurationSection());
        }

        [Fact]
        public void op_Redirect_Uri_whenToAbsolutePreservingQuery()
        {
            AbsoluteUri expected = "http://www.example.net/to?name=value";
            var actual = Config.ExeSection<RedirectionConfigurationSection>(GetType().Assembly).Redirect(new Uri("http://www.example.com/from?name=value"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Redirect_Uri_whenToAbsoluteDiscardQuery()
        {
            AbsoluteUri expected = "http://www.example.net/that?from=this";
            var actual = Config.ExeSection<RedirectionConfigurationSection>(GetType().Assembly).Redirect(new Uri("http://www.example.com/from?this=that"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Redirect_Uri_whenToHost()
        {
            AbsoluteUri expected = "http://www.example.com/path?name=value";
            var actual = Config.ExeSection<RedirectionConfigurationSection>(GetType().Assembly).Redirect(new Uri("http://example.com/path?name=value"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Redirect_Uri_whenToRelativePreservingQuery()
        {
            AbsoluteUri expected = "http://www.example.net/destination?name=value";
            var actual = Config.ExeSection<RedirectionConfigurationSection>(GetType().Assembly).Redirect(new Uri("http://www.example.net/source?name=value"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Redirect_Uri_whenToRelativeDiscardQuery()
        {
            AbsoluteUri expected = "http://www.example.net/that?from=this";
            var actual = Config.ExeSection<RedirectionConfigurationSection>(GetType().Assembly).Redirect(new Uri("http://www.example.net/source?this=that"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Redirect_Uri_whenNotConfigured()
        {
            Assert.Null(Config.ExeSection<RedirectionConfigurationSection>(GetType().Assembly).Redirect(new Uri("http://example.co.uk/")));
        }

        [Fact]
        public void op_Redirect_UriNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RedirectionConfigurationSection().Redirect(null));
        }

        [Fact]
        public void op_Redirect_UriRelative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RedirectionConfigurationSection().Redirect(new Uri("/", UriKind.Relative)));
        }

        [Fact]
        public void prop_Absolute()
        {
            Assert.True(new PropertyExpectations<RedirectionConfigurationSection>(p => p.Absolutes)
                            .TypeIs<RedirectionConfigurationElementCollection<AbsoluteUri>>()
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_Hosts()
        {
            Assert.True(new PropertyExpectations<RedirectionConfigurationSection>(p => p.Hosts)
                            .TypeIs<RedirectionConfigurationElementCollection<Host>>()
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_Relative()
        {
            Assert.True(new PropertyExpectations<RedirectionConfigurationSection>(p => p.Relatives)
                            .TypeIs<RedirectionConfigurationElementCollection<RelativeUri>>()
                            .IsDecoratedWith<ConfigurationPropertyAttribute>()
                            .Result);
        }
    }
}