namespace Cavity.Net
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml;

#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [Serializable]
    public struct Quality : ISerializable,
                            IEquatable<Quality>
    {
        private float _value;

        public Quality(float value)
            : this()
        {
            Value = value;
        }

        private Quality(SerializationInfo info,
                        StreamingContext context)
            : this()
        {
            Value = info.GetSingle("_value");
        }

        public static Quality One
        {
            get
            {
                return new Quality(1);
            }
        }

        public static Quality Zero
        {
            get
            {
                return new Quality(1);
            }
        }

        public float Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (0 > value)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                if (1 < value)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                var s = XmlConvert.ToString(value);

                _value = XmlConvert.ToSingle(5 < s.Length ? s.Substring(0, 5) : s);
            }
        }

        public static bool operator ==(Quality obj,
                                       Quality comparand)
        {
            return obj.Equals(comparand);
        }

        public static implicit operator float(Quality value)
        {
            return value.Value;
        }

        public static implicit operator Quality(float value)
        {
            return new Quality(value);
        }

        public static implicit operator string(Quality value)
        {
            return value.ToString();
        }

        public static implicit operator Quality(string value)
        {
            return FromString(value);
        }

        public static bool operator !=(Quality obj,
                                       Quality comparand)
        {
            return !obj.Equals(comparand);
        }

        public static Quality FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            value = value.Trim();
            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return new Quality(XmlConvert.ToSingle(value));
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((Quality)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return XmlConvert.ToString(Value);
        }

        public bool Equals(Quality other)
        {
            return ToString() == other.ToString();
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

            info.AddValue("_value", Value);
        }
    }
}