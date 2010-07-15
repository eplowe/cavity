namespace Cavity.Xml
{
    using System;
    using System.IO;
    using System.Net.Mime;
    using System.Xml;
    using System.Xml.XPath;
    using Cavity;
    using Cavity.Net.Mime;
    using Xunit;

    public sealed class ApplicationXmlFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ApplicationXml>()
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
            Assert.NotNull(new ApplicationXml());
        }

        [Fact]
        public void ctor_IXPathNavigableNull()
        {
            Assert.NotNull(new ApplicationXml(null));
        }

        [Fact]
        public void ctor_IXPathNavigable()
        {
            Assert.NotNull(new ApplicationXml(new XmlDocument()));
        }

        [Fact]
        public void prop_Content()
        {
            Assert.NotNull(new PropertyExpectations<ApplicationXml>("Content")
                .IsAutoProperty<object>()
                .ArgumentOutOfRangeException(1234)
                .Set(new XmlDocument())
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_ContentType()
        {
            Assert.NotNull(new PropertyExpectations<ApplicationXml>("ContentType")
                .TypeIs<ContentType>()
                .DefaultValueIs(new ContentType("application/xml"))
                .ArgumentNullException()
                .Set(new ContentType("application/xml"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Xml()
        {
            Assert.NotNull(new PropertyExpectations<ApplicationXml>("Xml")
                .TypeIs<IXPathNavigable>()
                .DefaultValueIsNull()
                .Set(null)
                .Set(new XmlDocument())
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_ApplicationXml_stringNull()
        {
            ApplicationXml obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_ApplicationXml_stringEmpty()
        {
            ApplicationXml expected;

            Assert.Throws<ArgumentOutOfRangeException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_ApplicationXml_string()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<root />");

            var expected = new ApplicationXml(xml);
            ApplicationXml actual = "<root />";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => ApplicationXml.FromString(null));
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ApplicationXml.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_string()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<root />");

            var expected = new ApplicationXml(xml);
            var actual = ApplicationXml.FromString("<root />");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToContent_TextReader()
        {
            IContent body;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("<root />");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        body = new ApplicationXml().ToContent(reader);
                    }
                }
            }

            Assert.Equal<string>("<root />", (body as ApplicationXml));
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
                        body = new ApplicationXml().ToContent(reader);
                    }
                }
            }

            Assert.Equal<string>(null, body as ApplicationXml);
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
                        body = new ApplicationXml().ToContent(reader);
                    }
                }
            }

            Assert.Equal<string>(null, body as ApplicationXml);
        }

        [Fact]
        public void op_ToContent_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationXml().ToContent(null));
        }

        [Fact]
        public void op_Write_TextWriter_whenPost()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    ApplicationXml.FromString("<root />").Write(writer);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        Assert.Equal("<root />", reader.ReadToEnd());
                    }
                }
            }
        }

        [Fact]
        public void op_Write_TextWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationXml().Write(null));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "<root />";
            var actual = ApplicationXml.FromString(expected).ToString();

            Assert.Equal(expected, actual);
        }
    }
}