namespace Cavity.Types
{
    using System;

    public class PropertiedClass1
    {
        public bool AutoBoolean
        {
            get;
            set;
        }

        public string AutoString
        {
            get;
            set;
        }

        public string ArgumentExceptionValue
        {
            get
            {
                return null;
            }

            set
            {
                throw new ArgumentException();
            }
        }

        public string ArgumentNullExceptionValue
        {
            get
            {
                return null;
            }

            set
            {
                throw new ArgumentNullException("value");
            }
        }

        public string ArgumentOutOfRangeExceptionValue
        {
            get
            {
                return null;
            }

            set
            {
                throw new ArgumentOutOfRangeException("value");
            }
        }

        public string FormatExceptionValue
        {
            get
            {
                return null;
            }

            set
            {
                throw new FormatException("value");
            }
        }
    }
}