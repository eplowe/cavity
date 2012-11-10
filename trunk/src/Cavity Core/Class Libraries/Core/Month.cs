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
                          IGetNextMonth, 
                          IGetPreviousMonth
    {
        private DateTime _date;

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

        public static Month Current
        {
            get
            {
                return DateTime.Today;
            }
        }

        public static Month MaxValue
        {
            get
            {
                return DateTime.MaxValue;
            }
        }

        public static Month MinValue
        {
            get
            {
                return DateTime.MinValue;
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

        public IGetNextMonth Next
        {
            get
            {
                return this;
            }
        }

        public IGetPreviousMonth Previous
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

        public static implicit operator DateTime(Month value)
        {
            return value.ToDateTime();
        }

        public static implicit operator Month(DateTime value)
        {
            return new Month(value);
        }

        public static implicit operator Month(Date value)
        {
            return new Month(value);
        }

        public static implicit operator string(Month value)
        {
            return value.ToString();
        }

        public static implicit operator Month(string value)
        {
            return FromString(value);
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
            return _date.AddMonths(value);
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
            return _date.AddYears(value);
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

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((Month)obj);
        }

        public override int GetHashCode()
        {
            return ToDateTime().GetHashCode();
        }

        public Month ToNext(MonthOfYear value)
        {
            return To(value, 1);
        }

        public DateTime ToDateTime()
        {
            return _date.Date;
        }

        public Month ToPrevious(MonthOfYear value)
        {
            return To(value, -1);
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

        Month IGetNextMonth.Month()
        {
            return AddMonths(1);
        }

        Month IGetNextMonth.Year()
        {
            return AddYears(1);
        }

        Month IGetNextMonth.Year(MonthOfYear month)
        {
            return (this as IGetNextMonth).Year((int)month);
        }

        Month IGetNextMonth.Year(int month)
        {
            return new Month(Year, month).AddYears(1);
        }

        Month IGetPreviousMonth.Month()
        {
            return AddMonths(-1);
        }

        Month IGetPreviousMonth.Year()
        {
            return AddYears(-1);
        }

        Month IGetPreviousMonth.Year(MonthOfYear month)
        {
            return (this as IGetPreviousMonth).Year((int)month);
        }

        Month IGetPreviousMonth.Year(int month)
        {
            return new Month(Year, month).AddYears(-1);
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