namespace Cavity.Data
{
    using System;
    using System.IO;

    using Cavity;
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
        public void op_ReadEntry_whenStringProperty(string expected, string json)
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