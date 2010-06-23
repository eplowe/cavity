namespace Cavity.Net.Mime
{
    using System;
    using System.IO;
    using System.Net.Mime;

    public sealed class TextPlain : Text, IContent, IMediaType
    {
        public TextPlain()
            : this(null as string)
        {
        }

        public TextPlain(string value)
            : base(new ContentType("text/plain"), value)
        {
        }

        public static implicit operator TextPlain(string value)
        {
            return object.ReferenceEquals(null, value) ? null as TextPlain : TextPlain.Parse(value);
        }

        public static TextPlain Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return new TextPlain(value);
        }

        public IContent ToBody(StreamReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            return new TextPlain(reader.EndOfStream ? null as string : reader.ReadToEnd());
        }

        public void Write(StreamWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            throw new NotSupportedException();
        }
    }
}