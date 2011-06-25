namespace Cavity
{
    using System;
    using System.Runtime.Serialization;
#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [Serializable]
    public class RelativeUri : IComparable, IComparable<RelativeUri>, IEquatable<RelativeUri>, ISerializable
    {
        private Uri _value;

        public RelativeUri(string value)
            : this(null == value ? null : new Uri(value, UriKind.RelativeOrAbsolute))
        {
        }

        public RelativeUri(Uri value)
        {
            Value = value;
        }

        protected RelativeUri(SerializationInfo info,
                              StreamingContext context)
        {
            if (null != info)
            {
                _value = new Uri(info.GetString("_value"), UriKind.Relative);
            }
        }

        public int Length
        {
            get
            {
                return ToString().Length;
            }
        }

        protected Uri Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                if (value.IsAbsoluteUri)
                {
                    throw new UriFormatException();
                }

                _value = value;
            }
        }

        public static bool operator ==(RelativeUri obj,
                                       RelativeUri comparand)
        {
            return ReferenceEquals(null, obj)
                       ? ReferenceEquals(null, comparand)
                       : obj.Equals(comparand);
        }

        public static bool operator >(RelativeUri obj,
                                      RelativeUri comparand)
        {
            return ReferenceEquals(null, obj)
                       ? false
                       : 0 < obj.CompareTo(comparand);
        }

        public static implicit operator string(RelativeUri uri)
        {
            return (null == uri) ? null : uri.Value.OriginalString;
        }

        public static implicit operator RelativeUri(string value)
        {
            return (null == value) ? null : new Uri(value, UriKind.Relative);
        }

        public static implicit operator Uri(RelativeUri uri)
        {
            return (null == uri) ? null : uri.Value;
        }

        public static implicit operator RelativeUri(Uri value)
        {
            return (null == value) ? null : new RelativeUri(value);
        }

        public static bool operator !=(RelativeUri obj,
                                       RelativeUri comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : !obj.Equals(comparand);
        }

        public static bool operator <(RelativeUri obj,
                                      RelativeUri comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : 0 > obj.CompareTo(comparand);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var cast = obj as RelativeUri;
            if (ReferenceEquals(null, cast))
            {
                return false;
            }

            return Value == cast.Value;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Value.OriginalString;
        }

        public virtual int CompareTo(object obj)
        {
            if (null == obj)
            {
                return 1;
            }

            var cast = obj as RelativeUri;
            if (null == cast)
            {
                throw new ArgumentOutOfRangeException("obj");
            }

            return string.CompareOrdinal(Value.OriginalString, cast.Value.OriginalString);
        }

        public virtual int CompareTo(RelativeUri other)
        {
            return null == other
                       ? 1
                       : string.CompareOrdinal(Value.OriginalString, other.Value.OriginalString);
        }

        public virtual bool Equals(RelativeUri other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                       ? true
                       : 0 == CompareTo(other);
        }

#if NET20 || NET35
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
#endif

        public virtual void GetObjectData(SerializationInfo info,
                                          StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("_value", _value.OriginalString);
        }
    }
}