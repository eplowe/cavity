namespace Cavity.Globalization
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [Serializable]
    public struct Language : ISerializable, 
                             IEquatable<Language>
    {
        public Language(string value)
            : this()
        {
            Value = value;
        }

        private Language(SerializationInfo info, 
                         StreamingContext context)
            : this()
        {
            Value = info.GetString("_value");
        }

        private string Value { get; set; }

        public static bool operator ==(Language obj, 
                                       Language comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator !=(Language obj, 
                                       Language comparand)
        {
            return !obj.Equals(comparand);
        }

        public static implicit operator CultureInfo(Language value)
        {
            return value.ToCultureInfo();
        }

        public static implicit operator Language(CultureInfo value)
        {
            return null == value
                       ? new Language()
                       : new Language(value.Name);
        }

        public static implicit operator string(Language value)
        {
            return value.ToString();
        }

        public static implicit operator Language(string value)
        {
            return new Language(value);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((Language)obj);
        }

        public bool Equals(Language other)
        {
            return ToString() == other.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public CultureInfo ToCultureInfo()
        {
            return new CultureInfo(ToString());
        }

        public override string ToString()
        {
            return Value ?? string.Empty;
        }

#if NET20 || NET35
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
#endif

        void ISerializable.GetObjectData(SerializationInfo info, 
                                         StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("_value", ToString());
        }
    }
}