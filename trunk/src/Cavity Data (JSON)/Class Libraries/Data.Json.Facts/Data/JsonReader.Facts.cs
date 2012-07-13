namespace Cavity.Data
{
    using System;
    using System.IO;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonReaderFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonReader>()
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
                using (var reader = new JsonReader(stream))
                {
                    Assert.NotNull(reader);
                }
            }
        }

        [Fact]
        public void ctor_StreamNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonReader(null));
        }

        [Fact]
        public void op_Read_whenEndOfStream()
        {
            using (var stream = new MemoryStream())
            {
                using (var reader = new JsonReader(stream))
                {
                    Assert.Equal(JsonNodeType.None, reader.NodeType);
                    Assert.False(reader.Read());
                }
            }
        }

        [Theory]
        [InlineData("[]")]
        [InlineData("[ ]")]
        [InlineData("[\r]")]
        [InlineData("[\r\n]")]
        [InlineData(" [] ")]
        [InlineData(" [ ] ")]
        [InlineData(" [\r] ")]
        [InlineData(" [\r\n] ")]
        public void op_Read_whenObjectArrayEmpty(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.True(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{}")]
        [InlineData("{ }")]
        [InlineData("{\r}")]
        [InlineData("{\r\n}")]
        [InlineData(" {} ")]
        [InlineData(" { } ")]
        [InlineData(" {\r} ")]
        [InlineData(" {\r\n} ")]
        public void op_Read_whenObjectEmpty(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.True(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : \"value\", \"Number\" : 123}")]
        [InlineData(" { \"Name\" : \"value\", \"Number\" : 123 } ")]
        [InlineData("\r {\r \"Name\" : \"value\",\r \"Number\" : 123\r }\r ")]
        [InlineData("\r\n {\r\n \"Name\" : \"value\",\r\n \"Number\" : 123\r\n }\r\n ")]
        public void op_Read_whenProperties(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Equal("value", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Equal("123", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Values\" : []}")]
        public void op_Read_whenPropertyArrayEmpty(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Values", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("Values", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.True(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Values\" : [\"abc\", 123, null, true, false]}")]
        public void op_Read_whenPropertyArrayMixed(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Values", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("Values", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal("abc", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal("123", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NullValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.TrueValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal("true", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.FalseValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal("false", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("123,456,789", "{\"Numbers\" : [123,456,789]}")]
        [InlineData("123,456,789", "{\"Numbers\" : [ 123,456,789 ]}")]
        [InlineData("123,456,789", "{\"Numbers\" : [ 123, 456, 789 ]}")]
        [InlineData("123,456,789", "{\"Numbers\" : [ 123 , 456 , 789 ]}")]
        public void op_Read_whenPropertyArrayNumber(string values, 
                                                    string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Numbers", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("Numbers", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        foreach (var value in values.Split(','))
                        {
                            Assert.True(reader.Read());
                            Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                            Assert.Null(reader.Name);
                            Assert.Equal(value, reader.Value);
                            Assert.False(reader.IsEmptyArray);
                            Assert.False(reader.IsEmptyObject);
                        }

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : [{\"Number\" : 123}, {\"Number\" : 456}]}")]
        public void op_Read_whenPropertyArrayObject(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Equal("123", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Equal("456", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Outer\" : [{\"Inner\" : {\"Number\" : 123}}]}")]
        public void op_Read_whenPropertyArrayObjectNested(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Outer", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("Outer", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.Equal("Outer", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Inner", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.Equal("Inner", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Equal("123", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : [{\"Number\" : 123}]}")]
        public void op_Read_whenPropertyArrayObjectSingle(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Equal("123", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("a,b,c", "{\"Letters\" : [\"a\",\"b\",\"c\"]}")]
        [InlineData("a,b,c", "{\"Letters\" : [ \"a\",\"b\",\"c\" ]}")]
        [InlineData("a,b,c", "{\"Letters\" : [ \"a\", \"b\", \"c\" ]}")]
        [InlineData("a,b,c", "{\"Letters\" : [ \"a\" , \"b\" , \"c\" ]}")]
        public void op_Read_whenPropertyArrayString(string values, 
                                                    string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Letters", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("Letters", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        foreach (var value in values.Split(','))
                        {
                            Assert.True(reader.Read());
                            Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                            Assert.Null(reader.Name);
                            Assert.Equal(value, reader.Value);
                            Assert.False(reader.IsEmptyArray);
                            Assert.False(reader.IsEmptyObject);
                        }

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("", "{\"\"}")]
        [InlineData(" ", "{\" \"}")]
        [InlineData("\"", "{\"\\\"\"}")]
        [InlineData("\\", "{\"\\\\\"}")]
        [InlineData("/", "{\"\\/\"}")]
        [InlineData("\b", "{\"\\b\"}")]
        [InlineData("\f", "{\"\\f\"}")]
        [InlineData("\n", "{\"\\n\"}")]
        [InlineData("\r", "{\"\\r\"}")]
        [InlineData("\t", "{\"\\t\"}")]
        [InlineData("\u0066", "{\"\\u0066\"}")]
        [InlineData("Example", "{\"Example\"}")]
        [InlineData("Foo Bar", "{\"Foo Bar\"}")]
        public void op_Read_whenPropertyNameOnly(string name, 
                                                 string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal(name, reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : false}")]
        public void op_Read_whenPropertyValueFalse(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.FalseValue, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Equal("false", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : null}")]
        public void op_Read_whenPropertyValueNull(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NullValue, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("123", "{\"Name\" : 123}")]
        [InlineData("1.23", "{\"Name\" : 1.23}")]
        [InlineData("1e3", "{\"Name\" : 1e3}")]
        [InlineData("1E3", "{\"Name\" : 1E3}")]
        [InlineData("1e+3", "{\"Name\" : 1e+3}")]
        [InlineData("1E+3", "{\"Name\" : 1E+3}")]
        [InlineData("1e-3", "{\"Name\" : 1e-3}")]
        [InlineData("1E-3", "{\"Name\" : 1E-3}")]
        public void op_Read_whenPropertyValueNumber(string value, 
                                                    string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Equal(value, reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : {\"Number\" : 123}}")]
        public void op_Read_whenPropertyValueObject(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("Number", reader.Name);
                        Assert.Equal("123", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("value", "{\"Name\" : \"value\"}")]
        [InlineData("left right", "{\"Name\" : \"left right\"}")]
        public void op_Read_whenPropertyValueString(string value, 
                                                    string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Equal(value, reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : true}")]
        public void op_Read_whenPropertyValueTrue(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.TrueValue, reader.NodeType);
                        Assert.Equal("Name", reader.Name);
                        Assert.Equal("true", reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Fact]
        public void op_Read_whenRfc4627example1()
        {
            var file = new FileInfo("rfc4627 example 1.json");
            using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new JsonReader(stream))
                {
                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Object, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Image", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Object, reader.NodeType);
                    Assert.Equal("Image", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Width", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                    Assert.Equal("Width", reader.Name);
                    Assert.Equal("800", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Height", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                    Assert.Equal("Height", reader.Name);
                    Assert.Equal("600", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Title", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Title", reader.Name);
                    Assert.Equal("View from 15th Floor", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Thumbnail", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Object, reader.NodeType);
                    Assert.Equal("Thumbnail", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Url", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Url", reader.Name);
                    Assert.Equal("http://www.example.com/image/481989943", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Height", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                    Assert.Equal("Height", reader.Name);
                    Assert.Equal("125", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Width", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Width", reader.Name);
                    Assert.Equal("100", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.EndObject, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("IDs", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Array, reader.NodeType);
                    Assert.Equal("IDs", reader.Name);

                    foreach (var value in "116,943,234,38793".Split(','))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal(value, reader.Value);
                    }

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.EndArray, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.EndObject, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                }
            }
        }

        [Fact]
        public void op_Read_whenRfc4627example2()
        {
            var file = new FileInfo("rfc4627 example 2.json");
            using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new JsonReader(stream))
                {
                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Array, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Object, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("precision", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("precision", reader.Name);
                    Assert.Equal("zip", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Latitude", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                    Assert.Equal("Latitude", reader.Name);
                    Assert.Equal("37.7668", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Longitude", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                    Assert.Equal("Longitude", reader.Name);
                    Assert.Equal("-122.3959", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Address", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Address", reader.Name);
                    Assert.Equal(string.Empty, reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("City", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("City", reader.Name);
                    Assert.Equal("SAN FRANCISCO", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("State", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("State", reader.Name);
                    Assert.Equal("CA", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Zip", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Zip", reader.Name);
                    Assert.Equal("94107", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Country", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Country", reader.Name);
                    Assert.Equal("US", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.EndObject, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Object, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("precision", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("precision", reader.Name);
                    Assert.Equal("zip", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Latitude", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                    Assert.Equal("Latitude", reader.Name);
                    Assert.Equal("37.371991", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Longitude", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                    Assert.Equal("Longitude", reader.Name);
                    Assert.Equal("-122.026020", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Address", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Address", reader.Name);
                    Assert.Equal(string.Empty, reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("City", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("City", reader.Name);
                    Assert.Equal("SUNNYVALE", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("State", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("State", reader.Name);
                    Assert.Equal("CA", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Zip", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Zip", reader.Name);
                    Assert.Equal("94085", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.Name, reader.NodeType);
                    Assert.Equal("Country", reader.Name);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.StringValue, reader.NodeType);
                    Assert.Equal("Country", reader.Name);
                    Assert.Equal("US", reader.Value);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.EndObject, reader.NodeType);

                    Assert.True(reader.Read());
                    Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                }
            }
        }

        [Fact]
        public void prop_IsEmptyArray()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.IsEmptyArray)
                            .IsNotDecorated()
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .Result);
        }

        [Fact]
        public void prop_IsEmptyObject()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.IsEmptyObject)
                            .IsNotDecorated()
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .Result);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.Name)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .DefaultValueIsNull()
                            .Result);
        }

        [Fact]
        public void prop_NodeType()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.NodeType)
                            .IsNotDecorated()
                            .TypeIs<JsonNodeType>()
                            .DefaultValueIs(JsonNodeType.None)
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.Value)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .DefaultValueIsNull()
                            .Result);
        }

        [Theory]
        [InlineData("{\"list\": [true,, false]}")]
        [InlineData("{\"list\": [true, , false]}")]
        public void read_double_comma(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("list", reader.Name);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("list", reader.Name);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.TrueValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal("true", reader.Value);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.FalseValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal("false", reader.Value);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"list\": [true, [1, 2, 3], false]}")]
        public void read_nested_arrays(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Object, reader.NodeType);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Name, reader.NodeType);
                        Assert.Equal("list", reader.Name);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);
                        Assert.Equal("list", reader.Name);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.TrueValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal("true", reader.Value);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.Array, reader.NodeType);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("1", reader.Value);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("2", reader.Value);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.NumberValue, reader.NodeType);
                        Assert.Equal("3", reader.Value);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.FalseValue, reader.NodeType);
                        Assert.Null(reader.Name);
                        Assert.Equal("false", reader.Value);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                    }
                }
            }
        }
    }
}