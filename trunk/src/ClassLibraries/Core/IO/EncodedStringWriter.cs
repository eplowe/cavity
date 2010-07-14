namespace Cavity.IO
{
    using System;
    using System.IO;
    using System.Text;

    public sealed class EncodedStringWriter : StringWriter
    {
        private readonly Encoding _encoding;

        public EncodedStringWriter(StringBuilder builder, IFormatProvider formatProvider, Encoding encoding)
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