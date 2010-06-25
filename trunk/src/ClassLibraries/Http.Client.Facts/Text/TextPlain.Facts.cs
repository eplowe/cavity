namespace Cavity.Text
{
    using System;
    using System.IO;
    using System.Net.Mime;
    using Cavity;
    using Cavity.Net.Mime;
    using Xunit;

    public sealed class TextPlainFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<TextPlain>()
                .DerivesFrom<ComparableObject>()
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
        public void prop_Content()
        {
            Assert.NotNull(new PropertyExpectations<TextPlain>("Content")
                .IsAutoProperty<object>()
                .ArgumentOutOfRangeException(1234)
                .Set("value")
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_ContentType()
        {
            Assert.NotNull(new PropertyExpectations<TextPlain>("ContentType")
                .TypeIs<ContentType>()
                .ArgumentNullException()
                .Set(new ContentType("text/plain"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.NotNull(new PropertyExpectations<TextPlain>("Value")
                .IsAutoProperty<string>()
                .IsNotDecorated()
                .Result);
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
        public void op_ToContent_TextReader()
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
                        body = new TextPlain().ToContent(reader);
                    }
                }
            }

            Assert.Equal<string>("text", (body as TextPlain).Value);
        }

        [Fact]
        public void op_ToContent_TextReader_whenStringEmpty()
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
                        body = new TextPlain().ToContent(reader);
                    }
                }
            }

            Assert.Equal<string>(string.Empty, (body as TextPlain).Value);
        }

        [Fact]
        public void op_ToContent_TextReaderEmpty()
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
                        body = new TextPlain().ToContent(reader);
                    }
                }
            }

            Assert.Equal<string>(string.Empty, (body as TextPlain).Value);
        }

        [Fact]
        public void op_ToContent_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TextPlain().ToContent(null as TextReader));
        }

        [Fact]
        public void op_Write_TextWriter_whenPost()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    new TextPlain("value").Write(writer);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        Assert.Equal<string>("value", reader.ReadToEnd());
                    }
                }
            }
        }

        [Fact]
        public void op_Write_TextWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TextPlain().Write(null as TextWriter));
        }

        [Fact]
        public void op_ToString()
        {
            Assert.Null(new TextPlain().ToString());
        }

        [Fact]
        public void op_ToString_whenNotNull()
        {
            string expected = "value";
            string actual = new TextPlain(expected).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}