namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Cavity.IO;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonDocumentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonDocument>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IEnumerable<JsonObject>>()
                            .Implements<IJsonSerializable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonDocument());
        }

        [Fact]
        public void opIndexer_int()
        {
            var expected = new JsonObject();

            var document = new JsonDocument
                               {
                                   expected
                               };

            var actual = document[0];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opIndexer_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonDocument()[-1]);
        }

        [Fact]
        public void opIndexer_int_whenEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonDocument()[0]);
        }

        [Fact]
        public void op_Add_JsonObject()
        {
            var expected = new JsonObject();

            var document = new JsonDocument
                               {
                                   expected
                               };

            var actual = document.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_JsonObjectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonDocument().Add(null));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var expected = new JsonObject();

            IEnumerable document = new JsonDocument
                                       {
                                           expected
                                       };

            foreach (var actual in document)
            {
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Load_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => JsonDocument.Load(null));
        }

        [Theory]
        [InlineData("[{\"i\": 0},{\"i\": 1}]")]
        public void op_Load_string_whenArray(string json)
        {
            var i = 0;
            foreach (var obj in JsonDocument.Load(json))
            {
                Assert.Equal(i++, obj.Number("i").ToInt32());
            }
        }

        [Theory]
        [InlineData("{\"Name\" : \"value\", \"Number\" : 123}")]
        public void op_Load_string_whenSingleObject(string json)
        {
            var document = JsonDocument.Load(json);

            Assert.Equal("value", document[0].String("Name").Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(" [] ")]
        public void op_Load_string_whenZeroObjects(string json)
        {
            Assert.Empty(JsonDocument.Load(json));
        }

        [Theory]
        [InlineData("{\"name\":\"value\"}")]
        public void op_ReadJson_JsonReader(string json)
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
                        var document = new JsonDocument();
                        document.ReadJson(reader);

                        Assert.Equal("value", document.First().String("name").Value);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadJson_JsonReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonDocument().ReadJson(null));
        }

        [Theory]
        [InlineData("[{\"one\":1},{\"two\":2}]")]
        public void op_ReadJson_JsonReader_whenArray(string json)
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
                        var document = new JsonDocument();
                        document.ReadJson(reader);

                        Assert.Equal("1", document.First().Number("one").Value);
                        Assert.Equal("2", document.Last().Number("two").Value);
                    }
                }
            }
        }

        [Theory]
        [InlineData("")]
        public void op_ReadJson_JsonReader_whenEmpty(string json)
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
                        reader.Read();

                        var document = new JsonDocument();
                        document.ReadJson(reader);

                        Assert.Empty(document);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{}")]
        public void op_ReadJson_JsonReader_whenEmptyObject(string json)
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
                        var document = new JsonDocument();
                        document.ReadJson(reader);

                        Assert.Equal(1, document.Count);
                    }
                }
            }
        }

        [Theory]
        [InlineData("[{\"one\":1},{\"two\":2}]")]
        public void op_ToString(string expected)
        {
            var document = new JsonDocument
                               {
                                   new JsonObject
                                       {
                                           new JsonPair("one", new JsonNumber("1"))
                                       }, 
                                   new JsonObject
                                       {
                                           new JsonPair("two", new JsonNumber("2"))
                                       }
                               };

            var actual = document.ToString();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("{\"id\":123}")]
        public void op_WriteJson_JsonWriter(string expected)
        {
            var document = new JsonDocument
                               {
                                   new JsonObject
                                       {
                                           new JsonPair("id", new JsonNumber("123"))
                                       }
                               };

            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    document.WriteJson(writer);
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_WriteJson_JsonWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonDocument().WriteJson(null));
        }

        [Theory]
        [InlineData("[{\"one\":1},{\"two\":2}]")]
        public void op_WriteJson_JsonWriter_whenArray(string expected)
        {
            var document = new JsonDocument
                               {
                                   new JsonObject
                                       {
                                           new JsonPair("one", new JsonNumber("1"))
                                       }, 
                                   new JsonObject
                                       {
                                           new JsonPair("two", new JsonNumber("2"))
                                       }
                               };

            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    document.WriteJson(writer);
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
        public void op_WriteJson_JsonWriter_whenEmpty(string expected)
        {
            var document = new JsonDocument();

            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    document.WriteJson(writer);
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
        public void op_WriteJson_JsonWriter_whenEmptyObject(string expected)
        {
            var document = new JsonDocument
                               {
                                   new JsonObject()
                               };

            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    document.WriteJson(writer);
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void prop_Count()
        {
            Assert.True(new PropertyExpectations<JsonDocument>(x => x.Count)
                            .IsNotDecorated()
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .Result);
        }

        [Fact]
        public void prop_Count_get()
        {
            var document = new JsonDocument();
            Assert.Equal(0, document.Count);

            document.Add(new JsonObject());

            Assert.Equal(1, document.Count);
        }

        [Theory]
        [InlineData("facebook.json", "facebook.pretty.json")]
        [InlineData("flickr.json", "flickr.pretty.json")]
        [InlineData("yahoo pipes.json", "yahoo pipes.pretty.json")]
        public void roundtrip_pretty(string source, 
                                     string destination)
        {
            var expected = new FileInfo(destination).ReadToEnd();

            var document = JsonDocument.Load(new FileInfo(source).ReadToEnd());

            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream, JsonWriterSettings.Pretty))
                {
                    document.WriteJson(writer);
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    ////new FileInfo("{0}.txt".FormatWith(source)).Append(actual);
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Theory]
        [InlineData("youtube.json")]
        [InlineData("google developer calendar.json")]
        public void roundtrip_terse(string json)
        {
            var expected = new FileInfo(json).ReadToEnd();

            var document = JsonDocument.Load(expected);

            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    document.WriteJson(writer);
                }

                using (var reader = new StreamReader(stream))
                {
                    var actual = reader.ReadToEnd();

                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}