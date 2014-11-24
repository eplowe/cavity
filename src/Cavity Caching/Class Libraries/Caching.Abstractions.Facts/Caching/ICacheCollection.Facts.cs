namespace Cavity.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Moq;
    using Xunit;

    public sealed class ICacheCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ICacheCollection>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Implements<IEnumerable<object>>()
                            .Result);
        }

        [Fact]
        public void index_string_get()
        {
            const string key = "example";
            var expected = new object();

            var mock = new Mock<ICacheCollection>();
            mock
                .SetupGet(x => x[key])
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object[key];

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void index_string_set()
        {
            const string key = "example";
            var value = new object();

            var mock = new Mock<ICacheCollection>();
            mock
                .SetupSet(x => x[key] = value)
                .Verifiable();

            mock.Object[key] = value;

            mock.VerifyAll();
        }

        [Fact]
        public void op_Add_string_object()
        {
            const string key = "example";
            var value = new object();

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Add(key, value))
                .Verifiable();

            mock.Object.Add(key, value);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Add_string_object_DateTime()
        {
            const string key = "example";
            var value = new object();
            var absoluteExpiration = DateTime.UtcNow;

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Add(key, value, absoluteExpiration))
                .Verifiable();

            mock.Object.Add(key, value, absoluteExpiration);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Add_string_object_TimeSpan()
        {
            const string key = "example";
            var value = new object();
            var slidingExpiration = TimeSpan.Zero;

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Add(key, value, slidingExpiration))
                .Verifiable();

            mock.Object.Add(key, value, slidingExpiration);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Clear()
        {
            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Clear())
                .Verifiable();

            mock.Object.Clear();

            mock.VerifyAll();
        }

        [Fact]
        public void op_ContainsKey_string()
        {
            const string key = "example";

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.ContainsKey(key))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.ContainsKey(key));

            mock.VerifyAll();
        }

        [Fact]
        public void op_GetOfT_string()
        {
            const string key = "example";
            var expected = new NetworkCredential();

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Get<NetworkCredential>(key))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Get<NetworkCredential>(key);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Get_string()
        {
            const string key = "example";
            var expected = new object();

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Get(key))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Get(key);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_RemoveOfT_string()
        {
            const string key = "example";
            var expected = new NetworkCredential();

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Remove<NetworkCredential>(key))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Remove<NetworkCredential>(key);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Remove_string()
        {
            const string key = "example";
            var expected = new object();

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Remove(key))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Remove(key);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Set_string_object()
        {
            const string key = "example";
            var value = new object();

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Set(key, value))
                .Verifiable();

            mock.Object.Set(key, value);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Set_string_object_DateTime()
        {
            const string key = "example";
            var value = new object();
            var absoluteExpiration = DateTime.UtcNow;

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Set(key, value, absoluteExpiration))
                .Verifiable();

            mock.Object.Set(key, value, absoluteExpiration);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Set_string_object_TimeSpan()
        {
            const string key = "example";
            var value = new object();
            var slidingExpiration = TimeSpan.Zero;

            var mock = new Mock<ICacheCollection>();
            mock
                .Setup(x => x.Set(key, value, slidingExpiration))
                .Verifiable();

            mock.Object.Set(key, value, slidingExpiration);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Count_get()
        {
            const int expected = 123;

            var mock = new Mock<ICacheCollection>();
            mock
                .SetupGet(x => x.Count)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Count;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}