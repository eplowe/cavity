namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                            .XmlSerializable()
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
        public void op_Equals_object()
        {
            var obj = new StringList
                          {
                              "example"
                          };

            var comparand = new StringList
                                {
                                    "example"
                                };

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new StringList().Equals(null));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = new StringList();

            // ReSharper disable EqualExpressionComparison
            Assert.True(obj.Equals(obj));

            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public void op_Equals_object_whenFalse()
        {
            var obj = new StringList
                          {
                              "123"
                          };

            var comparand = new StringList
                                {
                                    "456"
                                };

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = string.Empty.GetHashCode();
            var actual = new StringList().GetHashCode();

            Assert.Equal(expected, actual);
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
        public void op_ToString()
        {
            var expected = string.Empty;
            var actual = new StringList().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenMultipleItems()
        {
            var expected = "123{0}456{0}".FormatWith(Environment.NewLine);

            var obj = new StringList
                          {
                              "123",
                              "456"
                          };
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenSingleItem()
        {
            var expected = "example" + Environment.NewLine;

            var obj = new StringList
                          {
                              "example"
                          };
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xml_deserialize()
        {
            var obj = ("<list>" +
                       "<item>first</item>" +
                       "<item>last</item>" +
                       "</list>").XmlDeserialize<StringList>();

            Assert.Equal("first", obj.First());
            Assert.Equal("last", obj.Last());
        }

        [Fact]
        public void xml_serialize()
        {
            var obj = new StringList
                          {
                              "example"
                          };

            Assert.True(obj.XmlSerialize().CreateNavigator().Evaluate<bool>("1=count(/list/item[text()='example'])"));
        }
    }
}