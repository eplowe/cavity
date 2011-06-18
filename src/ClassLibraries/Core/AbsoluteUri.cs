namespace Cavity
{
    using System;
#if !NET20
    using System.IO;
#endif
    using System.Runtime.Serialization;
#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [Serializable]
    public class AbsoluteUri : IComparable, IComparable<AbsoluteUri>, IEquatable<AbsoluteUri>, ISerializable
    {
        private Uri _value;

        public AbsoluteUri(string value)
            : this(null == value ? null : new Uri(value, UriKind.Absolute))
        {
        }

        public AbsoluteUri(Uri value)
        {
            Value = value;
        }

        protected AbsoluteUri(SerializationInfo info,
                            StreamingContext context)
        {
            if (null != info)
            {
                _value = new Uri(info.GetString("_value"), UriKind.Absolute);
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

                if (!value.IsAbsoluteUri)
                {
                    throw new UriFormatException();
                }

                _value = value;
            }
        }

        public static bool operator ==(AbsoluteUri obj,
                                       AbsoluteUri comparand)
        {
            return ReferenceEquals(null, obj)
                       ? ReferenceEquals(null, comparand)
                       : obj.Equals(comparand);
        }

        public static bool operator >(AbsoluteUri obj,
                                      AbsoluteUri comparand)
        {
            return ReferenceEquals(null, obj)
                       ? false
                       : 0 < obj.CompareTo(comparand);
        }

        public static implicit operator string(AbsoluteUri uri)
        {
            return (null == uri) ? null : uri.Value.AbsoluteUri;
        }

        public static implicit operator AbsoluteUri(string value)
        {
            return (null == value) ? null : new Uri(value, UriKind.Absolute);
        }

        public static implicit operator Uri(AbsoluteUri uri)
        {
            return (null == uri) ? null : uri.Value;
        }

        public static implicit operator AbsoluteUri(Uri value)
        {
            return (null == value) ? null : new AbsoluteUri(value);
        }

        public static bool operator !=(AbsoluteUri obj,
                                       AbsoluteUri comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : !obj.Equals(comparand);
        }

        public static bool operator <(AbsoluteUri obj,
                                      AbsoluteUri comparand)
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

            var cast = obj as AbsoluteUri;
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
            return Value.AbsoluteUri;
        }

#if !NET20
        public virtual FileSystemInfo ToPath(FileSystemInfo root)
        {
            if (null == root)
            {
                throw new ArgumentNullException("root");
            }

            var path = ToString().Replace("%5C", "&bsol;");
            if (path.Contains("://"))
            {
                path = path.Replace("://", @"\");
            }
            else
            {
                var index = path.IndexOf(':');
                path = @"{0}\{1}".FormatWith(path.Substring(0, index), path.Substring(index + 1));
            }

            path = path
                .Replace('/', '\\')
                .Replace("*", "&ast;")
                .Replace(":", "&colon;")
                .Replace("%3E", "&gt;")
                .Replace("%3C", "&lt;")
                .Replace("?", "&quest;")
                .Replace("%22", "&quot;")
                .Replace("%7C", "&verbar;");

            return new DirectoryInfo(Path.Combine(root.FullName, path));
        }
#endif

        public virtual int CompareTo(object obj)
        {
            if (null == obj)
            {
                return 1;
            }

            var cast = obj as AbsoluteUri;
            if (null == cast)
            {
                throw new ArgumentOutOfRangeException("obj");
            }

            return string.CompareOrdinal(Value.AbsoluteUri, cast.Value.AbsoluteUri);
        }

        public virtual int CompareTo(AbsoluteUri other)
        {
            return null == other
                       ? 1
                       : string.CompareOrdinal(Value.AbsoluteUri, other.Value.AbsoluteUri);
        }

        public virtual bool Equals(AbsoluteUri other)
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

            info.AddValue("_value", _value.AbsoluteUri);
        }
    }
}