namespace Cavity.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net.Mime;
    using Cavity;
    using Xunit;

    public sealed class HttpHeaderCollectionFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpHeaderCollection>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .Implements<IComparable>()
                .Implements<IHttpHeaderCollection>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpHeaderCollection());
        }

        [Fact]
        public void prop_Count()
        {
            Assert.NotNull(new PropertyExpectations<HttpHeaderCollection>("Count")
                .TypeIs<int>()
                .DefaultValueIs(0)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_ContentType()
        {
            Assert.NotNull(new PropertyExpectations<HttpHeaderCollection>("ContentType")
                .TypeIs<ContentType>()
                .DefaultValueIsNull()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_ContentType_get()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("Content-Type", "text/plain") }
            };

            Assert.Equal<string>("text/plain", obj.ContentType.MediaType);
        }

        [Fact]
        public void prop_IsReadOnly()
        {
            Assert.NotNull(new PropertyExpectations<HttpHeaderCollection>("IsReadOnly")
                .TypeIs<bool>()
                .DefaultValueIs(false)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opIndexer_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderCollection()[null as Token]);
        }

        [Fact]
        public void opIndexer_string()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.Equal<string>("value", obj["name"]);
        }

        [Fact]
        public void opIndexer_string_whenEmpty()
        {
            Assert.Null(new HttpHeaderCollection()["name"]);
        }

        [Fact]
        public void opIndexer_string_whenMultiple()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") },
                { new HttpHeader("name", "bar") }
            };

            Assert.Equal<string>("foo, bar", obj["name"]);
        }

        [Fact]
        public void opImplicit_stringNull_HttpHeaderCollection()
        {
            string expected = null;
            string actual = null as HttpHeaderCollection;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_stringEmpty_HttpHeaderCollection()
        {
            string expected = string.Empty;
            string actual = new HttpHeaderCollection();

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_HttpHeaderCollection()
        {
            string expected = "name: value" + Environment.NewLine;
            string actual = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpHeaderCollection_stringNull()
        {
            HttpHeaderCollection obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpHeaderCollection_stringEmpty()
        {
            HttpHeaderCollection expected = string.Empty;
            HttpHeaderCollection actual = new HttpHeaderCollection();

            Assert.Equal<HttpHeaderCollection>(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpHeaderCollection_string()
        {
            HttpHeaderCollection expected = "name: value";
            HttpHeaderCollection actual = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.Equal<HttpHeaderCollection>(expected, actual);
        }

        [Fact]
        public void opEquality_HttpHeaderCollection_HttpHeaderCollection_whenTrue()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpHeaderCollection_HttpHeaderCollection_whenFalse()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpHeaderCollection_HttpHeaderCollection_whenSame()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpHeaderCollectionNull_HttpHeaderCollection()
        {
            HttpHeaderCollection operand1 = null;
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpHeaderCollection_HttpHeaderCollectionNull()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = null;

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opInequality_HttpHeaderCollection_HttpHeaderCollection_whenTrue()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpHeaderCollection_HttpHeaderCollection_whenFalse()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpHeaderCollection_HttpHeaderCollection_whenSame()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpHeaderCollectionNull_HttpHeaderCollection()
        {
            HttpHeaderCollection operand1 = null;
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpHeaderCollection_HttpHeaderCollectionNull()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = null;

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_HttpHeaderCollection_HttpHeaderCollection_whenSame()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpHeaderCollection_HttpHeaderCollection_whenTrue()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpHeaderCollection_HttpHeaderCollection_whenFalse()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpHeaderCollectionNull_HttpHeaderCollection()
        {
            HttpHeaderCollection operand1 = null;
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpHeaderCollection_HttpHeaderCollectionNull()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_HttpHeaderCollection_HttpHeaderCollection_whenSame()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpHeaderCollection_HttpHeaderCollection_whenTrue()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpHeaderCollection_HttpHeaderCollection_whenFalse()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpHeaderCollectionNull_HttpHeaderCollection()
        {
            HttpHeaderCollection operand1 = null;
            HttpHeaderCollection operand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpHeaderCollection_HttpHeaderCollectionNull()
        {
            HttpHeaderCollection operand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_HttpHeaderCollection_HttpHeaderCollection_Equal()
        {
            HttpHeaderCollection comparand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection comparand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.Equal<int>(0, HttpHeaderCollection.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpHeaderCollection_HttpHeaderCollection_whenSame()
        {
            HttpHeaderCollection comparand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection comparand2 = comparand1;

            Assert.Equal<int>(0, HttpHeaderCollection.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpHeaderCollectionNull_HttpHeaderCollection()
        {
            HttpHeaderCollection comparand1 = null;
            HttpHeaderCollection comparand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.True(HttpHeaderCollection.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_HttpHeaderCollection_HttpHeaderCollectionNull()
        {
            HttpHeaderCollection comparand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection comparand2 = null;

            Assert.True(HttpHeaderCollection.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpHeaderCollectionGreater_HttpHeaderCollection()
        {
            HttpHeaderCollection comparand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };
            HttpHeaderCollection comparand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };

            Assert.True(HttpHeaderCollection.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpHeaderCollectionLesser_HttpHeaderCollection()
        {
            HttpHeaderCollection comparand1 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };
            HttpHeaderCollection comparand2 = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };

            Assert.True(HttpHeaderCollection.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderCollection.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            HttpHeaderCollection expected = new HttpHeaderCollection();
            HttpHeaderCollection actual = HttpHeaderCollection.Parse(string.Empty);

            Assert.Equal<HttpHeaderCollection>(expected, actual);
        }

        [Fact]
        public void op_Parse_string()
        {
            HttpHeaderCollection expected = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection actual = HttpHeaderCollection.Parse("name: value");

            Assert.Equal<HttpHeaderCollection>(expected, actual);
        }

        [Fact]
        public void op_Add_IHttpHeader()
        {
            var obj = new HttpHeaderCollection();
            var item = new HttpHeader("name", "value");

            Assert.Equal<int>(0, obj.Count);

            obj.Add(item);

            Assert.Equal<int>(1, obj.Count);
        }

        [Fact]
        public void op_Add_IHttpHeaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderCollection().Add(null as IHttpHeader));
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.Equal<int>(1, obj.Count);

            obj.Clear();

            Assert.Equal<int>(0, obj.Count);
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.True(new HttpHeaderCollection().CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            HttpHeaderCollection value = new HttpHeaderCollection();

            Assert.Equal<int>(0, value.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            HttpHeaderCollection left = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };
            HttpHeaderCollection right = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_object_whenMultiple()
        {
            HttpHeaderCollection left = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") },
                { new HttpHeader("name", "bar") }
            };
            HttpHeaderCollection right = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo, bar") }
            };

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            HttpHeaderCollection left = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };
            HttpHeaderCollection right = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            HttpHeaderCollection left = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };
            HttpHeaderCollection right = new HttpHeaderCollection
            {
                { new HttpHeader("name", "bar") }
            };

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpHeaderCollection().CompareTo(123));
        }

        [Fact]
        public void op_Contains_IHttpHeader_whenTrue()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                { item }
            };

            Assert.True(obj.Contains(item));
        }

        [Fact]
        public void op_Contains_IHttpHeader_whenFalse()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };

            Assert.False(obj.Contains(new HttpHeader("name", "bar")));
        }

        [Fact]
        public void op_Contains_IHttpHeaderNull()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.False(obj.Contains(null as IHttpHeader));
        }

        [Fact]
        public void op_CopyTo_IHttpHeader_int()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                { item }
            };

            IHttpHeader[] array = new IHttpHeader[1];

            obj.CopyTo(array, 0);

            Assert.Equal<HttpHeader>(item, (HttpHeader)array[0]);
        }

        [Fact]
        public void op_Equals_object()
        {
            HttpHeaderCollection obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.True(HttpHeaderCollection.Parse("name: value").Equals(obj));
        }

        [Fact]
        public void op_Equals_object_whenMutiple()
        {
            HttpHeaderCollection obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") },
                { new HttpHeader("name", "bar") }
            };

            Assert.True(HttpHeaderCollection.Parse("name: foo, bar").Equals(obj));
        }

        [Fact]
        public void op_Equals_object_this()
        {
            HttpHeaderCollection obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            Assert.True(obj.Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new HttpHeaderCollection().Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(HttpHeaderCollection.Parse("name: value").Equals("name: value"));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                { item }
            };

            using (IEnumerator<IHttpHeader> enumerator = obj.GetEnumerator())
            {
                enumerator.MoveNext();
                Assert.Same(item, enumerator.Current);
            }
        }

        [Fact]
        public void op_GetHashCode()
        {
            HttpHeaderCollection obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            int expected = obj.ToString().GetHashCode();
            int actual = obj.GetHashCode();

            Assert.Equal<int>(expected, actual);
        }

        [Fact]
        public void op_Remove_IHttpHeader()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                { item }
            };

            obj.Remove(item);

            Assert.Equal<int>(0, obj.Count);
        }

        [Fact]
        public void op_ToDictionary()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            IDictionary<string, string> dictionary = obj.ToDictionary();
            
            Assert.Equal<string>("value", dictionary["name"]);
        }

        [Fact]
        public void op_ToDictionary_whenCaseSensitive()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") },
                { new HttpHeader("Name", "bar") }
            };

            IDictionary<string, string> dictionary = obj.ToDictionary();

            Assert.Equal<string>("foo", dictionary["name"]);
            Assert.Equal<string>("bar", dictionary["Name"]);
        }

        [Fact]
        public void op_ToDictionary_whenEmpty()
        {
            Assert.Empty(new HttpHeaderCollection().ToDictionary());
        }

        [Fact]
        public void op_ToDictionary_whenMultiple()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") },
                { new HttpHeader("name", "bar") }
            };

            IDictionary<string, string> dictionary = obj.ToDictionary();

            Assert.Equal<string>("foo, bar", dictionary["name"]);
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") },
                { new HttpHeader("name", "bar") }
            };

            string expected = "name: foo" + Environment.NewLine + "name: bar" + Environment.NewLine;
            string actual = obj.ToString();

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void IEnumerable_op_GetEnumerator()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                { item }
            };

            IEnumerator enumerator = (obj as IEnumerable).GetEnumerator();
            enumerator.MoveNext();
            Assert.Same(item, enumerator.Current);
        }
    }
}