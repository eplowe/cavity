namespace Cavity.Security.Cryptography
{
    using System;
    using System.Security.Cryptography;

    public sealed class Salt
    {
        private readonly byte[] _bytes;

        public Salt()
            : this(10)
        {
        }

        public Salt(int size)
        {
            if (1 > size)
            {
                throw new ArgumentOutOfRangeException("size");
            }

            _bytes = new byte[size];
            new RNGCryptoServiceProvider().GetNonZeroBytes(_bytes);
        }

        public Salt(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            _bytes = Convert.FromBase64String(value);
        }

        public static bool operator ==(Salt obj, 
                                       Salt comparand)
        {
            return ReferenceEquals(null, obj)
                       ? ReferenceEquals(null, comparand)
                       : obj.Equals(comparand);
        }

        public static implicit operator string(Salt salt)
        {
            return null == salt ? null : salt.ToString();
        }

        public static implicit operator Salt(string value)
        {
            return null == value ? null : new Salt(value);
        }

        public static bool operator !=(Salt obj, 
                                       Salt comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : !obj.Equals(comparand);
        }

        public override bool Equals(object obj)
        {
            var result = false;

            if (!ReferenceEquals(null, obj))
            {
                if (ReferenceEquals(this, obj))
                {
                    result = true;
                }
                else
                {
                    var cast = obj as Salt;
                    if (!ReferenceEquals(null, cast))
                    {
                        result = 0 == string.CompareOrdinal(this, cast);
                    }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public byte[] ToBytes()
        {
            return _bytes;
        }

        public override string ToString()
        {
            return Convert.ToBase64String(_bytes);
        }
    }
}