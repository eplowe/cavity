namespace Cavity.Net.Mime
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mime;

    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "This naming is intentional.")]
    public abstract class Text : ComparableObject
    {
        private ContentType _contentType;
        private string _value;

        protected Text(ContentType contentType, string value)
            : this()
        {
            this.ContentType = contentType;
            this.Value = value;
        }

        protected Text()
        {
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is protected rather than private for testability.")]
        public ContentType ContentType
        {
            get
            {
                return this._contentType;
            }

            protected set
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
            get
            {
                return this._value;
            }

            protected set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._value = value;
            }
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}