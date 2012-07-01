namespace Cavity.Net
{
    using System;
    using System.ComponentModel;
#if !NET20
    using System.Linq;
#endif
    using System.Runtime.Serialization;
#if NET20 || NET35
    using System.Security.Permissions;
#endif

    using Cavity.Properties;

    [ImmutableObject(true)]
    [Serializable]
    public class Token : IComparable, 
                         IComparable<Token>, 
                         IEquatable<Token>, 
                         ISerializable
    {
        private static readonly char[] _illegal = new[] { '"', '@', '(', ')', ',', '/', ':', ';', '<', '=', '>', '?', '[', '\\', ']', '{', '}' };

        private string _value;

        public Token(string value)
            : this()
        {
            Value = value;
        }

        protected Token(SerializationInfo info, 
                        StreamingContext context)
        {
            if (null != info)
            {
                _value = info.GetString("_value");
            }
        }

        private Token()
        {
        }

        public string Value
        {
            get
            {
                return _value;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                if (0 == value.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

#if NET20
                foreach (var c in value.ToUpperInvariant())
                {
                    if (' ' >= c || GenericExtensionMethods.EqualsOneOf(c, _illegal) || (char)127 <= c)
                    {
                        throw new FormatException(Resources.Token_FormatException_Message);
                    }
                }
#else
                if (value
                    .ToUpperInvariant()
                    .Any(c => ' ' >= c || c.EqualsOneOf(_illegal) || (char)127 <= c))
                {
                    throw new FormatException(Resources.Token_FormatException_Message);
                }
#endif

                _value = value;
            }
        }

        public static bool operator ==(Token obj, 
                                       Token comparand)
        {
            return ReferenceEquals(null, obj)
                       ? ReferenceEquals(null, comparand)
                       : obj.Equals(comparand, StringComparison.Ordinal);
        }

        public static bool operator >(Token obj, 
                                      Token comparand)
        {
            return !ReferenceEquals(null, obj) && 0 < obj.CompareTo(comparand, StringComparison.Ordinal);
        }

        public static implicit operator string(Token uri)
        {
            return (null == uri) ? null : uri.Value;
        }

        public static implicit operator Token(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : new Token(value);
        }

        public static bool operator !=(Token obj, 
                                       Token comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : !obj.Equals(comparand, StringComparison.Ordinal);
        }

        public static bool operator <(Token obj, 
                                      Token comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : 0 > obj.CompareTo(comparand, StringComparison.Ordinal);
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

            var cast = obj as Token;
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
            return Value;
        }

        public virtual int CompareTo(object obj)
        {
            if (null == obj)
            {
                return 1;
            }

            var cast = obj as Token;
            if (null == cast)
            {
                throw new ArgumentOutOfRangeException("obj");
            }

            return string.CompareOrdinal(Value, cast.Value);
        }

        public virtual int CompareTo(Token other)
        {
            return CompareTo(other, StringComparison.Ordinal);
        }

        public virtual int CompareTo(Token other, 
                                     StringComparison comparison)
        {
            return null == other
                       ? 1
                       : string.Compare(Value, other.Value, comparison);
        }

        public virtual bool Equals(Token other)
        {
            return Equals(other, StringComparison.Ordinal);
        }

        public virtual bool Equals(Token other, 
                                   StringComparison comparison)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || 0 == CompareTo(other, comparison);
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

            info.AddValue("_value", _value);
        }
    }
}