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

        [Theory]
        [InlineData("[]")]
        [InlineData("[ ]")]
        [InlineData("[\r]")]
        [InlineData("[\r\n]")]
        [InlineData(" [] ")]
        [InlineData(" [ ] ")]
        [InlineData(" [\r] ")]
        [InlineData(" [\r\n] ")]
        public void op_Read_whenArrayEmpty(string json)
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
                        Assert.True(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.True(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
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
        [InlineData("{\"Name\" : false}")]
        public void op_Read_whenFalseValue(string json)
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

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
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
        public void op_Read_whenMultipleProperties(string json)
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

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
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
        public void op_Read_whenNameOnly(string name, 
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

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : null}")]
        public void op_Read_whenNullValue(string json)
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

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
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
        public void op_Read_whenNumberValue(string value, 
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

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
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
        public void op_Read_whenNumberArray(string values, string json)
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
                            Assert.Equal("Numbers", reader.Name);
                            Assert.Equal(value, reader.Value);
                            Assert.False(reader.IsEmptyArray);
                            Assert.False(reader.IsEmptyObject);
                        }

                        Assert.True(reader.Read());
                        Assert.Equal(JsonNodeType.EndArray, reader.NodeType);
                        Assert.Equal("Numbers", reader.Name);
                        Assert.Null(reader.Value);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
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

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.True(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("value", "{\"Name\" : \"value\"}")]
        [InlineData("left right", "{\"Name\" : \"left right\"}")]
        public void op_Read_whenStringValue(string value, 
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

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{\"Name\" : true}")]
        public void op_Read_whenTrueValue(string json)
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

                        Assert.False(reader.Read());
                        Assert.Equal(JsonNodeType.EndObject, reader.NodeType);
                        Assert.False(reader.IsEmptyArray);
                        Assert.False(reader.IsEmptyObject);
                    }
                }
            }
        }

        [Fact]
        public void prop_IsEmptyArray()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.IsEmptyArray)
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsEmptyObject()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.IsEmptyObject)
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.Name)
                            .TypeIs<string>()
                            .DefaultValueIsNull()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_NodeType()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.NodeType)
                            .TypeIs<JsonNodeType>()
                            .DefaultValueIs(JsonNodeType.None)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<JsonReader>(x => x.Value)
                            .TypeIs<string>()
                            .DefaultValueIsNull()
                            .IsNotDecorated()
                            .Result);
        }
    }
}