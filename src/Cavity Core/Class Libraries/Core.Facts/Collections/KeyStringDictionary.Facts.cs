namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using Cavity.Data;
    using Xunit;

    public sealed class KeyStringDictionaryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<KeyStringDictionary>()
                            .DerivesFrom<Dictionary<string, string>>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new KeyStringDictionary());
        }

        [Fact]
        public void indexer_int()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("zero", "0"),
                new KeyStringPair("one", "1")
            };

            for (var i = 0; i < obj.Count; i++)
            {
                Assert.Equal(XmlConvert.ToString(i), obj[i]);
            }
        }

        [Fact]
        public void indexer_int_whenOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new KeyStringDictionary()[0]);
        }

        [Fact]
        public void op_Add_KeyStringPair()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", "value")
            };

            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Contains_KeyStringPair()
        {
            var item = new KeyStringPair("key", "value");
            var obj = new KeyStringDictionary
            {
                item
            };

            Assert.True(obj.Contains(item));
        }

        [Fact]
        public void op_ContainsKey_string()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", "value")
            };

            Assert.True(obj.ContainsKey("key"));
            Assert.False(obj.ContainsKey("example"));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", "value")
            };

            foreach (var item in obj)
            {
                Assert.IsType<KeyStringPair>(item);
                Assert.Equal("key", item.Key);
                Assert.Equal("value", item.Value);
            }
        }

        [Fact]
        public void op_Remove_KeyStringPair()
        {
            var item = new KeyStringPair("key", "value");
            var obj = new KeyStringDictionary
            {
                item
            };

            Assert.True(obj.Remove(item));
            Assert.Equal(0, obj.Count);
        }

        [Fact]
        public void op_TryValueOfDateTime_int()
        {
            var expected = DateTime.UtcNow;
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", expected.ToXmlString())
            };

            var actual = obj.TryValue<DateTime>(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryValueOfInt32_int()
        {
            const int expected = 123;
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", XmlConvert.ToString(expected))
            };

            var actual = obj.TryValue<int>(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryValueOfString_int()
        {
            const string expected = "value";
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", expected)
            };

            var actual = obj.TryValue<string>(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryValueOfDateTime_string()
        {
            var expected = DateTime.UtcNow;
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", expected.ToXmlString())
            };

            var actual = obj.TryValue<DateTime>("key");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryValueOfInt32_string()
        {
            const int expected = 123;
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", XmlConvert.ToString(expected))
            };

            var actual = obj.TryValue<int>("key");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryValueOfString_string()
        {
            const string expected = "value";
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", expected)
            };

            var actual = obj.TryValue<string>("key");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ValueOfDateTime_int()
        {
            var expected = DateTime.UtcNow;
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", expected.ToXmlString())
            };

            var actual = obj.Value<DateTime>(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ValueOfInt32_int()
        {
            const int expected = 123;
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", XmlConvert.ToString(expected))
            };

            var actual = obj.Value<int>(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ValueOfString_int()
        {
            const string expected = "value";
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", expected)
            };

            var actual = obj.Value<string>(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ValueOfDateTime_string()
        {
            var expected = DateTime.UtcNow;
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", expected.ToXmlString())
            };

            var actual = obj.Value<DateTime>("key");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ValueOfInt32_string()
        {
            const int expected = 123;
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", XmlConvert.ToString(expected))
            };

            var actual = obj.Value<int>("key");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ValueOfString_string()
        {
            const string expected = "value";
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", expected)
            };

            var actual = obj.Value<string>("key");

            Assert.Equal(expected, actual);
        }
    }
}