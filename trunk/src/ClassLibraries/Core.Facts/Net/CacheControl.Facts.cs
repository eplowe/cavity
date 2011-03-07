namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class CacheControlFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CacheControl>()
                .DerivesFrom<HttpHeader>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new CacheControl());
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new CacheControl("public"));
        }

        [Fact]
        public void prop_NoCache()
        {
            const string expected = "Cache-Control: no-cache";
            var actual = CacheControl.NoCache;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NoStore()
        {
            const string expected = "Cache-Control: no-store";
            var actual = CacheControl.NoStore;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Private()
        {
            const string expected = "Cache-Control: private";
            var actual = CacheControl.Private;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Public()
        {
            const string expected = "Cache-Control: public";
            var actual = CacheControl.Public;

            Assert.Equal(expected, actual);
        }
    }
}