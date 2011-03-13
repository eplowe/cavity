namespace Cavity.Net
{
    using System;
    using System.Runtime.Serialization;
#if NET20 || NET35
    using System.Security.Permissions;
#endif
    using Cavity.Properties;

    /// <summary>
    /// Represents an entity tag.
    /// </summary>
    /// <remarks>
    /// <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.19">ETag</see>
    /// </remarks>
    [Serializable]
    public struct EntityTag : ISerializable,
                              IComparable,
                              IComparable<EntityTag>,
                              IEquatable<EntityTag>
    {
        public EntityTag(string value)
            : this()
        {
            if (null == value)
            {
                return;
            }

            if ("\"\"".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if ("\"".Equals(value, StringComparison.OrdinalIgnoreCase) ||
                "W/\"".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException(Resources.EntityTag_FormatException_Message);
            }

            if (!value.StartsWith("\"", StringComparison.OrdinalIgnoreCase) &&
                !value.StartsWith("W/\"", StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException(Resources.EntityTag_FormatException_Message);
            }

            if (!value.EndsWith("\"", StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException(Resources.EntityTag_FormatException_Message);
            }

            Value = value;
        }

        private EntityTag(SerializationInfo info,
                          StreamingContext context)
            : this()
        {
            Value = info.GetString("_value");
        }

        public static EntityTag Null
        {
            get
            {
                return new EntityTag();
            }
        }

        private string Value { get; set; }

        public static bool operator ==(EntityTag obj,
                                       EntityTag comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator >(EntityTag operand1,
                                      EntityTag operand2)
        {
            return 0 < Compare(operand1, operand2);
        }

        public static implicit operator string(EntityTag value)
        {
            return value.ToString();
        }

        public static implicit operator EntityTag(string value)
        {
            return new EntityTag(value);
        }

        public static bool operator !=(EntityTag obj,
                                       EntityTag comparand)
        {
            return !obj.Equals(comparand);
        }

        public static bool operator <(EntityTag operand1,
                                      EntityTag operand2)
        {
            return 0 > Compare(operand1, operand2);
        }

        public static int Compare(EntityTag operand1,
                                  EntityTag operand2)
        {
            return string.CompareOrdinal(operand1, operand2);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((EntityTag)obj);
        }

        public override int GetHashCode()
        {
            return null == Value
                       ? 0
                       : Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value ?? "\"\"";
        }

        public int CompareTo(object obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return CompareTo((EntityTag)obj);
        }

        public int CompareTo(EntityTag other)
        {
            return Compare(this, other);
        }

        public bool Equals(EntityTag other)
        {
            return Value == other.Value;
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