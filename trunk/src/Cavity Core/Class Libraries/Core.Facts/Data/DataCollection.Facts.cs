namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Xml.Serialization;
    using Cavity.Xml.XPath;
    using Xunit;

    public sealed class DataCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DataCollection>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .XmlRoot("data")
                            .Implements<IEnumerable<KeyStringPair>>()
                            .Implements<IXmlSerializable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DataCollection());
        }

        [Fact]
        public void deserialize()
        {
            var obj = ("<data>" +
                       "<value name='name1'>value1</value>" +
                       "<value name='name2'>value2</value>" +
                       "</data>").XmlDeserialize<DataCollection>();

            Assert.True(obj.Contains("name1"));
            Assert.True(obj.Contains("name2"));
        }

        [Fact]
        public void deserializeEmpty()
        {
            Assert.Equal(0, "<data />".XmlDeserialize<DataCollection>().Count);
        }

        [Fact]
        public void indexer_int_get()
        {
            var obj = new DataCollection
            {
                {
                    "one", "1"
                    },
                {
                    "two", "2"
                    },
                {
                    "three", "3"
                    }
            };

            Assert.Equal("two", obj[1].Key);
            Assert.Equal("2", obj[1].Value);
        }

        [Fact]
        public void indexer_int_get_whenNotFound()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DataCollection()[99]);
        }

        [Fact]
        public void indexer_int_set()
        {
            var obj = new DataCollection
            {
                {
                    "one", "1"
                    },
                {
                    "nine", "9"
                    }
            };

            obj[1] = new KeyStringPair("two", "2");

            Assert.Equal("two", obj[1].Key);
            Assert.Equal("2", obj[1].Value);
        }

        [Fact]
        public void indexer_int_set_whenAdd()
        {
            var obj = new DataCollection
            {
                {
                    "one", "1"
                    }
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => obj[1] = new KeyStringPair("two", "2"));
        }

        [Fact]
        public void indexer_string_get()
        {
            const string expected = "2";

            var obj = new DataCollection
            {
                {
                    "one", "1"
                    },
                {
                    "two", expected
                    },
                {
                    "three", "3"
                    }
            };

            var actual = obj["two"];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void indexer_string_get_whenMultiple()
        {
            const string expected = "1,2,3";

            var obj = new DataCollection
            {
                {
                    "name", "1"
                    },
                {
                    "name", "2"
                    },
                {
                    "name", "3"
                    }
            };

            var actual = obj["name"];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void indexer_string_get_whenNotFound()
        {
            Assert.Null(new DataCollection()["unknown"]);
        }

        [Fact]
        public void indexer_string_set()
        {
            var expected = new DataCollection
            {
                {
                    "one", "1"
                    },
                {
                    "two", "2"
                    }
            };

            var actual = new DataCollection
            {
                {
                    "one", "1"
                    },
                {
                    "two", string.Empty
                    }
            };
            actual["two"] = "2";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void indexer_string_set_whenAdd()
        {
            var expected = new DataCollection
            {
                {
                    "one", "1"
                    },
                {
                    "two", "2"
                    }
            };

            var actual = new DataCollection
            {
                {
                    "one", "1"
                    }
            };
            actual["two"] = "2";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void indexer_string_set_whenMultiple()
        {
            var obj = new DataCollection();
            obj["name"] = "1,2,3";

            Assert.Equal("name", obj[0].Key);
            Assert.Equal("name", obj[1].Key);
            Assert.Equal("name", obj[2].Key);

            Assert.Equal("1", obj[0].Value);
            Assert.Equal("2", obj[1].Value);
            Assert.Equal("3", obj[2].Value);
        }

        [Fact]
        public void indexer_string_set_whenMultipleReplace()
        {
            var obj = new DataCollection();
            obj["name"] = "1,2,3";
            obj["name"] = "4,5,6";

            Assert.Equal("name", obj[0].Key);
            Assert.Equal("name", obj[1].Key);
            Assert.Equal("name", obj[2].Key);

            Assert.Equal("4", obj[0].Value);
            Assert.Equal("5", obj[1].Value);
            Assert.Equal("6", obj[2].Value);
        }

        [Fact]
        public void opEquality_DataCollection_DataCollection_FalseTest()
        {
            var operand1 = new DataCollection();
            var operand2 = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_DataCollection_DataCollection_SameTest()
        {
            var operand1 = new DataCollection();
            var operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_DataCollection_DataCollection_TrueTest()
        {
            var operand1 = new DataCollection();
            var operand2 = new DataCollection();

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opInequality_DataCollection_DataCollection_FalseTest()
        {
            var operand1 = new DataCollection();
            var operand2 = new DataCollection();

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_DataCollection_DataCollection_SameTest()
        {
            var operand1 = new DataCollection();
            var operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_DataCollection_DataCollection_TrueTest()
        {
            var operand1 = new DataCollection();
            var operand2 = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void op_Add_DataCollection()
        {
            var expected = new DataCollection
            {
                new KeyStringPair("name1", "value1"),
                new KeyStringPair("name2", "value2")
            };

            var actual = new DataCollection
            {
                expected
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_DataCollectionEmpty()
        {
            var expected = new DataCollection();
            var actual = new DataCollection
            {
                new DataCollection()
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_DataCollectionNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DataCollection().Add(null));
        }

        [Fact]
        public void op_Add_stringEmpty_string()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DataCollection().Add(string.Empty, "value"));
        }

        [Fact]
        public void op_Add_stringNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new DataCollection().Add(null, "value"));
        }

        [Fact]
        public void op_Add_string_string()
        {
            var obj = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Add_string_stringEmpty()
        {
            var obj = new DataCollection
            {
                {
                    "name", string.Empty
                    }
            };

            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Add_string_stringMultiple()
        {
            var obj = new DataCollection
            {
                {
                    "name", "1,2,3"
                    }
            };

            Assert.Equal("name", obj[0].Key);
            Assert.Equal("name", obj[1].Key);
            Assert.Equal("name", obj[2].Key);

            Assert.Equal("1", obj[0].Value);
            Assert.Equal("2", obj[1].Value);
            Assert.Equal("3", obj[2].Value);

            obj.Add("name", "4,5,6");

            Assert.Equal("name", obj[3].Key);
            Assert.Equal("name", obj[4].Key);
            Assert.Equal("name", obj[5].Key);

            Assert.Equal("4", obj[3].Value);
            Assert.Equal("5", obj[4].Value);
            Assert.Equal("6", obj[5].Value);
        }

        [Fact]
        public void op_Add_string_stringNull()
        {
            var obj = new DataCollection
            {
                {
                    "name", null
                    }
            };

            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Contains_string()
        {
            var obj = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            Assert.True(obj.Contains("name"));
        }

        [Fact]
        public void op_Contains_stringEmpty()
        {
            Assert.False(new DataCollection().Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringMissing()
        {
            var obj = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            Assert.False(obj.Contains("???"));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            Assert.False(new DataCollection().Contains(null));
        }

        [Fact]
        public void op_Contains_string_string()
        {
            var obj = new DataCollection
            {
                {
                    "name", string.Empty
                    },
                {
                    "name", "value"
                    }
            };

            Assert.True(obj.Contains("name", "value"));
        }

        [Fact]
        public void op_Contains_string_stringMissing()
        {
            var obj = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            Assert.False(obj.Contains("name", "???"));
        }

        [Fact]
        public void op_Equals_object()
        {
            var obj = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            var comparand = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectDiffer()
        {
            var obj = new DataCollection
            {
                {
                    "name", "value"
                    }
            };

            var comparand = new DataCollection
            {
                {
                    "foo", "bar"
                    }
            };

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new DataCollection().Equals(null));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = new DataCollection();
            object other = obj;

            Assert.True(obj.Equals(other));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new DataCollection().Equals("test"));
        }

        [Fact]
        public void op_FromPostData_NameValueCollection()
        {
            var form = new NameValueCollection
            {
                {
                    "foo", "bar"
                    },
                {
                    "checkbox", "first,second"
                    }
            };

            var obj = DataCollection.FromPostData(form);

            Assert.Equal("bar", obj["foo"]);
            Assert.Equal("checkbox", obj[1].Key);
            Assert.Equal("first", obj[1].Value);
            Assert.Equal("checkbox", obj[2].Key);
            Assert.Equal("second", obj[2].Value);
        }

        [Fact]
        public void op_FromPostData_NameValueCollectionEmpty()
        {
            Assert.Empty(DataCollection.FromPostData(new NameValueCollection()));
        }

        [Fact]
        public void op_FromPostData_NameValueCollectionNull()
        {
            Assert.Throws<ArgumentNullException>(() => DataCollection.FromPostData(null));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            Assert.IsAssignableFrom<IEnumerator>((new DataCollection() as IEnumerable).GetEnumerator());
        }

        [Fact]
        public void op_GetEnumeratorOfT()
        {
            Assert.IsAssignableFrom<IEnumerator<KeyStringPair>>((new DataCollection() as IEnumerable<KeyStringPair>).GetEnumerator());
        }

        [Fact]
        public void op_GetHashCode()
        {
            var obj = new DataCollection();

            var expected = obj.ToString().GetHashCode();
            var actual = obj.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetSchema()
        {
            Assert.Throws<NotSupportedException>(() => (new DataCollection() as IXmlSerializable).GetSchema());
        }

        [Fact]
        public void op_ReadXml_XmlReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new DataCollection() as IXmlSerializable).ReadXml(null));
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new DataCollection
            {
                {
                    "name1", "value1"
                    },
                {
                    "name2", "value2"
                    }
            };

            var expected = obj.XmlSerialize().CreateNavigator().OuterXml;
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_WriteXml_XmlWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new DataCollection() as IXmlSerializable).WriteXml(null));
        }

        [Fact]
        public void prop_Count()
        {
            Assert.True(new PropertyExpectations<DataCollection>(p => p.Count)
                            .IsNotDecorated()
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .Result);
        }

        [Fact]
        public void serialize()
        {
            var obj = new DataCollection
            {
                {
                    "foo", "bar"
                    }
            };

            Assert.True(obj.XmlSerialize().CreateNavigator().Evaluate<bool>("1=count(/data/value[@name='foo'][text()='bar'])"));
        }
    }
}