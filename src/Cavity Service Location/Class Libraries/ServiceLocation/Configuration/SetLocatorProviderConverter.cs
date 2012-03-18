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
            return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
                                           CultureInfo culture, 
                                           object value)
        {
            var name = value as string;
            if (null != name)
            {
                var type = Type.GetType(name);
                if (null != type)
                {
                    return Activator.CreateInstance(type, true);
                }
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