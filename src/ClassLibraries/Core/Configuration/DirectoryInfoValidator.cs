namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Cavity.Diagnostics;

    public sealed class DirectoryInfoValidator : ConfigurationValidatorBase
    {
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Apparently, type cannot be null.")]
        public override bool CanValidate(Type type)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            return typeof(DirectoryInfo).Equals(type);
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Apparently, object cannot be null.")]
        public override void Validate(object value)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (null == value as DirectoryInfo)
            {
                throw new ArgumentOutOfRangeException("value");
            }
        }
    }
}