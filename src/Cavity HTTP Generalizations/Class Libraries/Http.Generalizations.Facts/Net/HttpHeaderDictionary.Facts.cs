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
        public void op_Contains_Token()
        {
            var obj = new HttpHeaderDictionary();

            Assert.False(obj.Contains("name"));

            obj.Add(new HttpHeader("name", "value"));

            Assert.True(obj.Contains("name"));
        }

        [Fact]
        public void op_Contains_TokenNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeaderDictionary().Contains(null));
        }

        [Fact]
        public void op_FromString_string()
        {
            HttpHeader expected = "name: value";

            var obj = HttpHeaderDictionary.FromString(expected);

            var actual = obj.First<HttpHeader>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Empty(HttpHeaderDictionary.FromString(string.Empty).ToList<HttpHeader>());
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

            Assert.Equal(host, obj.First<HttpHeader>());
            Assert.Equal(ua, obj.Last<HttpHeader>());
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
    }
}