namespace Cavity.Text
{
    using System;
    using System.IO;
    using System.Net.Mime;
    using Cavity.Net.Mime;
    using Xunit;

    public sealed class TextPlainFacts
    {
        [Fact]
        public void a_definition()
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
        public void ctor_string()
        {
            Assert.NotNull(new TextPlain("value"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new TextPlain(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new TextPlain(null));
        }

        [Fact]
        public void opImplicit_TextPlain_string()
        {
            var expected = new TextPlain("value");
            TextPlain actual = "value";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_TextPlain_stringEmpty()
        {
            var expected = new TextPlain(string.Empty);
            TextPlain actual = string.Empty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_TextPlain_stringNull()
        {
            TextPlain obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new TextPlain("value");
            var actual = TextPlain.FromString("value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            var expected = new TextPlain(string.Empty);
            var actual = TextPlain.FromString(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => TextPlain.FromString(null));
        }

        [Fact]
        public void op_ToContent_TextReader()
        {
            IContent body;

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

            Assert.Equal("text", ((TextPlain)body).Value);
        }

        [Fact]
        public void op_ToContent_TextReaderEmpty()
        {
            IContent body;

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

            Assert.Equal(string.Empty, ((TextPlain)body).Value);
        }

        [Fact]
        public void op_ToContent_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TextPlain().ToContent(null));
        }

        [Fact]
        public void op_ToContent_TextReader_whenStringEmpty()
        {
            IContent body;

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

            Assert.Equal(string.Empty, ((TextPlain)body).Value);
        }

        [Fact]
        public void op_ToString()
        {
            var expected = string.Empty;
            var actual = new TextPlain().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenNotNull()
        {
            const string expected = "value";
            var actual = new TextPlain(expected).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Write_TextWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TextPlain().Write(null));
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
                        Assert.Equal("value", reader.ReadToEnd());
                    }
                }
            }
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
                               .DefaultValueIs(new ContentType("text/plain"))
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
    }
}