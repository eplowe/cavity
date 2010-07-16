namespace Cavity.Types
{
    using System;

    public sealed class PropertiedClass1
    {
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

        public bool AutoBoolean { get; set; }

        public string AutoString { get; set; }

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