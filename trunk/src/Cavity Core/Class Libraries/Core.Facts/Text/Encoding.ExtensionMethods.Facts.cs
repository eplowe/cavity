namespace Cavity.Text
{
    using System;
    using System.Net.Mime;
    using System.Text;

    using Xunit;

    public sealed class EncodingExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(EncodingExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_ToContentType_EncodingDefault_string()
        {
            var expected = new ContentType("text/example; charset=Windows-1252");
            var actual = Encoding.Default.ToContentType("text/example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToContentType_EncodingNull_string()
        {
            var expected = new ContentType("text/example");
            var actual = (null as Encoding).ToContentType("text/example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToContentType_Encoding_string()
        {
            var expected = new ContentType("text/example; charset=utf-8");
            var actual = Encoding.UTF8.ToContentType("text/example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToContentType_Encoding_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Encoding.UTF8.ToContentType(string.Empty));
        }

        [Fact]
        public void op_ToContentType_Encoding_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Encoding.UTF8.ToContentType(null));
        }
    }
}