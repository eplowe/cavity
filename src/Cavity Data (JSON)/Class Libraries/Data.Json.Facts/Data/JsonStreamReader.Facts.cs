namespace Cavity.Data
{
    using System;
    using System.IO;
    using System.Xml;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonStreamReaderFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonStreamReader>()
                            .DerivesFrom<StreamReader>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var stream = new MemoryStream())
            {
                Assert.NotNull(new JsonStreamReader(stream));
            }
        }

        [Fact]
        public void ctor_StreamNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonStreamReader(null));
        }

        [Theory]
        [InlineData(true, "{ \"Name\"=\"true\" }")]
        [InlineData(false, "{ \"Name\"=\"false\" }")]
        public void op_ReadEntry_whenBooleanProperty(bool expected, 
                                                     string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonStreamReader(stream))
                    {
                        var actual = (bool)reader.ReadEntry().Name;

                        Assert.Equal(expected, actual);
                    }
                }
            }
        }

        [Theory]
        [InlineData(".12345", "{ \"Name\"=\".12345\" }")]
        [InlineData("1.2345", "{ \"Name\"=\"1.2345\" }")]
        public void op_ReadEntry_whenDecimalProperty(string expected, 
                                                     string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonStreamReader(stream))
                    {
                        var actual = (decimal)reader.ReadEntry().Name;

                        Assert.Equal(XmlConvert.ToDecimal(expected), actual);
                    }
                }
            }
        }

        [Theory]
        [InlineData("1e+3", "{ \"Name\"=\"1e03\" }")]
        [InlineData("1e+3", "{ \"Name\"=\"1E03\" }")]
        [InlineData("1e+3", "{ \"Name\"=\"1e+03\" }")]
        [InlineData("1e+3", "{ \"Name\"=\"1E+03\" }")]
        [InlineData("1e-3", "{ \"Name\"=\"1e-03\" }")]
        [InlineData("1e-3", "{ \"Name\"=\"1E-03\" }")]
        [InlineData(".12345e+3", "{ \"Name\"=\".12345e03\" }")]
        [InlineData(".12345e+3", "{ \"Name\"=\".12345E03\" }")]
        [InlineData(".12345e+3", "{ \"Name\"=\".12345e+03\" }")]
        [InlineData(".12345e+3", "{ \"Name\"=\".12345E+03\" }")]
        [InlineData(".12345e-3", "{ \"Name\"=\".12345e-03\" }")]
        [InlineData(".12345e-3", "{ \"Name\"=\".12345E-03\" }")]
        [InlineData("1.2345e+3", "{ \"Name\"=\"1.2345e03\" }")]
        [InlineData("1.2345e+3", "{ \"Name\"=\"1.2345E03\" }")]
        [InlineData("1.2345e+3", "{ \"Name\"=\"1.2345e+03\" }")]
        [InlineData("1.2345e+3", "{ \"Name\"=\"1.2345E+03\" }")]
        [InlineData("1.2345e-3", "{ \"Name\"=\"1.2345e-03\" }")]
        [InlineData("1.2345e-3", "{ \"Name\"=\"1.2345E-03\" }")]
        public void op_ReadEntry_whenExponentProperty(string expected, 
                                                      string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonStreamReader(stream))
                    {
                        var actual = (double)reader.ReadEntry().Name;

                        Assert.Equal(XmlConvert.ToDouble(expected), actual);
                    }
                }
            }
        }

        [Theory]
        [InlineData(12345, "{ \"Name\"=\"12345\" }")]
        public void op_ReadEntry_whenIntegerProperty(long expected, 
                                                     string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonStreamReader(stream))
                    {
                        var actual = (long)reader.ReadEntry().Name;

                        Assert.Equal(expected, actual);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{ \"Name\"=\"null\" }")]
        public void op_ReadEntry_whenNullProperty(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonStreamReader(stream))
                    {
                        var actual = reader.ReadEntry().Name;

                        Assert.Null(actual);
                    }
                }
            }
        }

        [Theory]
        [InlineData("{}")]
        [InlineData("{ }")]
        [InlineData("{\r}")]
        [InlineData("{\r\n}")]
        public void op_ReadEntry_whenObjectEmpty(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonStreamReader(stream))
                    {
                        Assert.NotNull(reader.ReadEntry());
                    }
                }
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void op_ReadEntry_whenObjectNull(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonStreamReader(stream))
                    {
                        Assert.Null(reader.ReadEntry());
                    }
                }
            }
        }

        [Theory]
        [InlineData("", "{ \"Name\"=\"\" }")]
        [InlineData(" ", "{ \"Name\"=\" \" }")]
        [InlineData("\"", "{ \"Name\"=\"\\\"\" }")]
        [InlineData("\\", "{ \"Name\"=\"\\\\\" }")]
        [InlineData("/", "{ \"Name\"=\"\\/\" }")]
        [InlineData("\b", "{ \"Name\"=\"\\b\" }")]
        [InlineData("\f", "{ \"Name\"=\"\\f\" }")]
        [InlineData("\n", "{ \"Name\"=\"\\n\" }")]
        [InlineData("\r", "{ \"Name\"=\"\\r\" }")]
        [InlineData("\t", "{ \"Name\"=\"\\t\" }")]
        [InlineData("\u0066", "{ \"Name\"=\"\\u0066\" }")]
        [InlineData("Value", "{ \"Name\"=\"Value\" }")]
        [InlineData("Value", "{\r\"Name\"=\"Value\"\r}")]
        [InlineData("Value", "{\r\n\"Name\"=\"Value\"\r\n}")]
        [InlineData("True", "{ \"Name\"=\"True\" }")]
        [InlineData("False", "{ \"Name\"=\"False\" }")]
        [InlineData("Null", "{ \"Name\"=\"Null\" }")]
        public void op_ReadEntry_whenStringProperty(string expected, 
                                                    string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonStreamReader(stream))
                    {
                        var actual = (string)reader.ReadEntry().Name;

                        Assert.Equal(expected, actual);
                    }
                }
            }
        }
    }
}