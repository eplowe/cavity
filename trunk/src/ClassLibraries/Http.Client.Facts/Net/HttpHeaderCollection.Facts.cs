namespace Cavity.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mime;
    using System.Text;
    using Cavity;
    using Cavity.Net.Mime;
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
        public void op_ContainsName_Token_whenTrue()
        {
            var item = new HttpHeader("name", "value");
            var obj = new HttpHeaderCollection
            {
                { item }
            };

            Assert.True(obj.ContainsName("name"));
        }

        [Fact]
        public void op_ContainsName_Token_whenFalse()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("foo", "value") }
            };

            Assert.False(obj.ContainsName("bar"));
        }

        [Fact]
        public void op_ContainsName_TokenNull()
        {
            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") }
            };

            Assert.Throws<ArgumentNullException>(() => obj.ContainsName(null as string));
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
        public void op_Read_StreamReader()
        {
            HttpMessage message = new DerivedHttpMessage();
            HttpHeader connection = "Connection: close";

            using (var stream = new MemoryStream())
            {
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
            }

            Assert.Equal<int>(1, message.Headers.Count);
            Assert.True(message.Headers.Contains(connection));
        }

        [Fact]
        public void op_Read_StreamReaderEmpty()
        {
            var obj = new HttpHeaderCollection();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        obj.Read(reader);
                    }
                }
            }

            Assert.Empty(obj);
        }

        [Fact]
        public void op_Read_StreamReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderCollection().Read(null as StreamReader));
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
        public void op_ToString()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("name: value");

            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "value") }
            };

            string actual = obj.ToString();

            Assert.Equal<string>(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenCaseSensitive()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("name: foo");
            expected.AppendLine("Name: bar");

            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") },
                { new HttpHeader("Name", "bar") }
            };

            string actual = obj.ToString();

            Assert.Equal<string>(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToDictionary_whenEmpty()
        {
            Assert.Empty(new HttpHeaderCollection().ToString());
        }

        [Fact]
        public void op_ToDictionary_whenMultiple()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("name: foo, bar");

            var obj = new HttpHeaderCollection
            {
                { new HttpHeader("name", "foo") },
                { new HttpHeader("name", "bar") }
            };

            string actual = obj.ToString();

            Assert.Equal<string>(expected.ToString(), actual);
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