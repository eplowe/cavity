namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;

    public sealed class DirectoryInfoConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context,
                                            Type sourceType)
        {
            return typeof(string).Equals(sourceType) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
                                           CultureInfo culture,
                                           object value)
        {
            var path = value as string;
            return null == path
                       ? base.ConvertFrom(context, culture, value)
                       : new DirectoryInfo(path);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                                         CultureInfo culture,
                                         object value,
                                         Type destinationType)
        {
            return Convert.ToString(value, culture);
        }
    }
}