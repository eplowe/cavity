namespace Cavity.Net
{
    using System;
    using System.Linq;
    using System.Text;
    using Xunit;

    public sealed class HttpResponseFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpResponse>()
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
            Assert.NotNull(new HttpResponse());
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpResponse.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpResponse.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenHeaders()
        {
            const string line = "HTTP/1.1 204 No Content";
            HttpHeader control = "Cache-Control: no-cache";
            HttpHeader length = "Content-Length: 0";

            var buffer = new StringBuilder();
            buffer.AppendLine(line);
            buffer.AppendLine(control);
            buffer.AppendLine(length);

            var obj = HttpResponse.FromString(buffer.ToString());

            Assert.Equal(line, obj.Line);
            Assert.Equal(control, obj.Headers.List.First());
            Assert.Equal(length, obj.Headers.List.Last());
            Assert.Null(obj.Body);
        }

        [Fact]
        public void op_FromString_string_whenHeadersAndBody()
        {
            const string line = "HTTP/1.1 200 OK";
            HttpHeader type = "Content-Type: text/plain";
            HttpHeader length = "Content-Length: 7";

            var buffer = new StringBuilder();
            buffer.AppendLine(line);
            buffer.AppendLine(type);
            buffer.AppendLine(length);
            buffer.AppendLine(string.Empty);
            buffer.Append("example");

            var obj = HttpResponse.FromString(buffer.ToString());

            Assert.Equal(line, obj.Line);
            Assert.Equal(type, obj.Headers.List.First());
            Assert.Equal(length, obj.Headers.List.Last());

            Assert.Equal("example", ((TextBody)obj.Body).Text);
        }

        [Fact]
        public void op_FromString_string_whenStatusLine()
        {
            const string line = "HTTP/1.1 201 Created";

            var obj = HttpResponse.FromString(line);

            Assert.Equal(line, obj.Line);
            Assert.Empty(obj.Headers.List);
            Assert.Null(obj.Body);
        }

        [Fact]
        public void prop_Line()
        {
            Assert.True(new PropertyExpectations<HttpResponse>(x => x.Line)
                            .IsAutoProperty<HttpStatusLine>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}