namespace Cavity
{
    using System;

    public struct DateTimePeriod : IEquatable<DateTimePeriod>
    {
        private DateTime _beginning;

        private DateTime _ending;

        public DateTimePeriod(Date beginning,
                              Date ending)
        {
            _ending = ending.ToDateTime();
            if (beginning > ending)
            {
                throw new ArgumentOutOfRangeException("beginning");
            }

            _beginning = beginning.ToDateTime();
        }

        public DateTimePeriod(DateTime beginning, 
                              DateTime ending)
        {
            _ending = ending;
            if (beginning > ending)
            {
                throw new ArgumentOutOfRangeException("beginning");
            }

            _beginning = beginning;
        }

        public DateTime Beginning
        {
            get
            {
                return _beginning;
            }

            set
            {
                if (value > Ending)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _beginning = value;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return Ending - Beginning;
            }
        }

        public DateTime Ending
        {
            get
            {
                return _ending;
            }

            set
            {
                if (value < Beginning)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _ending = value;
            }
        }

        public static bool operator ==(DateTimePeriod obj,
                                       DateTimePeriod comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator !=(DateTimePeriod obj,
                                       DateTimePeriod comparand)
        {
            return !obj.Equals(comparand);
        }

        public static DateTimePeriod Between(DateTime one, DateTime two)
        {
            return one > two
                ? new DateTimePeriod(two, one)
                : new DateTimePeriod(one, two);
        }

        public override int GetHashCode()
        {
            return Beginning.GetHashCode() ^ Ending.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((DateTimePeriod)obj);
        }

        public bool Equals(DateTimePeriod other)
        {
            return Beginning == other.Beginning && Ending == other.Ending;
        }
    }
}