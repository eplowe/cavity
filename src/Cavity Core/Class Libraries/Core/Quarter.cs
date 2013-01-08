namespace Cavity
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Xml;

#if NET20 || NET35
    using System.Security.Permissions;
#endif

    [ImmutableObject(true)]
    [Serializable]
    public struct Quarter : IComparable,
                            IComparable<Quarter>,
                            IEquatable<Quarter>,
                            ISerializable,
                            IGetTimeZone<Quarter>
    {
        private DateTime _date;

        public Quarter(int year,
                       QuarterOfYear quarter)
            : this(new DateTime(year, ToMonth(quarter), 1))
        {
        }

        public Quarter(int year,
                       int quarter)
            : this(new DateTime(year, ToMonth((QuarterOfYear)quarter), 1))
        {
        }

        public Quarter(Date date)
        {
            _date = new DateTime(date.Year, date.Month, 1);
        }

        public Quarter(DateTime date)
        {
            _date = new DateTime(date.Year, date.Month, 1);
        }

        private Quarter(SerializationInfo info,
                        StreamingContext context)
            : this()
        {
            _date = info.GetDateTime("_value");
        }

        public static Quarter MaxValue
        {
            get
            {
                return new Quarter(DateTime.MaxValue);
            }
        }

        public static Quarter MinValue
        {
            get
            {
                return new Quarter(DateTime.MinValue);
            }
        }

        public static IGetTimeZone<Quarter> Today
        {
            get
            {
                return new Quarter(DateTime.Today);
            }
        }

        public QuarterOfYear QuarterOfYear
        {
            get
            {
                switch (_date.Month)
                {
                    case 1:
                    case 2:
                    case 3:
                        return QuarterOfYear.Q1;
                    case 4:
                    case 5:
                    case 6:
                        return QuarterOfYear.Q2;
                    case 7:
                    case 8:
                    case 9:
                        return QuarterOfYear.Q3;
                    default:
                        return QuarterOfYear.Q4;
                }
            }
        }

        public int Year
        {
            get
            {
                return _date.Year;
            }
        }

        Quarter IGetTimeZone<Quarter>.LocalTime
        {
            get
            {
                return new Quarter(DateTime.Today);
            }
        }

        Quarter IGetTimeZone<Quarter>.UniversalTime
        {
            get
            {
                return new Quarter(DateTime.Today.ToUniversalTime());
            }
        }

        public static bool operator ==(Quarter obj,
                                       Quarter comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator >(Quarter operand1,
                                      Quarter operand2)
        {
            return operand1.ToDateTime() > operand2.ToDateTime();
        }

        public static bool operator >=(Quarter operand1,
                                       Quarter operand2)
        {
            if (operand1 == operand2)
            {
                return true;
            }

            return operand1 > operand2;
        }

        public static bool operator <=(Quarter operand1,
                                       Quarter operand2)
        {
            if (operand1 == operand2)
            {
                return true;
            }

            return operand1 < operand2;
        }

        public static implicit operator string(Quarter value)
        {
            return value.ToString();
        }

        public static implicit operator Quarter(string value)
        {
            return FromString(value);
        }

        public static Quarter operator ++(Quarter operand)
        {
            return operand.Increment();
        }

        public static Quarter operator --(Quarter operand)
        {
            return operand.Decrement();
        }

        public static bool operator !=(Quarter obj,
                                       Quarter comparand)
        {
            return !obj.Equals(comparand);
        }

        public static bool operator <(Quarter operand1,
                                      Quarter operand2)
        {
            return operand1.ToDateTime() < operand2.ToDateTime();
        }

        public static int Compare(Quarter operand1,
                                  Quarter operand2)
        {
            return DateTime.Compare(operand1.ToDateTime(), operand2.ToDateTime());
        }

        public static Quarter FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            value = value.Trim();
            if (7 != value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            switch (value.Substring(4).ToUpperInvariant())
            {
                case " Q1":
                    return new Quarter(XmlConvert.ToInt32(value.Substring(0, 4)), 1);
                case " Q2":
                    return new Quarter(XmlConvert.ToInt32(value.Substring(0, 4)), 2);
                case " Q3":
                    return new Quarter(XmlConvert.ToInt32(value.Substring(0, 4)), 3);
                case " Q4":
                    return new Quarter(XmlConvert.ToInt32(value.Substring(0, 4)), 4);
                default:
                    throw new FormatException("The quarter value must be in the format 1999 Q2.");
            }
        }

        public Quarter AddQuarters(int value)
        {
            if (value > int.MaxValue / 3)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return new Quarter(_date.AddMonths(value * 3));
        }

        public Quarter AddYears(int value)
        {
            return new Quarter(_date.AddYears(value));
        }

        public int CompareTo(object obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return CompareTo((Quarter)obj);
        }

        public int CompareTo(Quarter other)
        {
            return Compare(this, other);
        }

        public Quarter Decrement()
        {
            return AddQuarters(-1);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((Quarter)obj);
        }

        public override int GetHashCode()
        {
            return ToDateTime().GetHashCode();
        }

        public Quarter Increment()
        {
            return AddQuarters(1);
        }

        public Quarter Next(QuarterOfYear value)
        {
            return To(value, 1);
        }

        public Quarter Previous(QuarterOfYear value)
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
            var year = ObjectExtensionMethods.ToXmlString(_date).Substring(0, 4);
#else
            var year = _date.ToXmlString().Substring(0, 4);
#endif
            switch (_date.Month)
            {
                case 1:
                case 2:
                case 3:
                    return year + " Q1";
                case 4:
                case 5:
                case 6:
                    return year + " Q2";
                case 7:
                case 8:
                case 9:
                    return year + " Q3";
                default:
                    return year + " Q4";
            }
        }

        public bool Equals(Quarter other)
        {
            return ToString() == other.ToString();
        }

#if !NET20
        Quarter IGetTimeZone<Quarter>.For(TimeZoneInfo value)
        {
            return new Quarter(DateTime.Today.ToLocalTime(value));
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

        private Quarter To(QuarterOfYear value,
                           int quarter)
        {
#if NET20
            if (!GenericExtensionMethods.In(value, 
#else
            if (!value.In(
#endif
                     QuarterOfYear.Q1,
                     QuarterOfYear.Q2,
                     QuarterOfYear.Q3,
                     QuarterOfYear.Q4))
            {
                throw new ArgumentOutOfRangeException("value");
            }

            var date = new Quarter(_date);
            while (true)
            {
                date = date.AddQuarters(quarter);
                if (value == date.QuarterOfYear)
                {
                    return date;
                }
            }
        }

        private static int ToMonth(QuarterOfYear value)
        {
            switch (value)
            {
                case QuarterOfYear.Q1:
                    return 1;
                case QuarterOfYear.Q2:
                    return 4;
                case QuarterOfYear.Q3:
                    return 7;
                case QuarterOfYear.Q4:
                    return 10;
                default:
                    throw new ArgumentOutOfRangeException("value");
            }
        }
    }
}