namespace Cavity.Net.Mime
{
    using System;
    using System.IO;
    using Cavity;
    using Xunit;

    public sealed class TextPlainFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<TextPlain>()
                .DerivesFrom<Text>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<IContent>()
                .Implements<IMediaType>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TextPlain());
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new TextPlain(null as string));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new TextPlain(string.Empty));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new TextPlain("value"));
        }

        [Fact]
        public void opImplicit_TextPlain_stringNull()
        {
            TextPlain obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_TextPlain_stringEmpty()
        {
            TextPlain expected = string.Empty;
            TextPlain actual = new TextPlain(string.Empty);

            Assert.Equal<TextPlain>(expected, actual);
        }

        [Fact]
        public void opImplicit_TextPlain_string()
        {
            TextPlain expected = "value";
            TextPlain actual = new TextPlain("value");

            Assert.Equal<TextPlain>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => TextPlain.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            TextPlain expected = new TextPlain(string.Empty);
            TextPlain actual = TextPlain.Parse(string.Empty);

            Assert.Equal<TextPlain>(expected, actual);
        }

        [Fact]
        public void op_Parse_string()
        {
            TextPlain expected = new TextPlain("value");
            TextPlain actual = TextPlain.Parse("value");

            Assert.Equal<TextPlain>(expected, actual);
        }

        [Fact]
        public void op_ToBody_StreamReader()
        {
            IContent body = null;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("text");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        body = new TextPlain().ToBody(reader);
                    }
                }
            }

            Assert.Equal<string>("text", (body as TextPlain).Value);
        }

        [Fact]
        public void op_ToBody_StreamReader_whenStringEmpty()
        {
            IContent body = null;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(string.Empty);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        body = new TextPlain().ToBody(reader);
                    }
                }
            }

            Assert.Null((body as TextPlain).Value);
        }

        [Fact]
        public void op_ToBody_StreamReaderEmpty()
        {
            IContent body = null;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        body = new TextPlain().ToBody(reader);
                    }
                }
            }

            Assert.Null((body as TextPlain).Value);
        }

        [Fact]
        public void op_ToBody_StreamReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TextPlain().ToBody(null as StreamReader));
        }

        [Fact]
        public void op_Write_StreamWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TextPlain().Write(null as StreamWriter));
        }
    }
}