namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public sealed class DirectoryInfoValidator : ConfigurationValidatorBase
    {
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Apparently, type cannot be null.")]
        public override bool CanValidate(Type type)
        {
            return typeof(DirectoryInfo).Equals(type);
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Apparently, object cannot be null.")]
        public override void Validate(object value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (null == new DirectoryInfo((string)value))
            {
                throw new ArgumentOutOfRangeException("value");
            }
        }
    }
}