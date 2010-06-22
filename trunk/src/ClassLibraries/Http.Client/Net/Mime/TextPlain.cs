namespace Cavity.Net.Mime
{
    using System;
    using System.IO;
    using System.Net.Mime;

    public sealed class TextPlain : ComparableObject, IMediaType, IContent
    {
        private string _value;

        public TextPlain()
            : this(string.Empty)
        {
        }

        public TextPlain(string value)
        {
            this.Value = value;
        }

        public ContentType ContentType
        {
            get
            {
                return new ContentType("text/plain");
            }
        }

        public string Value
        {
            get
            {
                return this._value;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._value = value;
            }
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

            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return this.Value;
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