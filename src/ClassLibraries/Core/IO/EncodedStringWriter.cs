namespace Cavity.IO
{
    using System;
    using System.IO;
    using System.Text;

    public sealed class EncodedStringWriter : StringWriter
    {
        private Encoding _encoding;

        public EncodedStringWriter(StringBuilder builder, IFormatProvider formatProvider, Encoding encoding)
            : base(builder, formatProvider)
        {
            this._encoding = encoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return this._encoding;
            }
        }
    }
}