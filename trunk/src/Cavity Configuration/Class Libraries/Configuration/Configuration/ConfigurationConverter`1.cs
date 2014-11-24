namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using Cavity.Diagnostics;

    public sealed class ConfigurationConverter<T> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context,
                                            Type sourceType)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
                                           CultureInfo culture,
                                           object value)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            var arg = value as string;
            return null == arg
                       ? base.ConvertFrom(context, culture, value)
                       : Activator.CreateInstance(typeof(T), arg);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                                         CultureInfo culture,
                                         object value,
                                         Type destinationType)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            return Convert.ToString(value, culture);
        }
    }
}