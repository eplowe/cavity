namespace Cavity.Caching
{
    using System;
    using System.Linq;

    using Cavity;
    using Xunit;

    public sealed class WebCacheCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<WebCacheCollection>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .Implements<ICacheCollection35>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new WebCacheCollection());
        }

        [Fact]
        public void indexer_string()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = new object();
                obj[key] = expected;
                var actual = obj[key];

                Assert.Same(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Add_string_object_DateTime()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = new object();
                obj.Add(key, expected, DateTime.UtcNow.AddDays(1));
                var actual = obj[key];

                Assert.Same(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Add_string_object_TimeSpan()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = new object();
                obj.Add(key, expected, new TimeSpan(123456));
                var actual = obj[key];

                Assert.Same(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new WebCacheCollection
                          {
                              { AlphaDecimal.Random(), new object() }
                          };
            Assert.Equal(1, obj.Count);
            obj.Clear();
            Assert.Equal(0, obj.Count);
        }

        [Fact]
        public void op_ContainsKey_string_whenFalse()
        {
            var obj = new WebCacheCollection();
            try
            {
                obj[AlphaDecimal.Random()] = new object();

                Assert.False(obj.ContainsKey(AlphaDecimal.Random()));
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_ContainsKey_string_whenTrue()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                obj[key] = new object();

                Assert.True(obj.ContainsKey(key));
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = new object();
                obj[key] = expected;
                var actual = obj.First();

                Assert.Same(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_GetEnumerator_whenEmpty()
        {
            Assert.Empty(new WebCacheCollection());
        }

        [Fact]
        public void op_GetOfT_string()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                const int expected = 123;
                obj[key] = expected;
                var actual = obj.Get<int>(key);

                Assert.Equal(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = new object();
                obj[key] = expected;
                var actual = obj.Get(key);

                Assert.Same(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string_FuncICacheCollectionObject()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = new object();
                obj[key] = expected;

                var actual = obj.Get(key, AddToCache);

                Assert.Equal(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string_FuncICacheCollectionObjectNull()
        {
            var obj = new WebCacheCollection();

            Assert.Throws<ArgumentNullException>(() => obj.Get(AlphaDecimal.Random(), null as Func<ICacheCollection, object>));
        }

        [Fact]
        public void op_Get_string_FuncICacheCollectionObject_whenMissing()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var actual = obj.Get(key, AddToCache);

                Assert.NotNull(actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string_FuncICacheCollectionT()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = default(DateTime);
                obj[key] = expected;

                var actual = obj.Get<DateTime>(key, AddToCacheOfT<DateTime>);

                Assert.Equal(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string_FuncICacheCollectionTNull()
        {
            var obj = new WebCacheCollection();

            Assert.Throws<ArgumentNullException>(() => obj.Get(AlphaDecimal.Random(), null as Func<ICacheCollection, DateTime>));
        }

        [Fact]
        public void op_Get_string_FuncICacheCollectionT_whenMissing()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var actual = obj.Get<DateTime>(key, AddToCacheOfT<DateTime>);

                Assert.NotNull(actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string_FuncObject()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = new object();
                obj[key] = expected;

                var actual = obj.Get(key, AddObject);

                Assert.Equal(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string_FuncObjectNull()
        {
            var obj = new WebCacheCollection();

            Assert.Throws<ArgumentNullException>(() => obj.Get(AlphaDecimal.Random(), null as Func<object>));
        }

        [Fact]
        public void op_Get_string_FuncObject_whenMissing()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var actual = obj.Get(key, AddObject);

                Assert.NotNull(actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string_FuncT()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = default(DateTime);
                obj[key] = expected;

                var actual = obj.Get<DateTime>(key, AddOfT<DateTime>);

                Assert.Equal(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Get_string_FuncTNull()
        {
            var obj = new WebCacheCollection();

            Assert.Throws<ArgumentNullException>(() => obj.Get(AlphaDecimal.Random(), null as Func<DateTime>));
        }

        [Fact]
        public void op_Get_string_FuncT_whenMissing()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = default(DateTime);
                var actual = obj.Get<DateTime>(key, AddOfT<DateTime>);

                Assert.Equal(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_RemoveOfT_string()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                const int expected = 123;
                obj[key] = expected;
                var actual = obj.Remove<int>(key);

                Assert.False(obj.ContainsKey(key));
                Assert.Equal(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Remove_string()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                var expected = new object();
                obj[key] = expected;
                var actual = obj.Remove(key);

                Assert.False(obj.ContainsKey(key));
                Assert.Same(expected, actual);
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Set_string_object()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                obj.Set(key, new object());

                Assert.True(obj.ContainsKey(key));
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Set_string_object_DateTime()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                obj.Set(key, new object(), DateTime.UtcNow.AddDays(1));

                Assert.True(obj.ContainsKey(key));
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void op_Set_string_object_TimeSpan()
        {
            var obj = new WebCacheCollection();
            try
            {
                var key = AlphaDecimal.Random();
                obj.Set(key, new object(), new TimeSpan(123456));

                Assert.True(obj.ContainsKey(key));
            }
            finally
            {
                obj.Clear();
            }
        }

        [Fact]
        public void prop_Count()
        {
            Assert.True(new PropertyExpectations<WebCacheCollection>(x => x.Count)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Count_get()
        {
            var obj = new WebCacheCollection();
            try
            {
                Assert.Equal(0, obj.Count);
                obj.Add(AlphaDecimal.Random(), new object());
                Assert.Equal(1, obj.Count);
            }
            finally
            {
                obj.Clear();
            }
        }

        private static object AddObject()
        {
            return new object();
        }

        private static T AddOfT<T>()
        {
            return default(T);
        }

        private static object AddToCache(ICacheCollection cache)
        {
            var obj = new object();
            cache.Add(AlphaDecimal.Random(), obj);

            return obj;
        }

        private static T AddToCacheOfT<T>(ICacheCollection cache)
        {
            var obj = default(T);
            cache.Add(AlphaDecimal.Random(), obj);

            return obj;
        }
    }
}