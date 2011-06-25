namespace Cavity.Data
{
    using System;
    using System.Runtime.Serialization;
#if NET20 || NET35
    using System.Security.Permissions;
#endif
    using System.Text;

    [Serializable]
    public struct KeyStringPair : ISerializable,
                                  IEquatable<KeyStringPair>
    {
        public KeyStringPair(string key,
                             string value)
            : this()
        {
            Key = key;
            Value = value;
        }

        private KeyStringPair(SerializationInfo info,
                              StreamingContext context)
            : this()
        {
            Key = info.GetString("_key");
            Value = info.GetString("_value");
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public static bool operator ==(KeyStringPair obj,
                                       KeyStringPair comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator !=(KeyStringPair obj,
                                       KeyStringPair comparand)
        {
            return !obj.Equals(comparand);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((KeyStringPair)obj);
        }

        public override int GetHashCode()
        {
            return (Key ?? string.Empty).GetHashCode() ^ (Value ?? string.Empty).GetHashCode();
        }

        public override string ToString()
        {
            var length = Math.Max((Key ?? string.Empty).Length, (Value ?? string.Empty).Length);
            var buffer = new StringBuilder();
            buffer.AppendLine(Key);
            buffer.AppendLine(new string('-', 0 == length ? 1 : length));
            buffer.Append(Value);

            return buffer.ToString();
        }

        public bool Equals(KeyStringPair other)
        {
            return string.Equals(Key, other.Key, StringComparison.Ordinal) &&
                   string.Equals(Value, other.Value, StringComparison.Ordinal);
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

            info.AddValue("_key", Key);
            info.AddValue("_value", Value);
        }
    }
}