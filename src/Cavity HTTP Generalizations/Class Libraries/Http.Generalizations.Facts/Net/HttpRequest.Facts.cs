namespace Cavity.Net
{
    using System;
    using System.Linq;
    using System.Text;

    using Xunit;

    public sealed class HttpRequestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpRequest>()
                            .DerivesFrom<HttpMessage>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpRequest());
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpRequest.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpRequest.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenHeaders()
        {
            const string line = "GET http://example.com/ HTTP/1.1";
            HttpHeader host = "Host: example.com";
            HttpHeader ua = "User-Agent: Example";

            var buffer = new StringBuilder();
            buffer.AppendLine(line);
            buffer.AppendLine(host);
            buffer.AppendLine(ua);

            var obj = HttpRequest.FromString(buffer.ToString());

            Assert.Equal(line, obj.Line);
            Assert.Equal(host, obj.Headers.List.First());
            Assert.Equal(ua, obj.Headers.List.Last());
            Assert.Null(obj.Body);
        }

        [Fact]
        public void op_FromString_string_whenHeadersAndBody()
        {
            const string line = "PUT http://example.com/ HTTP/1.1";
            HttpHeader host = "Host: example.com";
            HttpHeader type = "Content-Type: text/plain";

            var buffer = new StringBuilder();
            buffer.AppendLine(line);
            buffer.AppendLine(host);
            buffer.AppendLine(type);
            buffer.AppendLine(string.Empty);
            buffer.Append("example");

            var obj = HttpRequest.FromString(buffer.ToString());

            Assert.Equal(line, obj.Line);
            Assert.Equal(host, obj.Headers.List.First());
            Assert.Equal(type, obj.Headers.List.Last());

            Assert.Equal("example", ((TextBody)obj.Body).Text);
        }

        [Fact]
        public void op_FromString_string_whenRequestLine()
        {
            const string line = "GET http://example.com/ HTTP/1.1";

            var obj = HttpRequest.FromString(line);

            Assert.Equal(line, obj.Line);
            Assert.Empty(obj.Headers.List);
            Assert.Null(obj.Body);
        }

        [Fact]
        public void prop_Line()
        {
            Assert.True(new PropertyExpectations<HttpRequest>(x => x.Line)
                            .IsAutoProperty<HttpRequestLine>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}