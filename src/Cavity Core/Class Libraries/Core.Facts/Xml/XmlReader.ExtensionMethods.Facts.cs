namespace Cavity.Xml
{
    using System;
    using System.IO;
    using System.Xml;

    using Cavity;
    using Xunit;

    public sealed class XmlReaderExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(XmlReaderExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_Deserialize_XmlReader_string()
        {
            var expected = new DateTime(2009, 04, 25);

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("<dateTime>2009-04-25T00:00:00</dateTime>");
                    writer.Flush();
                    stream.Position = 0;

                    using (var reader = XmlReader.Create(stream))
                    {
                        Assert.True(reader.Read());
                        var actual = reader.Deserialize<DateTime>();

                        Assert.Equal(expected, actual);
                    }
                }
            }
        }

        [Fact]
        public void op_Deserialize_XmlReaderNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as XmlReader).Deserialize<DateTime>());
        }

        [Fact]
        public void op_IsEndElement_XmlReader_string()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("<example></example>");
                    writer.Flush();
                    stream.Position = 0;

                    using (var reader = XmlReader.Create(stream))
                    {
                        Assert.True(reader.Read());
                        Assert.False(reader.IsEndElement("example"));

                        Assert.True(reader.Read());
                        Assert.True(reader.IsEndElement("example"));
                    }
                }
            }
        }

        [Fact]
        public void op_IsEndElement_XmlReaderNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as XmlReader).IsEndElement("example"));
        }
    }
}