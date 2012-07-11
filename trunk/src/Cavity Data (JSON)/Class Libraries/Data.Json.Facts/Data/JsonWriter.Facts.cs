namespace Cavity.Data
{
    using System;
    using System.IO;
    using System.Xml;

    using Cavity.IO;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonWriterFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonWriter>()
                            .DerivesFrom<DisposableObject>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_Stream()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    Assert.NotNull(writer);
                }
            }
        }

        [Fact]
        public void ctor_StreamNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonWriter(null));
        }

        [Fact]
        public void ctor_Stream_JsonWriterSettings()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream, new JsonWriterSettings()))
                {
                    Assert.NotNull(writer);
                }
            }
        }

        [Fact]
        public void ctor_Stream_JsonWriterSettingsNull()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream, null))
                {
                    Assert.NotNull(writer);
                }
            }
        }

        [Fact]
        public void ctor_StreamNull_JsonWriterSettings()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonWriter(null, new JsonWriterSettings()));
        }

        [Fact]
        public void op_ArrayNull_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayNull());

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData(null, "{\"example\":[null]}")]
        [InlineData("123", "{\"example\":[123]}")]
        [InlineData(" 123 ", "{\"example\":[123]}")]
        public void op_ArrayNumber_string(string value, 
                                          string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayNumber(value);
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void op_ArrayNumber_stringEmpty(string value)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<ArgumentOutOfRangeException>(() => writer.ArrayNumber(value));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_ArrayNumber_string_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayNumber("example"));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":[\"2011-07-14T19:43:37Z\"]}")]
        public void op_ArrayValue_DateTime(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayValue(new DateTime(2011, 7, 14, 19, 43, 37, DateTimeKind.Utc));
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_ArrayValue_DateTime_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayValue(DateTime.UtcNow));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":[\"PT17H9M43.089S\"]}")]
        public void op_ArrayValue_TimeSpan(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayValue(new TimeSpan(0, 17, 9, 43, 89));
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_ArrayValue_TimeSpan_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayValue(TimeSpan.Zero));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData(true, "{\"example\":[true]}")]
        [InlineData(false, "{\"example\":[false]}")]
        public void op_ArrayValue_bool(bool value, 
                                       string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayValue(value);
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_ArrayValue_bool_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayValue(true));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":[1.0]}")]
        public void op_ArrayValue_decimal(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayValue(1.0m);
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_ArrayValue_decimal_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayValue(decimal.One));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":[1.23]}")]
        public void op_ArrayValue_double(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayValue(1.23d);
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_ArrayValue_double_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayValue(double.Epsilon));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":[12345]}")]
        public void op_ArrayValue_long(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayValue(12345L);
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_ArrayValue_long_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayValue(long.MinValue));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData(null, "{\"example\":[null]}")]
        [InlineData("", "{\"example\":[\"\"]}")]
        [InlineData(" ", "{\"example\":[\" \"]}")]
        [InlineData("value", "{\"example\":[\"value\"]}")]
        public void op_ArrayValue_string(string value, 
                                         string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayValue(value);
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_ArrayValue_string_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.ArrayValue(string.Empty));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_Array_stringNull()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<ArgumentNullException>(() => writer.Array(null));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_Array_string_whenRoot()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.Array("example"));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_Array_whenNotRoot()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.Array());

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_EndArray_whenParentNone()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.EndArray());

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_EndObject_whenParentNone()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.EndObject());

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":null}")]
        public void op_NullPair_string(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.NullPair("example");
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_NullPair_stringNull()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<ArgumentNullException>(() => writer.NullPair(null));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_NullPair_string_whenArrayParent()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.NullPair("name"));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_NumberPair_stringNull_string()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<ArgumentNullException>(() => writer.NumberPair(null, "123"));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData(null, "{\"example\":null}")]
        [InlineData(" 123 ", "{\"example\":123}")]
        [InlineData("123", "{\"example\":123}")]
        [InlineData("1.23", "{\"example\":1.23}")]
        [InlineData("1e3", "{\"example\":1e3}")]
        public void op_NumberPair_string_string(string value, 
                                                string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.NumberPair("example", value);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void op_NumberPair_string_stringEmpty(string value)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<ArgumentOutOfRangeException>(() => writer.NumberPair("example", value));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_NumberPair_string_stringNull_whenArrayParent()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.NumberPair("name", null));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_NumberPair_string_string_whenArrayParent()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.NumberPair("name", "1.23"));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("{}", 1)]
        public void op_Object(string expected, 
                              int count)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    for (var i = 0; i < count; i++)
                    {
                        writer.Object();
                        writer.EndObject();
                    }
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_Object_whenAfterPair()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("one", 1);

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.Object());

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_Object_whenParentNotArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.EndObject();

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.Object());

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_Pair_stringNull_bool()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<ArgumentNullException>(() => writer.Pair(null, true));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_Pair_stringNull_string()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<ArgumentNullException>(() => writer.Pair(null, "value"));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":\"2011-07-14T19:43:37Z\"}")]
        public void op_Pair_string_DateTime(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", new DateTime(2011, 7, 14, 19, 43, 37, DateTimeKind.Utc));
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":\"PT17H9M43.089S\"}")]
        public void op_Pair_string_TimeSpan(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", new TimeSpan(0, 17, 9, 43, 89));
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData(true, "{\"example\":true}")]
        [InlineData(false, "{\"example\":false}")]
        public void op_Pair_string_bool(bool value, 
                                        string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", value);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_Pair_string_bool_whenArrayParent()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.Pair("name", true));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("0", "{\"example\":0}")]
        [InlineData("0.0", "{\"example\":0.0}")]
        [InlineData("1.2345", "{\"example\":1.2345}")]
        public void op_Pair_string_decimal(string value, 
                                           string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", XmlConvert.ToDecimal(value));
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData(0, "{\"example\":0}")]
        [InlineData(1.2345, "{\"example\":1.2345}")]
        [InlineData(1230000000000000000, "{\"example\":1.23E+18}")]
        [InlineData(0.00000000000000123, "{\"example\":1.23E-15}")]
        public void op_Pair_string_double(double value, 
                                          string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", value);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData(12345, "{\"example\":12345}")]
        public void op_Pair_string_int(int value, 
                                       string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", value);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData(12345, "{\"example\":12345}")]
        public void op_Pair_string_long(long value, 
                                        string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", value);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData(0f, "{\"example\":0}")]
        [InlineData(1.2f, "{\"example\":1.2000000476837158}")]
        [InlineData(123000000f, "{\"example\":123000000}")]
        public void op_Pair_string_single(float value, 
                                          string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", value);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData(null, "{\"example\":null}")]
        [InlineData("", "{\"example\":\"\"}")]
        [InlineData(" ", "{\"example\":\" \"}")]
        [InlineData("value", "{\"example\":\"value\"}")]
        [InlineData(" value ", "{\"example\":\" value \"}")]
        public void op_StringPair_string_string(string value, 
                                                string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("example", value);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_StringPair_string_stringNull_whenArrayParent()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.Pair("name", null));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Fact]
        public void op_StringPair_string_string_whenArrayParent()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");

                    // ReSharper disable AccessToDisposedClosure
                    Assert.Throws<InvalidOperationException>(() => writer.Pair("name", "value"));

                    // ReSharper restore AccessToDisposedClosure
                }
            }
        }

        [Theory]
        [InlineData("[{},{}]")]
        public void write_array_with_two_empty_objects(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Array();
                    writer.Object();
                    writer.EndObject();
                    writer.Object();
                    writer.EndObject();
                    writer.EndArray();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("[]")]
        public void write_empty_array(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Array();
                    writer.EndArray();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("{}")]
        public void write_empty_object(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":[null,1,2,3,4,\" \",true,{\"one\":1}]}")]
        public void write_object_with_array(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.ArrayNull();
                    writer.ArrayNumber("1");
                    writer.ArrayValue(2L);
                    writer.ArrayValue(3m);
                    writer.ArrayValue(4d);
                    writer.ArrayValue(" ");
                    writer.ArrayValue(true);
                    writer.Object();
                    writer.Pair("one", 1);
                    writer.EndObject();
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("{\"example\":[{\"one\":1},{\"two\":2}]}")]
        public void write_object_with_array_of_objects(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("example");
                    writer.Object();
                    writer.Pair("one", 1);
                    writer.EndObject();
                    writer.Object();
                    writer.Pair("two", 2);
                    writer.EndObject();
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("{\"list\":[true,[1,2,3],false]}")]
        public void write_object_with_nested_arrays(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Array("list");
                    writer.ArrayValue(true);
                    writer.Array();
                    writer.ArrayValue(1);
                    writer.ArrayValue(2);
                    writer.ArrayValue(3);
                    writer.EndArray();
                    writer.ArrayValue(false);
                    writer.EndArray();
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("{\"id\":123,\"title\":\"\",\"value\":null,\"list\":[1,\"\",null,true,false],\"visible\":true,\"enabled\":false}")]
        public void write_object_with_pairs(string expected)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    writer.Pair("id", 123);
                    writer.Pair("title", string.Empty);
                    writer.Pair("value", null);
                    writer.Array("list");
                    writer.ArrayValue(1);
                    writer.ArrayValue(string.Empty);
                    writer.ArrayNull();
                    writer.ArrayValue(true);
                    writer.ArrayValue(false);
                    writer.EndArray();
                    writer.Pair("visible", true);
                    writer.Pair("enabled", false);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("pretty array.json")]
        public void write_pretty_array(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream, JsonWriterSettings.Debug))
                {
                    writer.Array();
                    writer.Object();
                    writer.Pair("id", 0);
                    writer.EndObject();
                    writer.Object();
                    writer.Pair("id", 123);
                    writer.Pair("title", string.Empty);
                    writer.Pair("value", null);
                    writer.Array("list");
                    writer.ArrayValue(1);
                    writer.ArrayValue(string.Empty);
                    writer.ArrayNull();
                    writer.ArrayValue(true);
                    writer.ArrayValue(false);
                    writer.EndArray();
                    writer.Pair("visible", true);
                    writer.Pair("enabled", false);
                    writer.EndObject();
                    writer.EndArray();
                }

                using (var reader = new StreamReader(stream))
                {
                    var expected = new FileInfo(json).ReadToEnd();
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("pretty object.json")]
        public void write_pretty_object(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream, JsonWriterSettings.Debug))
                {
                    writer.Object();
                    writer.Pair("id", 123);
                    writer.Pair("title", string.Empty);
                    writer.Pair("value", null);
                    writer.Array("list");
                    writer.ArrayValue(1);
                    writer.ArrayValue(string.Empty);
                    writer.ArrayNull();
                    writer.ArrayValue(true);
                    writer.ArrayValue(false);
                    writer.EndArray();
                    writer.Pair("visible", true);
                    writer.Pair("enabled", false);
                    writer.EndObject();
                }

                using (var reader = new StreamReader(stream))
                {
                    var expected = new FileInfo(json).ReadToEnd();
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}