namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;

    public class EncodedStringWriter : StringWriter
    {
        private readonly Encoding _encoding;

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor", Justification = "Providing a mirror of all the StringWriter constructors.")]
        public EncodedStringWriter(Encoding encoding)
        {
            _encoding = encoding;
        }

        public EncodedStringWriter(IFormatProvider formatProvider, 
                                   Encoding encoding)
            : base(formatProvider)
        {
            _encoding = encoding;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor(System.Text.StringBuilder)", Justification = "Providing a mirror of all the StringWriter constructors.")]
        public EncodedStringWriter(StringBuilder builder, 
                                   Encoding encoding)
            : base(builder)
        {
            _encoding = encoding;
        }

        public EncodedStringWriter(StringBuilder builder, 
                                   IFormatProvider formatProvider, 
                                   Encoding encoding)
            : base(builder, formatProvider)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return _encoding;
            }
        }
    }
}