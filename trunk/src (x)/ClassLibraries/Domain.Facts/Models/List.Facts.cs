﻿namespace Cavity.Models
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

                Assert.Equal("example", Config.Xml<List>(file).First());
            }
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
        public void op_AsEnumerable_ofString()
        {
            const string expected = "example";
            var obj = new List
            {
                expected
            };

            Assert.Equal(expected, obj.AsEnumerable().First());
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
        public void op_ToEnumerable_ofBoolean()
        {
            var obj = new List
            {
                "true"
            };

            Assert.True(obj.ToEnumerable<bool>().First());
        }

        [Fact]
        public void op_ToEnumerable_ofDateTime()
        {
            var expected = DateTime.UtcNow;
            var obj = new List
            {
                expected.ToXmlString()
            };

            Assert.Equal(expected, obj.ToEnumerable<DateTime>().First().ToUniversalTime());
        }

        [Fact]
        public void op_ToEnumerable_ofInt32()
        {
            var obj = new List
            {
                "123"
            };

            Assert.Equal(123, obj.ToEnumerable<int>().First());
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
    }
}