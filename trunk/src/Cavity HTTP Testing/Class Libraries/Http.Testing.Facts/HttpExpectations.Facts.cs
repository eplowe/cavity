namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Xunit;
    using Xunit.Extensions;

    public sealed class HttpExpectationsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpExpectations>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IHttpExpectations>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpExpectations());
        }

        [Fact]
        public void ctor_IEnumerableOfHttpExpectation()
        {
            Assert.NotNull(new HttpExpectations(new List<HttpExpectation>()));
        }

        [Fact]
        public void ctor_IEnumerableOfHttpExpectationNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpExpectations(null));
        }

        [Theory]
        [InlineData("example.crlf.http")]
        [InlineData("example.lf.http")]
        public void op_Load_FileInfo(string name)
        {
            Assert.True(HttpExpectations.Load(new FileInfo(name)).Result);
        }

        [Fact]
        public void op_Load_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.Load(null as FileInfo));
        }

        [Fact]
        public void op_Load_string()
        {
            var buffer = new StringBuilder();
            buffer.AppendLine(">request>");
            buffer.AppendLine("GET http://example.com/ HTTP/1.1");
            buffer.AppendLine("<response<");
            buffer.AppendLine("HTTP/1.1 200 OK");
            buffer.AppendLine("<<");
            buffer.AppendLine(string.Empty);
            buffer.AppendLine("# a comment");
            buffer.AppendLine(string.Empty);
            buffer.AppendLine(">request>");
            buffer.AppendLine("GET http://example.net/ HTTP/1.1");
            buffer.AppendLine("<response<");
            buffer.AppendLine("HTTP/1.1 303 See Other");
            buffer.AppendLine("Location: http://example.net/index.html");
            buffer.AppendLine("<<");

            var obj = HttpExpectations.Load(buffer.ToString());

            Assert.IsAssignableFrom<IHttpExpectations>(obj);

            var first = obj.First();
            Assert.Equal("GET http://example.com/ HTTP/1.1", first.Exchange.Request.Line);
            Assert.Equal("HTTP/1.1 200 OK", first.Exchange.Response.Line);

            var last = obj.Last();
            Assert.Equal("GET http://example.net/ HTTP/1.1", last.Exchange.Request.Line);
            Assert.Equal("HTTP/1.1 303 See Other", last.Exchange.Response.Line);
            Assert.Equal("http://example.net/index.html", last.Exchange.Response.Headers["Location"]);
        }

        [Fact]
        public void op_Load_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpExpectations.Load(string.Empty));
        }

        [Fact]
        public void op_Load_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.Load(null as string));
        }
    }
}