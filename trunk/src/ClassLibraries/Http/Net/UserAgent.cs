namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class UserAgent : IComparable, IUserAgent
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

        public static implicit operator string(UserAgent value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static implicit operator UserAgent(string value)
        {
            return object.ReferenceEquals(null, value) ? null as UserAgent : UserAgent.Parse(value);
        }

        public static bool operator ==(UserAgent operand1, UserAgent operand2)
        {
            return 0 == UserAgent.Compare(operand1, operand2);
        }

        public static bool operator !=(UserAgent operand1, UserAgent operand2)
        {
            return 0 != UserAgent.Compare(operand1, operand2);
        }

        public static bool operator <(UserAgent operand1, UserAgent operand2)
        {
            return UserAgent.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(UserAgent operand1, UserAgent operand2)
        {
            return UserAgent.Compare(operand1, operand2) > 0;
        }

        public static int Compare(UserAgent comparand1, UserAgent comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : string.Compare(
                    object.ReferenceEquals(null, comparand1) ? null as string : comparand1.Value,
                    object.ReferenceEquals(null, comparand2) ? null as string : comparand2.Value,
                    StringComparison.OrdinalIgnoreCase);
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

        public static UserAgent Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return new UserAgent(value);
        }

        public int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                UserAgent value = obj as UserAgent;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                comparison = UserAgent.Compare(this, value);
            }

            return comparison;
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (!object.ReferenceEquals(null, obj))
            {
                if (object.ReferenceEquals(this, obj))
                {
                    result = true;
                }
                else
                {
                    UserAgent cast = obj as UserAgent;

                    if (!object.ReferenceEquals(null, cast))
                    {
                        result = 0 == UserAgent.Compare(this, cast);
                    }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}