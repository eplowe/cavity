namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Linq;
    using Xunit;

    public sealed class MultitonCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MultitonCollection<string, string>>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Implements<IEnumerable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new MultitonCollection<long, string>());
        }

        [Fact]
        public void indexer_TKey_get()
        {
            const string expected = "example";

            var obj = new MultitonCollection<int, string>
            {
                {
                    123, expected
                    }
            };

            var actual = obj[123];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void indexer_TKey_getAdd()
        {
            var expected = DateTime.MinValue;

            var obj = new MultitonCollection<int, DateTime>();

            var actual = obj[123];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void indexer_TKey_set()
        {
            const string expected = "example";

            var obj = new MultitonCollection<int, string>
            {
                {
                    123, string.Empty
                    }
            };

            obj[123] = expected;

            var actual = obj[123];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void indexer_TKey_setAdd()
        {
            const string expected = "example";

            var obj = new MultitonCollection<int, string>();

            obj[123] = expected;

            var actual = obj[123];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_TKey_TValue()
        {
            var obj = new MultitonCollection<int, DateTime>();

            Assert.Equal(0, obj.Count());

            obj.Add(123, DateTime.Today);

            Assert.Equal(1, obj.Count());
        }

        [Fact]
        public void op_ContainsKey_TKey()
        {
            var obj = new MultitonCollection<int, DateTime>
            {
                {
                    123, DateTime.Today
                    }
            };

            Assert.True(obj.ContainsKey(123));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            IEnumerable obj = new MultitonCollection<int, DateTime>();
            Assert.NotNull(obj.GetEnumerator());

            Assert.NotNull(new MultitonCollection<int, DateTime>().GetEnumerator());
        }

        [Fact]
        public void op_GetValue_TKey()
        {
            var obj = new MultitonCollection<int, string>();

            const string expected = "example";

            obj.Add(123, expected);

            Assert.Equal(expected, obj.GetValue(123));
        }
    }
}