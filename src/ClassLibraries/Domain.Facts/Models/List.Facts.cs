namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using Cavity.Configuration;
    using Cavity.IO;
    using Cavity.Xml.XPath;
    using Xunit;

    public sealed class ListFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<List>()
                            .DerivesFrom<List<string>>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("list")
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new List());
        }

        [Fact]
        public void deserialize()
        {
            var obj = ("<list>" +
                       "<item>first</item>" +
                       "<item>last</item>" +
                       "</list>").XmlDeserialize<List>();

            Assert.Equal("first", obj.First());
            Assert.Equal("last", obj.Last());
        }

        [Fact]
        public void deserializeEmpty()
        {
            Assert.Equal(0, "<list />".XmlDeserialize<List>().Count);
        }

        [Fact]
        public void op_GetSchema()
        {
            Assert.Throws<NotSupportedException>(() => (new List() as IXmlSerializable).GetSchema());
        }

        [Fact]
        public void op_ReadXml_XmlReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new List() as IXmlSerializable).ReadXml(null));
        }

        [Fact]
        public void op_To_whenBoolean()
        {
            var obj = new List
            {
                "true"
            };

            Assert.True(obj.To<bool>().First());
        }

        [Fact]
        public void op_To_whenDateTime()
        {
            var expected = DateTime.UtcNow;
            var obj = new List
            {
                expected.ToXmlString()
            };

            Assert.Equal(expected, obj.To<DateTime>().First().ToUniversalTime());
        }

        [Fact]
        public void op_To_whenInt32()
        {
            var obj = new List
            {
                "123"
            };

            Assert.Equal(123, obj.To<int>().First());
        }

        [Fact]
        public void op_WriteXml_XmlWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new List() as IXmlSerializable).WriteXml(null));
        }

        [Fact]
        public void serialize()
        {
            var obj = new List
            {
                "example"
            };

            Assert.True(obj.XmlSerialize().CreateNavigator().Evaluate<bool>("1=count(/list/item[text()='example'])"));
        }

        [Fact]
        public void config()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.xml");
                file.Create("<list><item>example</item></list>");

                Assert.Equal("example", Config.Xml<List>(file).First());
            }
        }
    }
}