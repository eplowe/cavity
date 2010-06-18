namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class UserAgent : IUserAgent
    {
        private static readonly string _value = UserAgent.DefaultValue();

        public string Value
        {
            get
            {
                return _value;
            }
        }

        public static string DefaultValue()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return UserAgent.DefaultValue(version.Major, version.Minor);
        }

        public static string DefaultValue(int major, int minor)
        {
            return string.Format(CultureInfo.CurrentUICulture, Resources.UserAgent_ValueFormat, major, minor);
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}