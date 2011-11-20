namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using Cavity.Diagnostics;

    public sealed class ConfigurationValidator<T> : ConfigurationValidatorBase
    {
        public override bool CanValidate(Type type)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            return typeof(T).Equals(type);
        }

        public override void Validate(object value)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var obj = (T)value;
            if (ReferenceEquals(null, obj))
            {
                throw new ArgumentOutOfRangeException("value");
            }
        }
    }
}