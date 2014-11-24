namespace Cavity.Security.Cryptography
{
    using System;
    using System.ComponentModel;
    using System.Security.Cryptography;
    using System.Text;

    [ImmutableObject(true)]
    public sealed class SecureHash
    {
        private readonly byte[] _bytes;

        public SecureHash(string ciphertext)
        {
            if (null == ciphertext)
            {
                throw new ArgumentNullException("ciphertext");
            }

            if (0 == ciphertext.Length)
            {
                throw new ArgumentOutOfRangeException("ciphertext");
            }

            _bytes = Convert.FromBase64String(ciphertext);
        }

        private SecureHash(byte[] bytes)
        {
            _bytes = bytes;
        }

        public static bool operator ==(SecureHash obj,
                                       SecureHash comparand)
        {
            return ReferenceEquals(null, obj)
                       ? ReferenceEquals(null, comparand)
                       : obj.Equals(comparand);
        }

        public static implicit operator string(SecureHash salt)
        {
            return null == salt ? null : salt.ToString();
        }

        public static implicit operator SecureHash(string value)
        {
            return null == value ? null : new SecureHash(value);
        }

        public static bool operator !=(SecureHash obj,
                                       SecureHash comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : !obj.Equals(comparand);
        }

        public static SecureHash Encrypt(string plaintext,
                                         Salt salt)
        {
            if (null == plaintext)
            {
                throw new ArgumentNullException("plaintext");
            }

            if (null == salt)
            {
                throw new ArgumentNullException("salt");
            }

            var bytes = new byte[plaintext.Length + salt.ToBytes().Length];
            Encoding.UTF8.GetBytes(plaintext).CopyTo(bytes, 0);
            salt.ToBytes().CopyTo(bytes, plaintext.Length);

            SecureHash result;
            using (HashAlgorithm algorithm = new SHA256Managed())
            {
                result = new SecureHash(algorithm.ComputeHash(bytes));
            }

            return result;
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
                    var cast = obj as SecureHash;
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

        public override string ToString()
        {
            return Convert.ToBase64String(_bytes);
        }
    }
}