namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class ConfigurationConverterOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ConfigurationConverter<AbsoluteUri>>()
                            .DerivesFrom<TypeConverter>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ConfigurationConverter<AbsoluteUri>());
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContextNull_TypeString()
        {
            var sourceType = typeof(string);

            Assert.True(new ConfigurationConverter<AbsoluteUri>().CanConvertFrom(null, sourceType));
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContext_TypeInt()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var sourceType = typeof(int);

            Assert.False(new ConfigurationConverter<AbsoluteUri>().CanConvertFrom(context, sourceType));
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContext_TypeString()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var sourceType = typeof(string);

            Assert.True(new ConfigurationConverter<AbsoluteUri>().CanConvertFrom(context, sourceType));
        }

        [Fact]
        public void op_ConvertFrom_ITypeDescriptorContext_CultureInfo_object()
        {
            const string expected = "http://example.com/";
            var context = new Mock<ITypeDescriptorContext>().Object;
            var culture = CultureInfo.InvariantCulture;
            var value = (object)expected;

            var actual = (AbsoluteUri)new ConfigurationConverter<AbsoluteUri>().ConvertFrom(context, culture, value);
            if (null == actual)
            {
                Assert.NotNull(null);
            }
            else
            {
                Assert.Equal((AbsoluteUri)expected, actual);
            }
        }

        [Fact]
        public void op_ConvertFrom_ITypeDescriptorContext_CultureInfo_objectInt()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var culture = CultureInfo.InvariantCulture;
            var value = (object)123;

            Assert.Throws<NotSupportedException>(() => new ConfigurationConverter<AbsoluteUri>().ConvertFrom(context, culture, value));
        }

        [Fact]
        public void op_ConvertTo_ITypeDescriptorContext_CultureInfo_object_Type()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var culture = CultureInfo.InvariantCulture;
            var value = (object)123;
            var destinationType = typeof(string);

            var actual = new ConfigurationConverter<AbsoluteUri>().ConvertTo(context, culture, value, destinationType);

            Assert.Equal("123", actual);
        }
    }
}