namespace Cavity.Text
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net.Mime;
    using Cavity.Net.Mime;

    public sealed class TextPlain : ComparableObject, IContent, IMediaType
    {
        private ContentType _contentType;

        public TextPlain()
        {
            ContentType = new ContentType("text/plain");
        }

        public TextPlain(string content)
            : this()
        {
            Content = content;
        }

        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Caching 'as' result prevents differentiating null from unexpected type.")]
        public object Content
        {
            get
            {
                return Value;
            }

            set
            {
                if (null == value ||
                    value is string)
                {
                    Value = value as string;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value");
                }
            }
        }

        public ContentType ContentType
        {
            get
            {
                return _contentType;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _contentType = value;
            }
        }

        public string Value { get; set; }

        public static implicit operator TextPlain(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
        }

        public static TextPlain FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return new TextPlain(value);
        }

        public override string ToString()
        {
            return Content as string ?? string.Empty;
        }

        public void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            writer.Write(Content as string);
        }

        public IContent ToContent(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            return new TextPlain(reader.ReadToEnd());
        }
    }
}