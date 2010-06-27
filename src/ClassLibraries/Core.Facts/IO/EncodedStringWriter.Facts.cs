namespace Cavity.IO
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Xunit;

    public sealed class EncodedStringWriterFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<EncodedStringWriter>()
                .DerivesFrom<StringWriter>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor_StringBuilder_IFormatProvider_Encoding()
        {
            using (EncodedStringWriter obj = new EncodedStringWriter(new StringBuilder(), CultureInfo.InvariantCulture, Encoding.UTF8))
            {
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void prop_Encoding_get()
        {
            Encoding expected = Encoding.UTF8;
            Encoding actual = null;

            using (EncodedStringWriter obj = new EncodedStringWriter(new StringBuilder(), CultureInfo.InvariantCulture, expected))
            {
                actual = obj.Encoding;
            }

            Assert.Equal<Encoding>(expected, actual);
        }
    }
}