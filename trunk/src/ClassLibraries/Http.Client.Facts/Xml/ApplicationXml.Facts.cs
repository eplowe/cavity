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
        public void type_definition()
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
            Assert.NotNull(new ApplicationXml(null as IXPathNavigable));
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

            ApplicationXml expected = "<root />";
            ApplicationXml actual = new ApplicationXml(xml);

            Assert.Equal<ApplicationXml>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => ApplicationXml.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ApplicationXml.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<root />");

            ApplicationXml expected = new ApplicationXml(xml);
            ApplicationXml actual = ApplicationXml.Parse("<root />");

            Assert.Equal<ApplicationXml>(expected, actual);
        }

        [Fact]
        public void op_ToContent_TextReader()
        {
            IContent body = null;

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
                        body = new ApplicationXml().ToContent(reader);
                    }
                }
            }

            Assert.Equal<string>(null as string, (body as ApplicationXml));
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
                        body = new ApplicationXml().ToContent(reader);
                    }
                }
            }

            Assert.Equal<string>(null as string, (body as ApplicationXml));
        }

        [Fact]
        public void op_ToContent_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationXml().ToContent(null as TextReader));
        }

        [Fact]
        public void op_Write_TextWriter_whenPost()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    ApplicationXml.Parse("<root />").Write(writer);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        Assert.Equal<string>("<root />", reader.ReadToEnd());
                    }
                }
            }
        }

        [Fact]
        public void op_Write_TextWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationXml().Write(null as TextWriter));
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "<root />";
            string actual = ApplicationXml.Parse(expected).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}