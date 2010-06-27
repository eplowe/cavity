namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class UserAgent : ComparableObject, IUserAgent
    {
        private string _value;

        public UserAgent()
            : this(UserAgent.Format())
        {
        }

        public UserAgent(string value)
        {
            this.Value = value;
        }

        public string Value
        {
            get
            {
                return this._value;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._value = value;
            }
        }

        public static implicit operator UserAgent(string value)
        {
            return object.ReferenceEquals(null, value) ? null as UserAgent : UserAgent.FromString(value);
        }

        public static string Format()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return UserAgent.Format(version.Major, version.Minor);
        }

        public static string Format(int major, int minor)
        {
            return string.Format(CultureInfo.CurrentUICulture, Resources.UserAgent_ValueFormat, major, minor);
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
            return this.Value;
        }
    }
}