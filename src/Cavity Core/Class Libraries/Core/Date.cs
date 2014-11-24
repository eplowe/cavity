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

    [ImmutableObject(true)]
    [Serializable]
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date", Justification = "This name is intentional.")]
    public struct Date : IComparable,
                         IComparable<Date>,
                         IEquatable<Date>,
                         ISerializable,
                         ICalculateDateTimeCalendar,
                         ICalculateDateTimePeriod<Date>,
                         IChangeDate,
                         IGetNextDate,
                         IGetPreviousDate,
                         IGetTimeZone<Date>
    {
        private DateTime _date;

        public Date(int year,
                    MonthOfYear month,
                    int day)
            : this(year, (int)month, day)
        {
        }

        public Date(Month month,
                    int day)
            : this(month.Year, month.MonthOfYear, day)
        {
        }

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
                return new Date(DateTime.MaxValue);
            }
        }

        public static Date MinValue
        {
            get
            {
                return new Date(DateTime.MinValue);
            }
        }

        public static IGetTimeZone<Date> Today
        {
            get
            {
                return new Date(DateTime.Today);
            }
        }

        public static IGetTimeZone<Date> Tomorrow
        {
            get
            {
                return new Date(DateTime.Today.AddDays(1));
            }
        }

        public static IGetTimeZone<Date> Yesterday
        {
            get
            {
                var value = DateTime.Today.AddDays(-1);
                return new Date(value);
            }
        }

        public ICalculateDateTimeCalendar Calendar
        {
            get
            {
                return this;
            }
        }

        public IChangeDate Change
        {
            get
            {
                return this;
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

        public ICalculateDateTimePeriod<Date> Period
        {
            get
            {
                return this;
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

        DateTimePeriod ICalculateDateTimeCalendar.Month
        {
            get
            {
                return new DateTimePeriod(new Date(_date).FirstOfMonth.ToDateTime(),
                                          new Date(_date).LastOfMonth.ToDateTime());
            }
        }

        DateTimePeriod ICalculateDateTimeCalendar.Week
        {
            get
            {
                if (DayOfWeek.Monday == _date.DayOfWeek)
                {
                    return new DateTimePeriod(_date, new Date(_date).Next.Sunday.ToDateTime());
                }

                if (DayOfWeek.Sunday == _date.DayOfWeek)
                {
                    return new DateTimePeriod(new Date(_date).Previous.Monday.ToDateTime(), _date);
                }

                return new DateTimePeriod(new Date(_date).Previous.Monday.ToDateTime(),
                                          new Date(_date).Next.Sunday.ToDateTime());
            }
        }

        DateTimePeriod ICalculateDateTimeCalendar.Year
        {
            get
            {
                return new DateTimePeriod(new DateTime(_date.Year, 1, 1),
                                          new DateTime(_date.Year, 12, 31));
            }
        }

        Date IGetNextDate.Day
        {
            get
            {
                return AddDays(1);
            }
        }

        Date IGetNextDate.Week
        {
            get
            {
                return AddWeeks(1);
            }
        }

        Date IGetNextDate.Month
        {
            get
            {
                return AddMonths(1);
            }
        }

        Date IGetNextDate.Year
        {
            get
            {
                return AddYears(1);
            }
        }

        Date IGetNextMonth.January
        {
            get
            {
                return ToNext(MonthOfYear.January);
            }
        }

        Date IGetNextMonth.February
        {
            get
            {
                return ToNext(MonthOfYear.February);
            }
        }

        Date IGetNextMonth.March
        {
            get
            {
                return ToNext(MonthOfYear.March);
            }
        }

        Date IGetNextMonth.April
        {
            get
            {
                return ToNext(MonthOfYear.April);
            }
        }

        Date IGetNextMonth.May
        {
            get
            {
                return ToNext(MonthOfYear.May);
            }
        }

        Date IGetNextMonth.June
        {
            get
            {
                return ToNext(MonthOfYear.June);
            }
        }

        Date IGetNextMonth.July
        {
            get
            {
                return ToNext(MonthOfYear.July);
            }
        }

        Date IGetNextMonth.August
        {
            get
            {
                return ToNext(MonthOfYear.August);
            }
        }

        Date IGetNextMonth.September
        {
            get
            {
                return ToNext(MonthOfYear.September);
            }
        }

        Date IGetNextMonth.October
        {
            get
            {
                return ToNext(MonthOfYear.October);
            }
        }

        Date IGetNextMonth.November
        {
            get
            {
                return ToNext(MonthOfYear.November);
            }
        }

        Date IGetNextMonth.December
        {
            get
            {
                return ToNext(MonthOfYear.December);
            }
        }

        Date IGetNextWeekday.Monday
        {
            get
            {
                return ToNext(DayOfWeek.Monday);
            }
        }

        Date IGetNextWeekday.Tuesday
        {
            get
            {
                return ToNext(DayOfWeek.Tuesday);
            }
        }

        Date IGetNextWeekday.Wednesday
        {
            get
            {
                return ToNext(DayOfWeek.Wednesday);
            }
        }

        Date IGetNextWeekday.Thursday
        {
            get
            {
                return ToNext(DayOfWeek.Thursday);
            }
        }

        Date IGetNextWeekday.Friday
        {
            get
            {
                return ToNext(DayOfWeek.Friday);
            }
        }

        Date IGetNextWeekday.Saturday
        {
            get
            {
                return ToNext(DayOfWeek.Saturday);
            }
        }

        Date IGetNextWeekday.Sunday
        {
            get
            {
                return ToNext(DayOfWeek.Sunday);
            }
        }

        Date IGetPreviousDate.Day
        {
            get
            {
                return AddDays(-1);
            }
        }

        Date IGetPreviousDate.Week
        {
            get
            {
                return AddWeeks(-1);
            }
        }

        Date IGetPreviousDate.Month
        {
            get
            {
                return AddMonths(-1);
            }
        }

        Date IGetPreviousDate.Year
        {
            get
            {
                return AddYears(-1);
            }
        }

        Date IGetPreviousMonth.January
        {
            get
            {
                return ToPrevious(MonthOfYear.January);
            }
        }

        Date IGetPreviousMonth.February
        {
            get
            {
                return ToPrevious(MonthOfYear.February);
            }
        }

        Date IGetPreviousMonth.March
        {
            get
            {
                return ToPrevious(MonthOfYear.March);
            }
        }

        Date IGetPreviousMonth.April
        {
            get
            {
                return ToPrevious(MonthOfYear.April);
            }
        }

        Date IGetPreviousMonth.May
        {
            get
            {
                return ToPrevious(MonthOfYear.May);
            }
        }

        Date IGetPreviousMonth.June
        {
            get
            {
                return ToPrevious(MonthOfYear.June);
            }
        }

        Date IGetPreviousMonth.July
        {
            get
            {
                return ToPrevious(MonthOfYear.July);
            }
        }

        Date IGetPreviousMonth.August
        {
            get
            {
                return ToPrevious(MonthOfYear.August);
            }
        }

        Date IGetPreviousMonth.September
        {
            get
            {
                return ToPrevious(MonthOfYear.September);
            }
        }

        Date IGetPreviousMonth.October
        {
            get
            {
                return ToPrevious(MonthOfYear.October);
            }
        }

        Date IGetPreviousMonth.November
        {
            get
            {
                return ToPrevious(MonthOfYear.November);
            }
        }

        Date IGetPreviousMonth.December
        {
            get
            {
                return ToPrevious(MonthOfYear.December);
            }
        }

        Date IGetPreviousWeekday.Monday
        {
            get
            {
                return ToPrevious(DayOfWeek.Monday);
            }
        }

        Date IGetPreviousWeekday.Tuesday
        {
            get
            {
                return ToPrevious(DayOfWeek.Tuesday);
            }
        }

        Date IGetPreviousWeekday.Wednesday
        {
            get
            {
                return ToPrevious(DayOfWeek.Wednesday);
            }
        }

        Date IGetPreviousWeekday.Thursday
        {
            get
            {
                return ToPrevious(DayOfWeek.Thursday);
            }
        }

        Date IGetPreviousWeekday.Friday
        {
            get
            {
                return ToPrevious(DayOfWeek.Friday);
            }
        }

        Date IGetPreviousWeekday.Saturday
        {
            get
            {
                return ToPrevious(DayOfWeek.Saturday);
            }
        }

        Date IGetPreviousWeekday.Sunday
        {
            get
            {
                return ToPrevious(DayOfWeek.Sunday);
            }
        }

        Date IGetTimeZone<Date>.LocalTime
        {
            get
            {
                return new Date(_date);
            }
        }

        Date IGetTimeZone<Date>.UniversalTime
        {
            get
            {
                return new Date(_date.ToUniversalTime());
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

        public static implicit operator string(Date value)
        {
            return value.ToString();
        }

        public static implicit operator Date(string value)
        {
            return FromString(value);
        }

        public static Date operator ++(Date operand)
        {
            return operand.Increment();
        }

        public static Date operator --(Date operand)
        {
            return operand.Decrement();
        }

        public static int operator -(Date operand1,
                                     Date operand2)
        {
            return Convert.ToInt32(Math.Round((operand1.ToDateTime() - operand2.ToDateTime()).TotalDays, 0, MidpointRounding.AwayFromZero));
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
            return new Date(_date.AddDays(value));
        }

        public Date AddMonths(int value)
        {
            return new Date(_date.AddMonths(value));
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

            return new Date(_date.AddDays(value * 7));
        }

        public Date AddYears(int value)
        {
            return new Date(_date.AddYears(value));
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

        public int CompareTo(DateTime other)
        {
            return Equals(other)
                       ? 0
                       : ToDateTime().CompareTo(other.Date);
        }

        public Date Decrement()
        {
            return AddDays(-1);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((Date)obj);
        }

        public override int GetHashCode()
        {
            return ToDateTime().GetHashCode();
        }

        public Date Increment()
        {
            return AddDays(1);
        }

        public DateTime ToDateTime()
        {
            return _date.Date;
        }

        public Month ToMonth()
        {
            return new Month(_date.Date);
        }

        public int Subtract(Date other)
        {
            return this - other;
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

        public bool Equals(DateTime other)
        {
            return ToDateTime() == other.Date;
        }

        DateTimePeriod ICalculateDateTimePeriod<Date>.Days(int value)
        {
            return 0 == value
                       ? new DateTimePeriod(_date, _date)
                       : DateTimePeriod.Between(_date, _date.AddDays(value));
        }

        DateTimePeriod ICalculateDateTimePeriod<Date>.Between(Date value)
        {
            return DateTimePeriod.Between(_date, value.ToDateTime());
        }

        DateTimePeriod ICalculateDateTimePeriod<Date>.Months(int value)
        {
            return 0 == value
                       ? new DateTimePeriod(_date, _date)
                       : DateTimePeriod.Between(_date, _date.AddMonths(value).AddDays(value > 0 ? -1 : 1));
        }

        DateTimePeriod ICalculateDateTimePeriod<Date>.Since(Date value)
        {
            var later = value.ToDateTime();
            if (later > _date)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return DateTimePeriod.Between(_date, later);
        }

        DateTimePeriod ICalculateDateTimePeriod<Date>.Until(Date value)
        {
            var later = value.ToDateTime();
            if (later < _date)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return DateTimePeriod.Between(_date, later);
        }

        DateTimePeriod ICalculateDateTimePeriod<Date>.Weeks(int value)
        {
            return 0 == value
                       ? new DateTimePeriod(_date, _date)
                       : DateTimePeriod.Between(_date, AddWeeks(value).ToDateTime().AddDays(value > 0 ? -1 : 1));
        }

        DateTimePeriod ICalculateDateTimePeriod<Date>.Years(int value)
        {
            return 0 == value
                       ? new DateTimePeriod(_date, _date)
                       : DateTimePeriod.Between(_date, _date.AddYears(value).AddDays(value > 0 ? -1 : 1));
        }

        Date IChangeDate.Day(int value)
        {
            return new Date(Year, Month, value);
        }

        Date IChangeMonth<Date>.Month(int value)
        {
            return new Date(Year, value, Day);
        }

        Date IChangeMonth<Date>.To(MonthOfYear month)
        {
            return new Date(Year, month, Day);
        }

        Date IChangeMonth<Date>.Year(int value)
        {
            return new Date(value, Month, Day);
        }

#if !NET20
        Date IGetTimeZone<Date>.For(TimeZoneInfo value)
        {
            return new Date(_date.ToLocalTime(value));
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

        private Date ToNext(DayOfWeek value)
        {
            return To(value, 1);
        }

        private Date ToNext(MonthOfYear value)
        {
            return To(value, 1);
        }

        private Date ToPrevious(DayOfWeek value)
        {
            return To(value, -1);
        }

        private Date ToPrevious(MonthOfYear value)
        {
            return To(value, -1);
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

            var date = new Date(_date);
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

            var date = new Date(_date);
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