namespace Cavity.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [ImmutableObject(true)]
    [Serializable]
    public struct PostalAddressFileKey : IComparable<PostalAddressFileKey>,
                                         IEquatable<PostalAddressFileKey>,
                                         ISerializable
    {
        public PostalAddressFileKey(AlphaDecimal urn,
                                    AlphaDecimal umr)
            : this()
        {
            UniqueDeliveryPointReferenceNumber = urn;
            UniqueMultipleResidenceReferenceNumber = umr;
        }

        private PostalAddressFileKey(SerializationInfo info,
                                     StreamingContext context)
            : this()
        {
            UniqueDeliveryPointReferenceNumber = AlphaDecimal.FromString(info.GetString("_urn"));
            UniqueMultipleResidenceReferenceNumber = AlphaDecimal.FromString(info.GetString("_umr"));
        }

        public AlphaDecimal UniqueDeliveryPointReferenceNumber { get; private set; }

        public AlphaDecimal UniqueMultipleResidenceReferenceNumber { get; private set; }

        public static bool operator ==(PostalAddressFileKey obj,
                                       PostalAddressFileKey comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator >(PostalAddressFileKey operand1,
                                      PostalAddressFileKey operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }

        public static bool operator <(PostalAddressFileKey operand1,
                                      PostalAddressFileKey operand2)
        {
            return operand1.CompareTo(operand2) < 0;
        }

        public static implicit operator string(PostalAddressFileKey value)
        {
            return value.ToString();
        }

        public static implicit operator PostalAddressFileKey(string value)
        {
            return FromString(value);
        }

        public static bool operator !=(PostalAddressFileKey obj,
                                       PostalAddressFileKey comparand)
        {
            return !obj.Equals(comparand);
        }

        public static int Compare(PostalAddressFileKey operand1,
                                  PostalAddressFileKey operand2)
        {
            var comparison = operand1.UniqueDeliveryPointReferenceNumber.CompareTo(operand2.UniqueDeliveryPointReferenceNumber);
            if (comparison > 0)
            {
                return 1;
            }

            if (comparison < 0)
            {
                return -1;
            }

            comparison = operand1.UniqueMultipleResidenceReferenceNumber.CompareTo(operand2.UniqueMultipleResidenceReferenceNumber);
            if (comparison > 0)
            {
                return 1;
            }

            if (comparison < 0)
            {
                return -1;
            }

            return 0;
        }

        public static PostalAddressFileKey FromString(string value)
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

            var parts = value.Split('·', StringSplitOptions.RemoveEmptyEntries);
            var urn = AlphaDecimal.FromString(parts[0]);
            var umr = 1 == parts.Length ? AlphaDecimal.Zero : AlphaDecimal.FromString(parts[1]);
            return new PostalAddressFileKey
                       {
                           UniqueDeliveryPointReferenceNumber = urn,
                           UniqueMultipleResidenceReferenceNumber = umr,
                       };
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((PostalAddressFileKey)obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public int CompareTo(PostalAddressFileKey other)
        {
            return Compare(this, other);
        }

        public override string ToString()
        {
            return "{0}·{1}".FormatWith(UniqueDeliveryPointReferenceNumber, UniqueMultipleResidenceReferenceNumber);
        }

        public bool Equals(PostalAddressFileKey other)
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

            info.AddValue("_urn", (string)UniqueDeliveryPointReferenceNumber);
            info.AddValue("_umr", (string)UniqueMultipleResidenceReferenceNumber);
        }
    }
}