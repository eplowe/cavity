namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public sealed class SetLocatorProviderConverter : TypeConverter
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
            var type = value as string;
            if (null != type)
            {
                var provider = (ISetLocatorProvider)Activator.CreateInstance(Type.GetType(type), true);
                return provider;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                                         CultureInfo culture,
                                         object value,
                                         Type destinationType)
        {
            throw new NotSupportedException();
        }
    }
}