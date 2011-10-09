namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using Cavity.Diagnostics;

    public sealed class DirectoryInfoValidator : ConfigurationValidatorBase
    {
        public override bool CanValidate(Type type)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            return typeof(DirectoryInfo).Equals(type);
        }

        public override void Validate(object value)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
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