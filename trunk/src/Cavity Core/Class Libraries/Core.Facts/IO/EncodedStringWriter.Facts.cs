namespace Cavity.IO
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Text;

    using Xunit;

    public sealed class EncodedStringWriterFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<EncodedStringWriter>()
                            .DerivesFrom<StringWriter>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "Cavity.IO.EncodedStringWriter.#ctor(System.Text.Encoding)", Justification = "Providing a mirror of all the StringWriter constructors.")]
        [Fact]
        public void ctor_Encoding()
        {
            using (var obj = new EncodedStringWriter(Encoding.UTF8))
            {
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void ctor_IFormatProvider_Encoding()
        {
            using (var obj = new EncodedStringWriter(CultureInfo.InvariantCulture, Encoding.UTF8))
            {
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void ctor_StringBuilder_Encoding()
        {
            using (var obj = new EncodedStringWriter(new StringBuilder(), Encoding.UTF8))
            {
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void ctor_StringBuilder_IFormatProvider_Encoding()
        {
            using (var obj = new EncodedStringWriter(new StringBuilder(), CultureInfo.InvariantCulture, Encoding.UTF8))
            {
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void prop_Encoding_get()
        {
            var expected = Encoding.UTF8;
            Encoding actual;

            using (var obj = new EncodedStringWriter(new StringBuilder(), CultureInfo.InvariantCulture, expected))
            {
                actual = obj.Encoding;
            }

            Assert.Equal(expected, actual);
        }
    }
}