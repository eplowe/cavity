namespace Cavity
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.Serialization;

#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date", Justification = "This name is intentional.")]
    [ImmutableObject(true)]
    [Serializable]
    public struct Date : IComparable, 
                         IComparable<Date>, 
                         IEquatable<Date>, 
                         ISerializable, 
                         IGetNextDate, 
                         IGetPreviousDate
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
            _date = new DateTime(date.Year, date.Month, date.Day);
        }

        private Date(SerializationInfo info, 
                     StreamingContext context)
            : this()
        {
            _date = info.GetDateTime("_value");
        }

        public static Date MaxValue
        {
            get
            {
                return DateTime.MaxValue;
            }
        }

        public static Date MinValue
        {
            get
            {
                return DateTime.MinValue;
            }
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

        public DayOfWeek DayOfWeek
        {
            get
            {
                return _date.DayOfWeek;
            }
        }

        public int DayOfYear
        {
            get
            {
                return _date.DayOfYear;
            }
        }

        public int DaysInMonth
        {
            get
            {
                return DateTime.DaysInMonth(Year, Month);
            }
        }

        public Date FirstOfMonth
        {
            get
            {
                return new Date(Year, Month, 1);
            }
        }

        public bool IsDaylightSavingTime
        {
            get
            {
                return _date.IsDaylightSavingTime();
            }
        }

        public bool IsLeapYear
        {
            get
            {
                return 29 == DateTime.DaysInMonth(Year, 2);
            }
        }

        public Date LastOfMonth
        {
            get
            {
                return new Date(Year, Month, DaysInMonth);
            }
        }

        public int Month
        {
            get
            {
                return _date.Month;
            }
        }

        public MonthOfYear MonthOfYear
        {
            get
            {
                return (MonthOfYear)Month;
            }
        }

        public IGetNextDate Next
        {
            get
            {
                return this;
            }
        }

        public IGetPreviousDate Previous
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

        Date IGetNextDate.Day
        {
            get
            {
                return AddDays(1);
            }
        }

        Date IGetNextDate.Friday
        {
            get
            {
                return ToNext(DayOfWeek.Friday);
            }
        }

        Date IGetNextDate.Monday
        {
            get
            {
                return ToNext(DayOfWeek.Monday);
            }
        }

        Date IGetNextDate.Saturday
        {
            get
            {
                return ToNext(DayOfWeek.Saturday);
            }
        }

        Date IGetNextDate.Sunday
        {
            get
            {
                return ToNext(DayOfWeek.Sunday);
            }
        }

        Date IGetNextDate.Thursday
        {
            get
            {
                return ToNext(DayOfWeek.Thursday);
            }
        }

        Date IGetNextDate.Tuesday
        {
            get
            {
                return ToNext(DayOfWeek.Tuesday);
            }
        }

        Date IGetNextDate.Week
        {
            get
            {
                return AddWeeks(1);
            }
        }

        Date IGetNextDate.Wednesday
        {
            get
            {
                return ToNext(DayOfWeek.Wednesday);
            }
        }

        Date IGetPreviousDate.Day
        {
            get
            {
                return AddDays(-1);
            }
        }

        Date IGetPreviousDate.Friday
        {
            get
            {
                return ToPrevious(DayOfWeek.Friday);
            }
        }

        Date IGetPreviousDate.Monday
        {
            get
            {
                return ToPrevious(DayOfWeek.Monday);
            }
        }

        Date IGetPreviousDate.Saturday
        {
            get
            {
                return ToPrevious(DayOfWeek.Saturday);
            }
        }

        Date IGetPreviousDate.Sunday
        {
            get
            {
                return ToPrevious(DayOfWeek.Sunday);
            }
        }

        Date IGetPreviousDate.Thursday
        {
            get
            {
                return ToPrevious(DayOfWeek.Thursday);
            }
        }

        Date IGetPreviousDate.Tuesday
        {
            get
            {
                return ToPrevious(DayOfWeek.Tuesday);
            }
        }

        Date IGetPreviousDate.Week
        {
            get
            {
                return AddWeeks(-1);
            }
        }

        Date IGetPreviousDate.Wednesday
        {
            get
            {
                return ToPrevious(DayOfWeek.Wednesday);
            }
        }

        public static bool operator ==(Date obj, 
                                       Date comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator >(Date operand1, 
                                      Date operand2)
        {
            return operand1.ToDateTime() > operand2.ToDateTime();
        }

        public static bool operator >=(Date operand1, 
                                       Date operand2)
        {
            if (operand1 == operand2)
            {
                return true;
            }

            return operand1 > operand2;
        }

        public static bool operator <=(Date operand1, 
                                       Date operand2)
        {
            if (operand1 == operand2)
            {
                return true;
            }

            return operand1 < operand2;
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

        public static bool operator <(Date operand1, 
                                      Date operand2)
        {
            return operand1.ToDateTime() < operand2.ToDateTime();
        }

        public static int Compare(Date operand1, 
                                  Date operand2)
        {
            return DateTime.Compare(operand1.ToDateTime(), operand2.ToDateTime());
        }

        public static Date FromString(string value)
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

            return new Date(DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal));
        }

        public Date AddDays(int value)
        {
            return _date.AddDays(value);
        }

        public Date AddMonths(int value)
        {
            return _date.AddMonths(value);
        }

        public Date AddQuarters(int value)
        {
            if (value > int.MaxValue / 3)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return AddMonths(value * 3);
        }

        public Date AddWeeks(int value)
        {
            if (value > int.MaxValue / 7)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return _date.AddDays(value * 7);
        }

        public Date AddYears(int value)
        {
            return _date.AddYears(value);
        }

        public int CompareTo(object obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return CompareTo((Date)obj);
        }

        public int CompareTo(Date other)
        {
            return Compare(this, other);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((Date)obj);
        }

        public override int GetHashCode()
        {
            return ToDateTime().GetHashCode();
        }

        public Date ToNext(DayOfWeek value)
        {
            return To(value, 1);
        }

        public Date ToNext(MonthOfYear value)
        {
            return To(value, 1);
        }

        public DateTime ToDateTime()
        {
            return _date.Date;
        }

        public Month ToMonth()
        {
            return _date.Date;
        }

        public Date ToPrevious(DayOfWeek value)
        {
            return To(value, -1);
        }

        public Date ToPrevious(MonthOfYear value)
        {
            return To(value, -1);
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

        Date IGetNextDate.Month()
        {
            return AddMonths(1);
        }

        Date IGetNextDate.Month(int day)
        {
            return new Date(Year, Month, day).AddMonths(1);
        }

        Date IGetNextDate.Year()
        {
            return AddYears(1);
        }

        Date IGetNextDate.Year(MonthOfYear month, 
                               int day)
        {
            return (this as IGetNextDate).Year((int)month, day);
        }

        Date IGetNextDate.Year(int month, 
                               int day)
        {
            return new Date(Year, month, day).AddYears(1);
        }

        Date IGetPreviousDate.Month()
        {
            return AddMonths(-1);
        }

        Date IGetPreviousDate.Month(int day)
        {
            return new Date(Year, Month, day).AddMonths(-1);
        }

        Date IGetPreviousDate.Year()
        {
            return AddYears(-1);
        }

        Date IGetPreviousDate.Year(MonthOfYear month, 
                                   int day)
        {
            return (this as IGetPreviousDate).Year((int)month, day);
        }

        Date IGetPreviousDate.Year(int month, 
                                   int day)
        {
            return new Date(Year, month, day).AddYears(-1);
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

        private Date To(DayOfWeek value, 
                        int day)
        {
#if NET20
            if (!GenericExtensionMethods.In(value,
#else
            if (!value.In(
#endif
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday))
            {
                throw new ArgumentOutOfRangeException("value");
            }

            var date = _date;
            while (true)
            {
                date = date.AddDays(day);
                if (value == date.DayOfWeek)
                {
                    return date;
                }
            }
        }

        private Date To(MonthOfYear value, 
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

            var date = _date;
            while (true)
            {
                date = date.AddMonths(month);
                if ((int)value == date.Month)
                {
                    return date;
                }
            }
        }
    }
}