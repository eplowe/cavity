namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.Serialization;
#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date", Justification = "This name is intentional.")]
    [Serializable]
    public struct Date : ISerializable, 
                         IEquatable<Date>
    {
        private DateTime _date;

        public Date(int year, 
                    int month, 
                    int day)
            : this(new DateTime(year, month, day))
        {
        }

        public Date(DateTime date)
        {
            _date = date;
        }

        private Date(SerializationInfo info, 
                     StreamingContext context)
            : this()
        {
            _date = info.GetDateTime("_value");
        }

        public static Date Today
        {
            get
            {
                return DateTime.Today;
            }
        }

        public static Date Tomorrow
        {
            get
            {
                return DateTime.Today.AddDays(1);
            }
        }

        public static Date Yesterday
        {
            get
            {
                return DateTime.Today.AddDays(-1);
            }
        }

        public int Day
        {
            get
            {
                return _date.Day;
            }
        }

        public int Month
        {
            get
            {
                return _date.Month;
            }
        }

        public int Year
        {
            get
            {
                return _date.Year;
            }
        }

        public static bool operator ==(Date obj, 
                                       Date comparand)
        {
            return obj.Equals(comparand);
        }

        public static implicit operator DateTime(Date value)
        {
            return value.ToDateTime();
        }

        public static implicit operator Date(DateTime value)
        {
            return new Date(value);
        }

        public static implicit operator string(Date value)
        {
            return value.ToString();
        }

        public static implicit operator Date(string value)
        {
            return FromString(value);
        }

        public static bool operator !=(Date obj, 
                                       Date comparand)
        {
            return !obj.Equals(comparand);
        }

        public static Date FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return new Date(DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal));
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((Date)obj);
        }

        public override int GetHashCode()
        {
            return ToDateTime().GetHashCode();
        }

        public DateTime ToDateTime()
        {
            return _date.Date;
        }

        public override string ToString()
        {
#if NET20
            return ObjectExtensionMethods.ToXmlString(_date).Substring(0, 10);
#else
            return _date.ToXmlString().Substring(0, 10);
#endif
        }

        public bool Equals(Date other)
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

            info.AddValue("_value", _date);
        }
    }
}