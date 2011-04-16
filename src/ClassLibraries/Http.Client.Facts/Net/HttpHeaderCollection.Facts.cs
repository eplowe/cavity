namespace Cavity.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mime;
    using System.Text;
    using Cavity.Net.Mime;
    using Xunit;

    public sealed class HttpHeaderCollectionFacts
    {
        [Fact]
        public void IEnumerable_op_GetEnumerator()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                item
            };

            var enumerator = (obj as IEnumerable).GetEnumerator();
            enumerator.MoveNext();
            Assert.Same(item, enumerator.Current);
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpHeaderCollection>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .Implements<IComparable>()
                            .Implements<ICollection<IHttpHeader>>()
                            .Implements<IContentType>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpHeaderCollection());
        }

        [Fact]
        public void opImplicit_HttpHeaderCollection_string()
        {
            var expected = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };
            HttpHeaderCollection actual = "name: value";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpHeaderCollection_stringEmpty()
        {
            var expected = new HttpHeaderCollection();
            HttpHeaderCollection actual = string.Empty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpHeaderCollection_stringNull()
        {
            HttpHeaderCollection obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opIndexer_string()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };

            Assert.Equal("value", obj["name"]);
        }

        [Fact]
        public void opIndexer_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderCollection()[null as Token]);
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
                new HttpHeader("name", "foo"),
                new HttpHeader("name", "bar")
            };

            Assert.Equal("foo, bar", obj["name"]);
        }

        [Fact]
        public void op_Add_IHttpHeader()
        {
            var obj = new HttpHeaderCollection();
            var item = new HttpHeader("name", "value");

            Assert.Equal(0, obj.Count);

            obj.Add(item);

            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Add_IHttpHeaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderCollection().Add(null));
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };

            Assert.Equal(1, obj.Count);

            obj.Clear();

            Assert.Equal(0, obj.Count);
        }

        [Fact]
        public void op_ContainsName_TokenNull()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "foo")
            };

            Assert.Throws<ArgumentNullException>(() => obj.ContainsName(null as string));
        }

        [Fact]
        public void op_ContainsName_Token_whenFalse()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("foo", "value")
            };

            Assert.False(obj.ContainsName("bar"));
        }

        [Fact]
        public void op_ContainsName_Token_whenTrue()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                item
            };

            Assert.True(obj.ContainsName("name"));
        }

        [Fact]
        public void op_Contains_IHttpHeaderNull()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };

            Assert.False(obj.Contains(null));
        }

        [Fact]
        public void op_Contains_IHttpHeader_whenFalse()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "foo")
            };

            Assert.False(obj.Contains(new HttpHeader("name", "bar")));
        }

        [Fact]
        public void op_Contains_IHttpHeader_whenTrue()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                item
            };

            Assert.True(obj.Contains(item));
        }

        [Fact]
        public void op_CopyTo_IHttpHeader_int()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                item
            };

            var array = new IHttpHeader[1];

            obj.CopyTo(array, 0);

            Assert.Equal(item, (HttpHeader)array[0]);
        }

        [Fact]
        public void op_Equals_object()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };

            Assert.True(HttpHeaderCollection.FromString("name: value").Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new HttpHeaderCollection().Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(HttpHeaderCollection.FromString("name: value").Equals("name: value"));
        }

        [Fact]
        public void op_Equals_object_whenMultiple()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "foo"),
                new HttpHeader("name", "bar")
            };

            Assert.True(HttpHeaderCollection.FromString("name: foo, bar").Equals(obj));
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };
            var actual = HttpHeaderCollection.FromString("name: value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            var expected = new HttpHeaderCollection();
            var actual = HttpHeaderCollection.FromString(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderCollection.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenMultipleLines()
        {
            var value =
                "a" + Environment.NewLine +
                ' ' + "b" + Environment.NewLine +
                '\t' + "c";

            var expected = new HttpHeaderCollection
            {
                new HttpHeader("name", value)
            };
            var actual = HttpHeaderCollection.FromString("name: " + value);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_withLeadingWhiteSpace()
        {
            var expected = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };
            var actual = HttpHeaderCollection.FromString("name:         value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                item
            };

            using (var enumerator = obj.GetEnumerator())
            {
                enumerator.MoveNext();
                Assert.Same(item, enumerator.Current);
            }
        }

        [Fact]
        public void op_GetHashCode()
        {
            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };

            var expected = obj.ToString().GetHashCode();
            var actual = obj.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Read_TextReader()
        {
            HttpMessage message = new DerivedHttpMessage();
            HttpHeader connection = "Connection: close";

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(connection);
                writer.WriteLine(string.Empty);
                writer.WriteLine("body");
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    message.Read(reader);
                }
            }

            Assert.Equal(1, message.Headers.Count);
            Assert.True(message.Headers.Contains(connection));
        }

        [Fact]
        public void op_Read_TextReaderEmpty()
        {
            var obj = new HttpHeaderCollection();

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    obj.Read(reader);
                }
            }

            Assert.Empty(obj);
        }

        [Fact]
        public void op_Read_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderCollection().Read(null));
        }

        [Fact]
        public void op_Remove_IHttpHeader()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                item
            };

            obj.Remove(item);

            Assert.Equal(0, obj.Count);
        }

        [Fact]
        public void op_ToString()
        {
            var expected = new StringBuilder();
            expected.AppendLine("name: value");

            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "value")
            };

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenCaseSensitive()
        {
            var expected = new StringBuilder();
            expected.AppendLine("name: foo");
            expected.AppendLine("Name: bar");

            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "foo"),
                new HttpHeader("Name", "bar")
            };

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenEmpty()
        {
            Assert.Empty(new HttpHeaderCollection().ToString());
        }

        [Fact]
        public void op_ToString_whenMultiple()
        {
            var expected = new StringBuilder();
            expected.AppendLine("name: foo, bar");

            var obj = new HttpHeaderCollection
            {
                new HttpHeader("name", "foo"),
                new HttpHeader("name", "bar")
            };

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_Write_TextWriter()
        {
            var headers = new HttpHeaderCollection
            {
                new HttpHeader("Connection", "close")
            };

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                headers.Write(writer);
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    Assert.Equal("Connection: close" + Environment.NewLine, reader.ReadToEnd());
                }
            }
        }

        [Fact]
        public void op_Write_TextWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderCollection().Write(null));
        }

        [Fact]
        public void op_Write_TextWriter_whenEmpty()
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                new HttpHeaderCollection().Write(writer);
                writer.Flush();
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    Assert.Equal(string.Empty, reader.ReadToEnd());
                }
            }
        }

        [Fact]
        public void prop_ContentType()
        {
            Assert.True(new PropertyExpectations<HttpHeaderCollection>(p => p.ContentType)
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
                new HttpHeader("Content-Type", "text/plain")
            };

            Assert.Equal("text/plain", obj.ContentType.MediaType);
        }

        [Fact]
        public void prop_Count()
        {
            Assert.True(new PropertyExpectations<HttpHeaderCollection>(p => p.Count)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsReadOnly()
        {
            Assert.True(new PropertyExpectations<HttpHeaderCollection>(p => p.IsReadOnly)
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .IsNotDecorated()
                            .Result);
        }
    }
}