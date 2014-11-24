namespace Cavity
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.Serialization;

#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [ImmutableObject(true)]
    [Serializable]
    public struct Month : IComparable,
                          IComparable<Month>,
                          IEquatable<Month>,
                          ISerializable,
                          IChangeMonth<Month>,
                          IGetTimeZone<Month>
    {
        private DateTime _date;

        public Month(int year,
                     MonthOfYear month)
            : this(new DateTime(year, (int)month, 1))
        {
        }

        public Month(int year,
                     int month)
            : this(new DateTime(year, month, 1))
        {
        }

        public Month(Date date)
        {
            _date = new DateTime(date.Year, date.Month, 1);
        }

        public Month(DateTime date)
        {
            _date = new DateTime(date.Year, date.Month, 1);
        }

        private Month(SerializationInfo info,
                      StreamingContext context)
            : this()
        {
            _date = info.GetDateTime("_value");
        }

        public static Month MaxValue
        {
            get
            {
                return new Month(DateTime.MaxValue);
            }
        }

        public static Month MinValue
        {
            get
            {
                return new Month(DateTime.MinValue);
            }
        }

        public static IGetTimeZone<Month> Today
        {
            get
            {
                return new Month(DateTime.Today);
            }
        }

        public int DaysInMonth
        {
            get
            {
                return DateTime.DaysInMonth(Year, _date.Month);
            }
        }

        public bool IsLeapYear
        {
            get
            {
                return 29 == DateTime.DaysInMonth(Year, 2);
            }
        }

        public MonthOfYear MonthOfYear
        {
            get
            {
                return (MonthOfYear)_date.Month;
            }
        }

        public IChangeMonth<Month> Change
        {
            get
            {
                return this;
            }
        }

        public int Year
        {
            get
            {
                return _date.Year;
            }
        }

        Month IGetTimeZone<Month>.LocalTime
        {
            get
            {
                return new Month(DateTime.Today);
            }
        }

        Month IGetTimeZone<Month>.UniversalTime
        {
            get
            {
                return new Month(DateTime.Today.ToUniversalTime());
            }
        }

        public static bool operator ==(Month obj,
                                       Month comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator >(Month operand1,
                                      Month operand2)
        {
            return operand1.ToDateTime() > operand2.ToDateTime();
        }

        public static bool operator >=(Month operand1,
                                       Month operand2)
        {
            if (operand1 == operand2)
            {
                return true;
            }

            return operand1 > operand2;
        }

        public static bool operator <=(Month operand1,
                                       Month operand2)
        {
            if (operand1 == operand2)
            {
                return true;
            }

            return operand1 < operand2;
        }

        public static implicit operator string(Month value)
        {
            return value.ToString();
        }

        public static implicit operator Month(string value)
        {
            return FromString(value);
        }

        public static Month operator ++(Month operand)
        {
            return operand.Increment();
        }

        public static Month operator --(Month operand)
        {
            return operand.Decrement();
        }

        public static bool operator !=(Month obj,
                                       Month comparand)
        {
            return !obj.Equals(comparand);
        }

        public static bool operator <(Month operand1,
                                      Month operand2)
        {
            return operand1.ToDateTime() < operand2.ToDateTime();
        }

        public static int Compare(Month operand1,
                                  Month operand2)
        {
            return DateTime.Compare(operand1.ToDateTime(), operand2.ToDateTime());
        }

        public static Month FromString(string value)
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

            return new Month(DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal));
        }

        public Month AddMonths(int value)
        {
            return new Month(_date.AddMonths(value));
        }

        public Month AddQuarters(int value)
        {
            if (value > int.MaxValue / 3)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return AddMonths(value * 3);
        }

        public Month AddYears(int value)
        {
            return new Month(_date.AddYears(value));
        }

        public int CompareTo(object obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return CompareTo((Month)obj);
        }

        public int CompareTo(Month other)
        {
            return Compare(this, other);
        }

        public Month Decrement()
        {
            return AddMonths(-1);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((Month)obj);
        }

        public override int GetHashCode()
        {
            return ToDateTime().GetHashCode();
        }

        public Month Increment()
        {
            return AddMonths(1);
        }

        public Month Next(MonthOfYear value)
        {
            return To(value, 1);
        }

        public Month Previous(MonthOfYear value)
        {
            return To(value, -1);
        }

        public DateTime ToDateTime()
        {
            return _date.Date;
        }

        public Date ToDate()
        {
            return new Date(_date.Date);
        }

        public override string ToString()
        {
#if NET20
            return ObjectExtensionMethods.ToXmlString(_date).Substring(0, 7);
#else
            return _date.ToXmlString().Substring(0, 7);
#endif
        }

        public bool Equals(Month other)
        {
            return ToString() == other.ToString();
        }

        Month IChangeMonth<Month>.Month(int value)
        {
            return new Month(Year, value);
        }

        Month IChangeMonth<Month>.To(MonthOfYear month)
        {
            return new Month(Year, month);
        }

        Month IChangeMonth<Month>.Year(int value)
        {
            return new Month(value, MonthOfYear);
        }

#if !NET20
        Month IGetTimeZone<Month>.For(TimeZoneInfo value)
        {
            return new Month(DateTime.Today.ToLocalTime(value));
        }
#endif

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

        private Month To(MonthOfYear value,
                         int month)
        {
#if NET20
            if (!GenericExtensionMethods.In(value, 
#else
            if (!value.In(
#endif
                     MonthOfYear.January,
                     MonthOfYear.February,
                     MonthOfYear.March,
                     MonthOfYear.April,
                     MonthOfYear.May,
                     MonthOfYear.June,
                     MonthOfYear.July,
                     MonthOfYear.August,
                     MonthOfYear.September,
                     MonthOfYear.October,
                     MonthOfYear.November,
                     MonthOfYear.December))
            {
                throw new ArgumentOutOfRangeException("value");
            }

            var date = new Month(_date);
            while (true)
            {
                date = date.AddMonths(month);
                if (value == date.MonthOfYear)
                {
                    return date;
                }
            }
        }
    }
}