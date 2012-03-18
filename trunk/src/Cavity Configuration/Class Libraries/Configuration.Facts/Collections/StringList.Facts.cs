namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    using Cavity.Configuration;
    using Cavity.IO;
    using Cavity.Xml.XPath;

    using Xunit;

    public sealed class StringListFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<StringList>()
                            .DerivesFrom<List<string>>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .XmlRoot("list")
                            .Implements<IXmlSerializable>()
                            .Result);
        }

        [Fact]
        public void config()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.xml");
                file.Create("<list><item>example</item></list>");

                Assert.Equal("example", Config.Xml<StringList>(file).First());
            }
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new StringList());
        }

        [Fact]
        public void deserialize()
        {
            var obj = ("<list>" +
                       "<item>first</item>" +
                       "<item>last</item>" +
                       "</list>").XmlDeserialize<StringList>();

            Assert.Equal("first", obj.First());
            Assert.Equal("last", obj.Last());
        }

        [Fact]
        public void deserializeEmpty()
        {
            Assert.Equal(0, "<list />".XmlDeserialize<StringList>().Count);
        }

        [Fact]
        public void op_AsEnumerable_ofString()
        {
            const string expected = "example";
            var obj = new StringList
                          {
                              expected
                          };

            Assert.Equal(expected, obj.AsEnumerable().First());
        }

        [Fact]
        public void op_GetSchema()
        {
            Assert.Throws<NotSupportedException>(() => (new StringList() as IXmlSerializable).GetSchema());
        }

        [Fact]
        public void op_ReadXml_XmlReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new StringList() as IXmlSerializable).ReadXml(null));
        }

        [Fact]
        public void op_ToEnumerable_ofBoolean()
        {
            var obj = new StringList
                          {
                              "true"
                          };

            Assert.True(obj.ToEnumerable<bool>().First());
        }

        [Fact]
        public void op_ToEnumerable_ofDateTime()
        {
            var expected = DateTime.UtcNow;
            var obj = new StringList
                          {
                              expected.ToXmlString()
                          };

            Assert.Equal(expected, obj.ToEnumerable<DateTime>().First().ToUniversalTime());
        }

        [Fact]
        public void op_ToEnumerable_ofInt32()
        {
            var obj = new StringList
                          {
                              "123"
                          };

            Assert.Equal(123, obj.ToEnumerable<int>().First());
        }

        [Fact]
        public void op_WriteXml_XmlWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new StringList() as IXmlSerializable).WriteXml(null));
        }

        [Fact]
        public void serialize()
        {
            var obj = new StringList
                          {
                              "example"
                          };

            Assert.True(obj.XmlSerialize().CreateNavigator().Evaluate<bool>("1=count(/list/item[text()='example'])"));
        }
    }
}