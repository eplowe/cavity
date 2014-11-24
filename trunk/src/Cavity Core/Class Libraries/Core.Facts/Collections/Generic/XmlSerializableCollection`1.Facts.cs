namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml.Serialization;
    using Cavity.Xml.XPath;
    using Xunit;
    using Xunit.Extensions;

    public sealed class XmlSerializableCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlSerializableCollection<TestXmlSerializableCollectionItem>>()
                            .DerivesFrom<Collection<TestXmlSerializableCollectionItem>>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TestXmlSerializableCollection());
        }

        [Theory]
        [InlineData(true, 123)]
        [InlineData(false, 456)]
        public void op_Equals_TestXmlSerializableCollectionOfT(bool expected,
                                                               int value)
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123)
                          };

            var other = new TestXmlSerializableCollection
                            {
                                new TestXmlSerializableCollectionItem(value)
                            };

            var actual = obj.Equals(other);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_TestXmlSerializableCollectionOfTNull()
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123),
                              new TestXmlSerializableCollectionItem(456)
                          };

            Assert.False(obj.Equals(null));
        }

        [Fact]
        public void op_Equals_TestXmlSerializableCollectionOfT_whenCountsDiffer()
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123)
                          };

            var other = new TestXmlSerializableCollection
                            {
                                new TestXmlSerializableCollectionItem(123),
                                new TestXmlSerializableCollectionItem(456)
                            };

            Assert.False(obj.Equals(other));
        }

        [Fact]
        public void op_Equals_TestXmlSerializableCollectionOfT_whenSame()
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123),
                              new TestXmlSerializableCollectionItem(456)
                          };

            Assert.True(obj.Equals(obj));
        }

        [Theory]
        [InlineData(true, 123)]
        [InlineData(false, 456)]
        public void op_Equals_object(bool expected,
                                     int value)
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123)
                          };

            object other = new TestXmlSerializableCollection
                               {
                                   new TestXmlSerializableCollectionItem(value)
                               };

            var actual = obj.Equals(other);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123),
                              new TestXmlSerializableCollectionItem(456)
                          };

            object other = null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.False(obj.Equals(other));

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void op_Equals_object_whenCountsDiffer()
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123)
                          };

            var other = new TestXmlSerializableCollection
                            {
                                new TestXmlSerializableCollectionItem(123),
                                new TestXmlSerializableCollectionItem(456)
                            };

            Assert.False(obj.Equals(other));
        }

        [Fact]
        public void op_Equals_object_whenEmpty()
        {
            var obj = new TestXmlSerializableCollection();

            var other = new TestXmlSerializableCollection();

            Assert.True(obj.Equals(other));
        }

        [Fact]
        public void op_Equals_object_whenInvalidCast()
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123)
                          };

            const int other = 123;

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.Throws<InvalidCastException>(() => obj.Equals(other));

            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [Fact]
        public void op_Equals_object_whenSame()
        {
            object obj = new TestXmlSerializableCollection
                             {
                                 new TestXmlSerializableCollectionItem(123),
                                 new TestXmlSerializableCollectionItem(456)
                             };

            // ReSharper disable EqualExpressionComparison
            Assert.True(obj.Equals(obj));

            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public void op_GetHashCode()
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123)
                          };

            var expected = obj.ToString().GetHashCode();

            var actual = obj.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_IXmlSerializable_GetSchema()
        {
            IXmlSerializable obj = new TestXmlSerializableCollection();

            Assert.Throws<NotSupportedException>(() => obj.GetSchema());
        }

        [Fact]
        public void op_IXmlSerializable_ReadXml_XmlReaderNull()
        {
            IXmlSerializable obj = new TestXmlSerializableCollection();

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => obj.ReadXml(null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_IXmlSerializable_WriteXml_XmlWriterNull()
        {
            IXmlSerializable obj = new TestXmlSerializableCollection();

            Assert.Throws<ArgumentNullException>(() => obj.WriteXml(null));
        }

        [Fact]
        public void op_ToString()
        {
            var expected = "<collection>{0}  <item value=\"123\" />{0}</collection>".FormatWith(Environment.NewLine);

            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123)
                          };

            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xml_deserialize()
        {
            var obj = ("<collection>" +
                       "<item value='123' />" +
                       "<item value='456' />" +
                       "</collection>").XmlDeserialize<TestXmlSerializableCollection>();

            Assert.Equal(123, obj.First().Value);
            Assert.Equal(456, obj.Last().Value);
        }

        [Fact]
        public void xml_deserializeEmpty()
        {
            Assert.Equal(0, "<collection />".XmlDeserialize<TestXmlSerializableCollection>().Count);
        }

        [Theory]
        [InlineData("1=count(/collection)")]
        [InlineData("2=count(/collection/item)")]
        [InlineData("1=count(/collection/item[@value='123'])")]
        [InlineData("1=count(/collection/item[@value='456'])")]
        public void xml_serialize(string xpath)
        {
            var obj = new TestXmlSerializableCollection
                          {
                              new TestXmlSerializableCollectionItem(123),
                              new TestXmlSerializableCollectionItem(456)
                          };

            Assert.True(obj.XmlSerialize().CreateNavigator().Evaluate<bool>(xpath));
        }
    }
}