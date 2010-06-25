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
            this.ContentType = new ContentType("text/plain");
        }

        public TextPlain(string content)
            : this()
        {
            this.Content = content;
        }

        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Caching 'as' result prevents differentiating null from unexpected type.")]
        public object Content
        {
            get
            {
                return this.Value;
            }

            set
            {
                if (null == value || value is string)
                {
                    this.Value = value as string;
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
                return this._contentType;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._contentType = value;
            }
        }

        public string Value
        {
            get;
            set;
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

        public IContent ToContent(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            return new TextPlain(reader.ReadToEnd());
        }

        public void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            writer.Write(this.Content as string);
        }

        public override string ToString()
        {
            return this.Content as string;
        }
    }
}