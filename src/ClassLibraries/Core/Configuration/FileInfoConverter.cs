namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using Cavity.Diagnostics;

    public sealed class FileInfoConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context,
                                            Type sourceType)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            return typeof(string).Equals(sourceType) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
                                           CultureInfo culture,
                                           object value)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            var path = value as string;
            return null == path
                       ? base.ConvertFrom(context, culture, value)
                       : new FileInfo(path);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                                         CultureInfo culture,
                                         object value,
                                         Type destinationType)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            return Convert.ToString(value, culture);
        }
    }
}