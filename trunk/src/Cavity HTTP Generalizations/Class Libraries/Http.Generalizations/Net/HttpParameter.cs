namespace Cavity.Net
{
    using System;

    using Cavity.Properties;

    public class Parameter : ComparableObject
    {
        public Parameter(Token name, string value)
            : this()
        {
            Name = name;
            Value = value;
        }

        private Parameter()
        {
        }

        public Token Name { get; set; }

        public string Value { get; set; }

        public static implicit operator Parameter(string value)
        {
            return (null == value) ? null : FromString(value);
        }

        public static Parameter FromString(string value)
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

            if (value.ContainsAny(';'))
            {
                throw new FormatException(Resources.Parameter_FormatException_MessageSemicolon);
            }

#if NET20
            var parts = StringExtensionMethods.Split(value, '=', StringSplitOptions.None);
#else
            var parts = value.Split('=', StringSplitOptions.None);
#endif
            if (2 != parts.Length)
            {
                throw new FormatException(Resources.Parameter_FormatException_Message);
            }

            return new Parameter(parts[0], parts[1]);
        }

        public override string ToString()
        {
#if NET20
            return StringExtensionMethods.FormatWith("{0}={1}", Name, Value);
#else
            return "{0}={1}".FormatWith(Name, Value);
#endif
        }
    }
}