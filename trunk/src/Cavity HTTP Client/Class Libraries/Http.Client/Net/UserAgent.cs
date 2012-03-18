namespace Cavity.Net
{
    using System;
    using System.Reflection;

    using Cavity.Properties;

    public sealed class UserAgent : ComparableObject, 
                                    IUserAgent
    {
        private string _value;

        public UserAgent()
            : this(Format())
        {
        }

        public UserAgent(string value)
        {
            Value = value;
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

                _value = value;
            }
        }

        public static implicit operator UserAgent(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
        }

        public static string Format()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return Format(version.Major, version.Minor);
        }

        public static string Format(int major, 
                                    int minor)
        {
            return Resources.UserAgent_ValueFormat.FormatWith(major, minor);
        }

        public static UserAgent FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return new UserAgent(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}