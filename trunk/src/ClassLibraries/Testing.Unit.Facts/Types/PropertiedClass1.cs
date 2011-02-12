namespace Cavity.Types
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public sealed class PropertiedClass1
    {
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "This is for testing purposes.")]
        public string ArgumentExceptionValue
        {
            get
            {
                return null;
            }

            set
            {
                throw new ArgumentException("A message.");
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "This is for testing purposes.")]
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

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "This is for testing purposes.")]
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

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "This is for testing purposes.")]
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