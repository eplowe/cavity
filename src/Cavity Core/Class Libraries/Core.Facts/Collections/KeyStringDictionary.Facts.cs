namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml;

    using Cavity.Data;

    using Xunit;
    using Xunit.Extensions;

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
            const string expected = "value";

            var data = new KeyStringDictionary
                           {
                               { "NAME", expected }
                           };

            var actual = data["name"];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_IEqualityComparerOfString()
        {
            var data = new KeyStringDictionary(StringComparer.Ordinal)
                           {
                               { "NAME", "value" }
                           };

            Assert.Throws<KeyNotFoundException>(() => data["name"]);
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new KeyStringDictionary
                               {
                                   new KeyStringPair("key", "value")
                               };
            KeyStringDictionary actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                var obj = new KeyStringDictionary
                              {
                                  new KeyStringPair("key", "value")
                              };
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                actual = (KeyStringDictionary)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
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
        public void op_Contains_KeyStringPair()
        {
            var item = new KeyStringPair("key", "value");
            var obj = new KeyStringDictionary
                          {
                              item
                          };

            Assert.True(obj.Contains(item));
        }

        [Theory]
        [InlineData(false, "")]
        [InlineData(false, "A")]
        [InlineData(false, "A,B")]
        [InlineData(false, "A,C")]
        [InlineData(true, "B")]
        [InlineData(true, "B,D")]
        public void op_Empty_strings(bool expected, 
                                     string keys)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "1"), 
                              new KeyStringPair("B", string.Empty), 
                              new KeyStringPair("C", "3"), 
                              new KeyStringPair("D", string.Empty)
                          };

            var actual = obj.Empty(keys.Split(',', StringSplitOptions.RemoveEmptyEntries));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, "value")]
        [InlineData(true, "")]
        public void op_Empty_stringsEmpty(bool expected, 
                                          string value)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", value)
                          };

            var actual = obj.Empty();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Empty_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new KeyStringDictionary().Empty(null));
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

        [Theory]
        [InlineData(2, "")]
        [InlineData(1, "A")]
        [InlineData(1, "A,B")]
        [InlineData(2, "A,B,C")]
        [InlineData(2, "A,B,C,D")]
        public void op_Length_strings(int expected, 
                                      string keys)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "1"), 
                              new KeyStringPair("B", string.Empty), 
                              new KeyStringPair("C", "3"), 
                              new KeyStringPair("D", string.Empty)
                          };

            var actual = obj.Length(keys.Split(',', StringSplitOptions.RemoveEmptyEntries));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(5, "value")]
        public void op_Length_stringsEmpty(int expected, 
                                           string value)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", value)
                          };

            var actual = obj.Length();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Length_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new KeyStringDictionary().Length(null));
        }

        [Fact]
        public void op_Move_string_string()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "1"), 
                              new KeyStringPair("B", string.Empty), 
                              new KeyStringPair("C", "3")
                          };

            obj.Move("C", "B");

            const string expected = "1,3,";
            var actual = obj.Strings().Concat(',');

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, "")]
        [InlineData(true, "A")]
        [InlineData(true, "A,B")]
        [InlineData(true, "A,C")]
        [InlineData(false, "B")]
        [InlineData(false, "B,D")]
        public void op_NotEmpty_strings(bool expected, 
                                        string keys)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "1"), 
                              new KeyStringPair("B", string.Empty), 
                              new KeyStringPair("C", "3"), 
                              new KeyStringPair("D", string.Empty)
                          };

            var actual = obj.NotEmpty(keys.Split(',', StringSplitOptions.RemoveEmptyEntries));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, "value")]
        [InlineData(false, "")]
        public void op_NotEmpty_stringsEmpty(bool expected, 
                                             string value)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", value)
                          };

            var actual = obj.NotEmpty();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_NotEmpty_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new KeyStringDictionary().NotEmpty(null));
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
        public void op_Strings_strings()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "1"), 
                              new KeyStringPair("B", "2"), 
                              new KeyStringPair("C", "3")
                          };

            const string expected = "1,2,3";
            var actual = obj.Strings("A", "B", "C").Concat(",");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Strings_stringsEmpty()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "1"), 
                              new KeyStringPair("B", "2"), 
                              new KeyStringPair("C", "3")
                          };

            const string expected = "1,2,3";
            var actual = obj.Strings().Concat(",");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Strings_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new KeyStringDictionary().Strings(null).IsEmpty());
        }

        [Fact]
        public void op_TryAdd_KeyStringPair_whenFalse()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("key", "value")
                          };

            Assert.False(obj.TryAdd(new KeyStringPair("key", "value")));
            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_TryAdd_KeyStringPair_whenTrue()
        {
            var obj = new KeyStringDictionary();

            Assert.True(obj.TryAdd(new KeyStringPair("key", "value")));
            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_TryAdd_string_string_whenFalse()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("key", "value")
                          };

            Assert.False(obj.TryAdd("key", "value"));
            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_TryAdd_string_string_whenTrue()
        {
            var obj = new KeyStringDictionary();

            Assert.True(obj.TryAdd("key", "value"));
            Assert.Equal(1, obj.Count);
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

        [Theory]
        [InlineData(123, "123", -1)]
        [InlineData(-1, "", -1)]
        public void op_TryValueOfInt32_int_int(int expected, 
                                               string value, 
                                               int empty)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("key", value)
                          };

            var actual = obj.TryValue(0, empty);

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

        [Theory]
        [InlineData("example", "example", "empty")]
        [InlineData("empty", "", "empty")]
        public void op_TryValueOfString_string_string(string expected, 
                                                      string value, 
                                                      string empty)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("key", expected)
                          };

            var actual = obj.TryValue("key", empty);

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

        [Theory]
        [InlineData(123, "123", -1)]
        [InlineData(-1, "", -1)]
        public void op_ValueOfInt32_int_int(int expected, 
                                            string value, 
                                            int empty)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("key", value)
                          };

            var actual = obj.Value(0, empty);

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

        [Theory]
        [InlineData("example", "example", "empty")]
        [InlineData("empty", "", "empty")]
        public void op_ValueOfString_string_string(string expected, 
                                                   string value, 
                                                   string empty)
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("key", expected)
                          };

            var actual = obj.Value("key", empty);

            Assert.Equal(expected, actual);
        }
    }
}