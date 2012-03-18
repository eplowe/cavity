namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Xunit;

    public sealed class HttpHeaderDictionaryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpHeaderDictionary>()
                            .DerivesFrom<Dictionary<Token, string>>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Implements<IHttpMessageHeaders>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpHeaderDictionary());
        }

        [Fact]
        public void op_Add_HttpHeader()
        {
            var expected = new HttpHeader("name", "value");

            var obj = new HttpHeaderDictionary
                          {
                              expected
                          };

            Assert.Equal("value", obj["name"]);
        }

        [Fact]
        public void op_Add_HttpHeaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderDictionary().Add(null));
        }

        [Fact]
        public void op_FromString_string()
        {
            HttpHeader expected = "name: value";

            var obj = HttpHeaderDictionary.FromString(expected);

            var actual = obj.List.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Empty(HttpHeaderDictionary.FromString(string.Empty).List);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderDictionary.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenMultiple()
        {
            HttpHeader host = "Host: example.com";
            HttpHeader ua = "User-Agent: Example";

            var buffer = new StringBuilder();
            buffer.AppendLine(host);
            buffer.AppendLine(ua);

            var obj = HttpHeaderDictionary.FromString(buffer.ToString());

            Assert.Equal(host, obj.List.First());
            Assert.Equal(ua, obj.List.Last());
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var expected = new KeyValuePair<Token, string>("name", "value");

            var obj = new HttpHeaderDictionary
                          {
                              {
                                  "name", "value"
                                  }
                          };

            foreach (var actual in obj)
            {
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_HttpHeaderDictionary_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeaderDictionary.FromString(null));
        }

        [Fact]
        public void prop_List()
        {
            Assert.True(new PropertyExpectations<HttpHeaderDictionary>(p => p.List)
                            .TypeIs<IEnumerable<HttpHeader>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_List_get()
        {
            var expected = new HttpHeader("name", "value");

            var obj = new HttpHeaderDictionary
                          {
                              {
                                  "name", "value"
                                  }
                          };

            foreach (var actual in obj.List)
            {
                Assert.Equal(expected, actual);
            }
        }
    }
}