namespace Cavity.Caching
{
    using System;
    using System.Net;

    using Cavity;

    using Moq;

    using Xunit;

    public sealed class ICacheCollection35Facts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ICacheCollection35>()
                            .IsInterface()
                            .Implements<ICacheCollection>()
                            .Result);
        }

        [Fact]
        public void op_Get_string_FuncOfObject()
        {
            const string key = "example";

            var mock = new Mock<ICacheCollection35>();
            mock
                .Setup(x => x.Get(key, () => Lazy()))
                .Returns(null)
                .Verifiable();

            Assert.Null(mock.Object.Get(key, () => Lazy()));
        }

        [Fact]
        public void op_Get_string_FuncOfICacheCollectionObject()
        {
            const string key = "example";

            var mock = new Mock<ICacheCollection35>();
            mock
                .Setup(x => x.Get(key, () => Lazy(mock.Object)))
                .Returns(null)
                .Verifiable();

            Assert.Null(mock.Object.Get(key, () => Lazy(mock.Object)));
        }

        [Fact]
        public void op_GetOfT_string_FuncOfObject()
        {
            const string key = "example";

            var mock = new Mock<ICacheCollection35>();
            mock
                .Setup(x => x.Get(key, () => Lazy<NetworkCredential>()))
                .Returns(default(NetworkCredential))
                .Verifiable();

            Assert.Null(mock.Object.Get(key, () => Lazy()));
        }

        [Fact]
        public void op_GetOfT_string_FuncOfICacheCollectionObject()
        {
            const string key = "example";

            var mock = new Mock<ICacheCollection35>();
            mock
                .Setup(x => x.Get(key, () => Lazy<NetworkCredential>(mock.Object)))
                .Returns(default(NetworkCredential))
                .Verifiable();

            Assert.Null(mock.Object.Get(key, () => Lazy(mock.Object)));
        }

        private static object Lazy()
        {
            return null;
        }

        private static T Lazy<T>()
        {
            return default(T);
        }

        private static object Lazy(ICacheCollection cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException("cache");
            }

            return null;
        }

        private static T Lazy<T>(ICacheCollection cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException("cache");
            }

            return default(T);
        }
    }
}